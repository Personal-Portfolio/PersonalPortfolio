using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.Shared.Core;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage.Commands
{
    public class CurrencyCommandService : ICurrencyCommandService
    {
        private readonly PortfolioDbContext _ctx;
        private readonly ILogger<ICurrencyCommandService> _logger;
        private readonly IBulkCommandsService _bulkCommandsService;

        // TODO: dataSource store
        private static readonly IReadOnlyDictionary<string, int> Sources = new Dictionary<string, int>
        {
            {"European Central Bank", 1 }
        };

        public CurrencyCommandService(
            PortfolioDbContext ctx,
            ILogger<ICurrencyCommandService> logger,
            IBulkCommandsService bulkCommandsService)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bulkCommandsService = bulkCommandsService ?? throw new ArgumentNullException(nameof(bulkCommandsService));
        }

        public async Task<int> AddOrUpdateAsync(IEnumerable<CurrencyInfo> infos, CancellationToken token)
        {

            // TODO: bulk insert or update
            var counter = new ConcurrentBag<int>();

            foreach (var currencyInfo in infos)
            {
                var data = await _ctx.Currencies
                    .FirstOrDefaultAsync(m => m.Code == currencyInfo.Code, token)
                    .ConfigureAwait(false);
                
                if (data != null)
                {
                    data.Description = currencyInfo.Description;
                    data.DateUpdated = DateTime.UtcNow;
                }
                else
                {
                    data = new Currency
                    {
                        Description = currencyInfo.Description,
                        Code = currencyInfo.Code
                    };

                    await _ctx.Currencies.AddAsync(data, token).ConfigureAwait(false);
                }

                var count = await _ctx.SaveChangesAsync(token)
                    .ConfigureAwait(false);

                counter.Add(count);
            }

            return counter.Sum();
        }

        public async Task<int> AddRatesAsync(IEnumerable<(DateTime, string, string, decimal, string)> rates, CancellationToken token)
        {
            var currencyMap = await _ctx.Currencies
                .ToDictionaryAsync(c => c.Code, v => v.Id, token)
                .ConfigureAwait(false);

            var currencyTimestamps = await _ctx.CurrencyRates
                .GroupBy(e => new { e.SourceCurrencyId, e.CurrencyId, e.DataSourceId })
                .Select(g => new { g.Key, Value = g.Max(e => e.RateTime) })
                .ToDictionaryAsync(e => (e.Key.SourceCurrencyId, e.Key.CurrencyId, e.Key.DataSourceId), e => e.Value, token)
                .ConfigureAwait(false);

            var entities = new List<CurrencyRate>();

            foreach (var (rateDate, sourceCode, targetCode, value, dataSource) in rates)
            {
                if (!currencyMap.TryGetValue(sourceCode, out var sourceId)
                    || !currencyMap.TryGetValue(targetCode, out var targetId))
                {
                    _logger.LogWarning("Unknown currency pair: {0}-{1}", sourceCode, targetCode);
                    continue;
                }

                if (currencyTimestamps.TryGetValue((sourceId, targetId, 0), out var date)
                    && date >= rateDate.Date)
                {
                    // The data is below the last time stamp
                    // Intend to use the full reset of the store
                    continue;
                }

                if (!Sources.TryGetValue(dataSource, out var dataSourceId))
                {
                    _logger.LogWarning("Unknown data source: {0}", dataSource);
                    continue;
                }

                entities.Add(new CurrencyRate
                {
                    DateCreated = DateTime.UtcNow,
                    SourceCurrencyId = sourceId,
                    CurrencyId = targetId,
                    RateTime = rateDate.Date,
                    Value = value,
                    DataSourceId = dataSourceId
                });
            }

            return await InsertEntities(entities, token);
        }

        private async Task<int> InsertEntities(IList<CurrencyRate> entities, CancellationToken token)
        {
            int counter;

            if (!entities.Any())
            {
                counter = 0;
            }
            else if (_bulkCommandsService == null)
            {
                await _ctx.CurrencyRates.AddRangeAsync(entities, token)
                    .ConfigureAwait(false);

                counter = await _ctx.SaveChangesAsync(token)
                    .ConfigureAwait(false);
            }
            else
            {
                counter = await _bulkCommandsService.InsertAsync(entities, token).ConfigureAwait(false);
            }

            _logger.LogDebug("Inserted {0} items.", counter);

            return counter;
        }
    }
}
