using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalPortfolio.Shared.Storage.Abstractions;
using PersonalPortfolio.Shared.Storage.SqlServer.Configuration;

namespace PersonalPortfolio.Shared.Storage.SqlServer
{
    internal class BulkCommandsService<TContext>: IBulkCommandsService
    where TContext: DbContext
    {
        private readonly IOptions<BulkConfiguration> _configuration;
        private readonly TContext _ctx;
        private readonly ILogger<BulkCommandsService<TContext>> _logger;

        public BulkCommandsService(IOptions<BulkConfiguration> configuration, TContext ctx, ILogger<BulkCommandsService<TContext>> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _ctx = ctx ?? throw new ArgumentNullException(nameof(_ctx));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> InsertAsync<T>(IList<T> entities, CancellationToken token) where T : class
        {
            if (_ctx.Model.FindEntityType(typeof(T)) == null)
                throw new InvalidOperationException(
                    $"Context {typeof(TContext)} does not contain model for type {typeof(T)}");


            await using var transaction = await _ctx.Database.BeginTransactionAsync(token).ConfigureAwait(false);

            var config = new BulkConfig
            {
                BatchSize = _configuration?.Value.BatchSize ?? 2000,
                CalculateStats = true,
                SetOutputIdentity = true
            };

            await _ctx.BulkInsertAsync(
                    entities,
                    config,
                    p => _logger.LogDebug($"Inserting entities of {typeof(T)}. Progress: {(p * 100).ToString("F2", CultureInfo.InvariantCulture) + "%"}"),
                    token)
                .ConfigureAwait(false);
            
            await transaction.CommitAsync(token).ConfigureAwait(false);

            return config.StatsInfo.StatsNumberInserted;
        }
    }
}