using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalPortfolio.DbInitializer.Configuration;
using PersonalPortfolio.Shared.Core;
using PersonalPortfolio.Shared.Storage;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.DbInitializer
{
    internal class DbInitializerHostedService : BackgroundService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<DbInitializerHostedService> _logger;
        private readonly PortfolioDbContext _context;
        private readonly ICurrencyCommandService _currencyCommandService;
        private readonly List<CurrencyInfo> _supportedCurrencies;

        public DbInitializerHostedService(
            IHostApplicationLifetime applicationLifetime,
            ILogger<DbInitializerHostedService> logger,
            PortfolioDbContext context,
            ICurrencyCommandService currencyCommandService,
            IOptions<Financial> supportedCurrencies)
        {
            _applicationLifetime = applicationLifetime
                                   ?? throw new ArgumentNullException(nameof(applicationLifetime));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _currencyCommandService = currencyCommandService ?? throw new ArgumentNullException(nameof(currencyCommandService));
            _supportedCurrencies = supportedCurrencies?.Value?.RegisteredCurrencies?.ToList() ?? new List<CurrencyInfo>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogDebug("Background service started.");

                var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync(stoppingToken).ConfigureAwait(false);
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync(stoppingToken).ConfigureAwait(false);

                _logger.LogInformation("Already applied migrations: {0}", appliedMigrations);
                _logger.LogInformation("Pending migrations: {0}", pendingMigrations);

                _logger.LogInformation("Start migrations...");

                await _context.Database.MigrateAsync(stoppingToken).ConfigureAwait(false);

                _logger.LogInformation("Done.");

                _logger.LogInformation("Update supported currencies: {0}", _supportedCurrencies.Select(c => c.Code));
                await _currencyCommandService.AddOrUpdateAsync(_supportedCurrencies, stoppingToken);

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