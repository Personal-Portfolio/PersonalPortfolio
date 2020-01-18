using Microsoft.EntityFrameworkCore;

namespace PersonalPortfolio.Shared.Storage.Abstractions
{
    public interface IContextModelConfigurator
    {
        void Configure(ModelBuilder builder);
    }
}