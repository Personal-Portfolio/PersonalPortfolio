using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.Shared.Storage;

namespace PersonalPortfolio.RatesLoader
{
    internal class LoaderHostedService : BackgroundService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<LoaderHostedService> _logger;
        private readonly IContextFactory<PortfolioDbContext> _contextFactory;

        public LoaderHostedService(IHostApplicationLifetime applicationLifetime, ILogger<LoaderHostedService> logger,
            IContextFactory<PortfolioDbContext> contextFactory)
        {
            _applicationLifetime = applicationLifetime
                                   ?? throw new ArgumentNullException(nameof(applicationLifetime));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _contextFactory = contextFactory;
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