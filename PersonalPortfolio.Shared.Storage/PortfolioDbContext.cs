using Microsoft.EntityFrameworkCore;

namespace PersonalPortfolio.Shared.Storage
{
    public class PortfolioDbContext: DbContext
    {
        public PortfolioDbContext(DbContextOptions options): base(options)
        { }

        public DbSet<Security> Securities { get; set; }
        public DbSet<SymbolRate> Rates { get; set; }
    }
}