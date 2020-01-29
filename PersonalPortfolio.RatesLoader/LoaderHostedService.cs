using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.Shared.Core;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.RatesLoader
{
    internal class LoaderHostedService : BackgroundService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<LoaderHostedService> _logger;
        private readonly ICurrencyInfoService _currencyInfoProvider;
        private readonly ICurrencyCommandService _currencyCommandService;
        private readonly ICurrencyQueryService _queryService;

        public LoaderHostedService(
            IHostApplicationLifetime applicationLifetime,
            ILogger<LoaderHostedService> logger,
            ICurrencyInfoService currencyInfoProvider,
            ICurrencyCommandService currencyCommandService,
            ICurrencyQueryService queryService)
        {
            _applicationLifetime = applicationLifetime
                                   ?? throw new ArgumentNullException(nameof(applicationLifetime));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _currencyInfoProvider = currencyInfoProvider ?? throw new ArgumentNullException(nameof(currencyInfoProvider));
            _currencyCommandService = currencyCommandService ?? throw new ArgumentNullException(nameof(currencyCommandService));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogDebug("Background service started.");

                _logger.LogDebug("Get supported currencies.");

                var registeredCurrencies = await _queryService.GetRegisteredCurrenciesAsync(stoppingToken)
                    .ConfigureAwait(false);

                var registeredCurrenciesCodes = registeredCurrencies.Select(s => s.Code).ToList();

                _logger.LogInformation("Load rates for list of currencies: {0}", registeredCurrenciesCodes);

                var rates = await _currencyInfoProvider.GetHistoricalRatesForCurrencyList(registeredCurrenciesCodes, stoppingToken)
                    .ConfigureAwait(false);
                var updates = await _currencyCommandService.AddRatesAsync(rates, stoppingToken);

                _logger.LogDebug("Command execution finished.");
                //_applicationLifetime.StopApplication();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.ToString());
                //_applicationLifetime.StopApplication();
            }
        }
    }
}