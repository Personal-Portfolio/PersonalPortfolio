using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace PersonalPortfolio.DataProviders.EuroFx
{
    internal class CurrencyParser
    {
        public CurrencyParser(string baseCurrency, IReadOnlyList<string> currencyFilter)
        {
            _baseCurrency = baseCurrency ?? throw new ArgumentNullException(nameof(baseCurrency));
            _currencyFilter = currencyFilter ?? throw new ArgumentNullException(nameof(currencyFilter));
        }

        public async Task<IEnumerable<(DateTime, string, string, decimal, string)>> ParseResponse(Stream responseStream, CancellationToken token)
        {
            const string defaultNameSpace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";
            const string prefixNameSpace = "http://www.gesmes.org/xml/2002-08-01";
            const string prefix = "gesmes";
            var nodeSelector =
                $@"/*[local-name()='Cube' and namespace-uri()='{defaultNameSpace}']/*[local-name()='Cube' and namespace-uri()='{defaultNameSpace}']";

            var document = await XElement.LoadAsync(responseStream, LoadOptions.None, token)
                .ConfigureAwait(false);

            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace(prefix, prefixNameSpace);

            var source = document.XPathSelectElement($@"/{prefix}:Sender/{prefix}:name", ns)?.Value; 
            
            var expectedRatePairs = Permute(_currencyFilter).ToList();

            return document.XPathSelectElements(nodeSelector)
                .SelectMany(node => ParseDateNode(node, _currencyFilter, expectedRatePairs))
                .Select(r => (r.Date, r.SourceCode, r.TargetCode, r.Value, source));
        }

        #region Private

        private readonly IReadOnlyList<string> _currencyFilter;
        private readonly string _baseCurrency;

        private IEnumerable<RateData> ParseDateNode(
            XElement node,
            IReadOnlyCollection<string> currencyFilter,
            IEnumerable<(string, string)> expectedRatePairs)
        {
            var timeAttribute = node.Attribute("time")?.Value;

            if (string.IsNullOrWhiteSpace(timeAttribute)
                || !DateTime.TryParse(timeAttribute, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AssumeUniversal, out var date)
                || !node.HasElements)
                return new List<RateData>();

            date = date.ToUniversalTime();

            var ratesDictionary = node.Elements()
                .Select(n => ParseCurrencyNode(n, currencyFilter, date))
                .Where(t => t != null)
                .ToDictionary(e => (e.SourceCode, e.TargetCode));

            var matchedData = new List<RateData>();
            var calculatedData = new List<RateData>();

            foreach (var expectedRatePair in expectedRatePairs)
            {
                if (ratesDictionary.ContainsKey(expectedRatePair))
                {
                    matchedData.Add(ratesDictionary[expectedRatePair]);
                }
                else if (ratesDictionary.ContainsKey((_baseCurrency, expectedRatePair.Item1)) &&
                         ratesDictionary.ContainsKey((_baseCurrency, expectedRatePair.Item2)))
                {
                    var rate = ratesDictionary[(_baseCurrency, expectedRatePair.Item1)].Value /
                               ratesDictionary[(_baseCurrency, expectedRatePair.Item2)].Value;

                    calculatedData.Add(new RateData(expectedRatePair.Item1, expectedRatePair.Item2, rate, date));
                }
                else if (ratesDictionary.ContainsKey((expectedRatePair.Item2, expectedRatePair.Item1)))
                {
                    var rate = 1 / ratesDictionary[(expectedRatePair.Item2, expectedRatePair.Item1)].Value;

                    calculatedData.Add(new RateData(expectedRatePair.Item1, expectedRatePair.Item2, rate, date));
                }
            }

            return matchedData.Union(calculatedData);
        }

        private RateData ParseCurrencyNode(XElement node, IEnumerable<string> currencyFilter, DateTime date)
        {
            var rateAttribute = node.Attribute("rate")?.Value;
            var currencyAttribute = node.Attribute("currency")?.Value;

            if (string.IsNullOrWhiteSpace(rateAttribute)
                || !decimal.TryParse(rateAttribute, out var rate)
                || string.IsNullOrWhiteSpace(currencyAttribute)
                || !currencyFilter.Contains(currencyAttribute))
                return null;

            return new RateData(_baseCurrency, currencyAttribute, rate, date);
        }

        private sealed class RateData
        {
            public RateData(string sourceCode, string targetCode, decimal value, DateTime date)
            {
                SourceCode = sourceCode;
                TargetCode = targetCode;
                Value = value;
                Date = date;
            }

            public string SourceCode { get; }
            public string TargetCode { get; }
            public decimal Value { get; }
            public DateTime Date { get; }
        }

        private static IEnumerable<(T, T)> Permute<T>(IReadOnlyList<T> seq)
        {
            var pairs = from firstIndex in Enumerable.Range(0, seq.Count)
                from secondIndex in Enumerable.Range(firstIndex + 1, seq.Count - firstIndex - 1)
                select (seq[firstIndex], seq[secondIndex]);

            return pairs.SelectMany(GenerateInversePair);
        }

        private static IEnumerable<(T, T)> GenerateInversePair<T>((T, T) p)
        {
            yield return p;
            yield return (p.Item2, p.Item1);
        }

        #endregion

    }
}
