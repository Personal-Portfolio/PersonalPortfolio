using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.DataProviders.Moex.Model;
using PersonalPortfolio.Shared.Core;

namespace PersonalPortfolio.DataProviders.Moex
{
    internal class SecurityInfoService : ISecurityInfoService
    {
        private readonly IExchangeClient _exchangeClient;
        public SecurityInfoService(IExchangeClient exchangeClient)
        {
            _exchangeClient = exchangeClient ?? throw new ArgumentNullException(nameof(exchangeClient));
        }

        public async Task<IEnumerable<ISecurityPrice>> GetHistory(string securityCode, CancellationToken token)
        {
            // Get security definition
            var securityDefinition = await _exchangeClient.GetSecurityDefinition(securityCode, token)
                .ConfigureAwait(false);

            if (securityDefinition == null)
                return new Price[0];

            // Find Primary board
            var board = securityDefinition.Boards.FirstOrDefault(e => e.IsPrimary);

            if (board == null
                || string.IsNullOrWhiteSpace(board.CurrencyId)
                || string.IsNullOrWhiteSpace(board.EngineName)
                || string.IsNullOrWhiteSpace(board.MarketName)
                || !board.HistoryFrom.HasValue
                || !board.HistoryTill.HasValue)
                return new Price[0];

            // Get history info by board range
            var history = await GetFullSecurityHistory(
                board.EngineName, board.MarketName, securityCode, board.HistoryFrom.Value, board.HistoryTill.Value, token)
                .ConfigureAwait(false);

            // Load history

            return history.Select(h => new Price
            {
                SecurityCode = securityCode,
                CurrencyCode = board.CurrencyId,
                TradeDate = h.TradeDate,
                Average = h.Average,
                Low = h.Low,
                High = h.High,
                Close = h.Last,
                Open = h.Open,
                DataSourceCode = $"MOEX:{board.BoardId}"
            });
        }

        private async Task<IEnumerable<SecurityHistoryData>> GetFullSecurityHistory(
            string engine,
            string market, string securityCode,
            DateTime from,
            DateTime till,
            CancellationToken token)
        {
            var index = 0;
            int total;
            var history = new List<SecurityHistoryData>();

            do
            {
                var res = await _exchangeClient.GetSecurityHistory(engine, market, securityCode, from, till, index, token)
                    .ConfigureAwait(false);

                history.AddRange(res.History);
                index = res.Cursor[0].Index + res.Cursor[0].PageSize;
                total = res.Cursor[0].Total;

            } while (total >= index);

            return history;
        }
    }
}