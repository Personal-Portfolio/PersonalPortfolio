﻿using Microsoft.EntityFrameworkCore;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Configurations
{
    internal class ContextModelConfigurator: IContextModelConfigurator
    {
        public void Configure(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SecurityConfiguration());
            builder.ApplyConfiguration(new SymbolRateConfiguration());
        }
    }
}