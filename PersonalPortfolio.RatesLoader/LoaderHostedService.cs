using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.Shared.Storage;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.RatesLoader
{
    internal class LoaderHostedService : BackgroundService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<LoaderHostedService> _logger;
        private readonly IContextFactory<PortfolioDbContext> _contextFactory;
        private readonly ISecurityQueryService _queryService;

        public LoaderHostedService(
            IHostApplicationLifetime applicationLifetime,
            ILogger<LoaderHostedService> logger,
            IContextFactory<PortfolioDbContext> contextFactory,
            ISecurityQueryService queryService)
        {
            _applicationLifetime = applicationLifetime
                                   ?? throw new ArgumentNullException(nameof(applicationLifetime));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogDebug("Background service started.");

                await using (var ctx = _contextFactory.CreateDbContext())
                {
                    await ctx.Database.MigrateAsync(stoppingToken).ConfigureAwait(false);
                }

                var test = await _queryService.GetAllSecurityInfos(stoppingToken).ConfigureAwait(false);

                foreach (var info in test)
                {
                    _logger.LogInformation($"Loaded security {info.Code} of type {info.Type} and with base currency {info.BaseCurrency.Code}");
                }

                _logger.LogDebug("Command execution finished.");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.ToString());
                _applicationLifetime.StopApplication();
            }
        }
    }
}