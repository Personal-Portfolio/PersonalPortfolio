using Microsoft.EntityFrameworkCore;

namespace PersonalPortfolio.Shared.Storage
{
    public interface IContextFactory<out TContext> where TContext: DbContext
    {
        TContext CreateDbContext();
    }
}
