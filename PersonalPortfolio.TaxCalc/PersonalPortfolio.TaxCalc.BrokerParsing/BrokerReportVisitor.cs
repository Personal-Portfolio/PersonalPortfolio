using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cursively;

namespace PersonalPortfolio.TaxCalc.BrokerParsing
{
    internal sealed class BrokerReportVisitor : CsvReaderVisitorWithUTF8HeadersBase
    {
        private readonly Decoder _utf8Decoder;
        private readonly Action<Dictionary<string, string>> _onEndOfDataRecord;

        private Dictionary<string, string> _record;

        private readonly char[] _buffer;
        private int _bufferConsumed;

        public BrokerReportVisitor(Decoder decoder, int maxFieldLength, Action<Dictionary<string, string>> onEndOfDataRecord)
        {
            _utf8Decoder = decoder;
            _onEndOfDataRecord = onEndOfDataRecord;
            _buffer = new char[maxFieldLength];
        }

        protected override void VisitPartialDataFieldContents(ReadOnlySpan<byte> chunk) =>
            VisitFieldContents(chunk, false);

        protected override void VisitEndOfDataField(ReadOnlySpan<byte> chunk) =>
            VisitFieldContents(chunk, true);

        protected override void VisitEndOfDataRecord()
        {
            _onEndOfDataRecord(_record);
        }

        protected override void VisitEndOfHeaderRecord()
        {
            _record = Headers.ToDictionary(h => h, _ => string.Empty);
        }

        private void VisitFieldContents(ReadOnlySpan<byte> chunk, bool flush)
        {
            var charCount = _utf8Decoder.GetCharCount(chunk, flush);
            
            if (charCount + _bufferConsumed > _buffer.Length)
                throw new InvalidDataException($"Field is longer than {_buffer.Length} characters.");

            _utf8Decoder.GetChars(chunk, new Span<char>(_buffer, _bufferConsumed, charCount), flush);
            _bufferConsumed += charCount;

            if (!flush)
                return;

            _record[Headers[CurrentFieldIndex]] = new string(_buffer, 0, _bufferConsumed);
            _bufferConsumed = 0;
        }
    }
}
