using System;
using System.Linq.Expressions;

namespace PersonalPortfolio.Shared.Storage.Queries
{
    public interface IMapper<TEntity, TDomainModel>
    {
        Expression<Func<TEntity, TDomainModel>> GetProjection();
    }
}