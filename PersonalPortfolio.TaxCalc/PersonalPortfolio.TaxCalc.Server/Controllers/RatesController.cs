using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.Shared.Abstractions;

namespace PersonalPortfolio.TaxCalc.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {

        private readonly ILogger<RatesController> _logger;
        private readonly IRatesProvider _provider;
        private readonly IMemoryCache _cache;

        public RatesController(ILogger<RatesController> logger, IRatesProvider provider, IMemoryCache cache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet]
        [Route("{dateTics}")]
        public Task<IEnumerable<IRate>> Get(long dateTics, CancellationToken cancellation)
        {
            var date = DateTime.FromBinary(dateTics);

            return _cache.GetOrCreateAsync(dateTics, async entry =>
            {
                _logger.LogInformation($"Get rates from server for date {date}.");

                var item = await _provider.GetRatesForDateAsync(date, cancellation)
                    .ConfigureAwait(false);

                if (item == null)
                    return null;

                entry
                    .SetValue(item)
                    .SetPriority(CacheItemPriority.NeverRemove);

                return item;
            });
        }
    }
}
