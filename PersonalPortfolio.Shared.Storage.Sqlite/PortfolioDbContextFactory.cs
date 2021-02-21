using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalPortfolio.Shared.Storage.Abstractions;

namespace PersonalPortfolio.Shared.Storage.Sqlite
{
    //internal class PortfolioDbContextFactory: IContextFactory<PortfolioDbContext>
    //{
    //    private const string ConnectionStringName = "PortfolioDb";
    //    private readonly string _connectionString;

    //    public PortfolioDbContextFactory(IConfiguration configuration)
    //    {
    //        _connectionString = configuration.GetConnectionString(ConnectionStringName) ?? 
    //            throw new ArgumentNullException(nameof(configuration), $"Connection string with name {ConnectionStringName} not found.");
    //    }

    //    public PortfolioDbContext CreateDbContext()
    //    {
    //        var ctxOptionsBuilder = new DbContextOptionsBuilder<PortfolioDbContext>();
    //        ctxOptionsBuilder
    //            .UseSqlite(_connectionString, o => o.MigrationsAssembly(typeof(PortfolioDbContextFactory).Assembly.FullName));

    //        return new PortfolioDbContext(ctxOptionsBuilder.Options, null);
    //    }
    //}
}
