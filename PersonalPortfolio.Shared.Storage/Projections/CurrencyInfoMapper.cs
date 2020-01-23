using System;
using System.Linq.Expressions;
using PersonalPortfolio.Shared.Core;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage.Projections
{
    public class CurrencyInfoMapper: IMapper<Currency, CurrencyInfo>
    {
        public Expression<Func<Currency, CurrencyInfo>> GetProjection()
        {
            return e => new CurrencyInfo
            {
                Code = e.Code,
                Description = e.Description
            };
        }
    }
}
