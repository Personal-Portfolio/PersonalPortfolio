using System;
using Microsoft.AspNetCore.Components;

namespace PersonalPortfolio.TaxCalc.Client
{
    internal sealed class ServiceProviderComponentActivator : IComponentActivator
    {
        public IServiceProvider ServiceProvider { get; }

        public ServiceProviderComponentActivator(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IComponent CreateInstance(Type componentType)
        {
            var instance = ServiceProvider.GetService(componentType) ?? Activator.CreateInstance(componentType);

            if (!(instance is IComponent component))
            {
                throw new ArgumentException($"The type {componentType.FullName} does not implement {nameof(IComponent)}.", nameof(componentType));
            }

            return component;
        }
    }
}
