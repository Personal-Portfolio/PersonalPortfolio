using System;
using System.Linq.Expressions;

namespace PersonalPortfolio.Shared.Storage.Abstractions
{
    public interface IMapper<TEntity, TDomainModel>
    {
        Expression<Func<TEntity, TDomainModel>> GetProjection();
    }
}