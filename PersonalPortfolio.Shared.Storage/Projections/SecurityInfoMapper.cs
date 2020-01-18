using System;
using System.Linq.Expressions;
using PersonalPortfolio.Shared.Core;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage.Projections
{
    public class SecurityInfoMapper: IMapper<Security, SecurityInfo>
    {
        public Expression<Func<Security, SecurityInfo>> GetProjection()
        {
            return e => new SecurityInfo
            {
                Code = e.Ticker,
                Description = e.Description,
                Type = e.Type.Name,
                BaseCurrency = new CurrencyInfo
                {
                    Code = e.BaseCurrency.Code,
                    Description = e.BaseCurrency.Description
                }
            };
        }
    }
}
