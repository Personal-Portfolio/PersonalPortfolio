using Microsoft.EntityFrameworkCore;

namespace PersonalPortfolio.Shared.Storage
{
    public interface IContextModelConfigurator
    {
        void Configure(ModelBuilder builder);
    }
}