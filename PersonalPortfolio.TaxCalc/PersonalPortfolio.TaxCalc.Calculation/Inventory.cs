using System;
using System.Collections.Generic;
using PersonalPortfolio.TaxCalc.Core;

namespace PersonalPortfolio.TaxCalc.Calculation
{
    internal class Inventory
    {
        private readonly Queue<Asset> _queue = new();

        public void InputAssets(int quantity, DateTime date, decimal cost, decimal totalFee)
        {
            for (var i = 0; i < quantity; i++)
            {
                _queue.Enqueue(new Asset
                {
                    Fee = totalFee / quantity,
                    Price = cost / quantity,
                    SettleDate = date
                });
            }
        }

        public IEnumerable<Asset> SellAssets(int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                yield return _queue.Dequeue();
            }
        }
    }
}