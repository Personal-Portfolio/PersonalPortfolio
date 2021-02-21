using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalPortfolio.DataProviders.Moex.Model;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.PricesLoader
{
    internal class LoaderHostedService : BackgroundService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<LoaderHostedService> _logger;
        private readonly ISecurityInfoService _client;

        public LoaderHostedService(
            IHostApplicationLifetime applicationLifetime,
            ILogger<LoaderHostedService> logger,
            ISecurityInfoService client)
        {
            _applicationLifetime = applicationLifetime
                                   ?? throw new ArgumentNullException(nameof(applicationLifetime));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogDebug("Background service started.");

                var securityCode = new []
                {
                    "FXMM", "FXRL", "SU25083RMFS5", "FXRB"
                };

                var dict = new Dictionary<string, ISecurityPrice[]>();

                foreach (var code in securityCode)
                {
                    var prices = await _client.GetHistory(code, stoppingToken).ConfigureAwait(false);

                    dict.Add(code, prices.ToArray());
                }


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