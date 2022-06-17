using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Core.DependencyInjection
{
    public class ServiceProviderComponentResolver : IComponentResolver
    {
        private readonly IServiceProvider _services;

        public ServiceProviderComponentResolver(IServiceProvider services)
        {
            Guard.AgainstNull(services, nameof(services));

            _services = services;
        }

        public object Resolve(Type dependencyType)
        {
            return _services.GetService(dependencyType);
        }

        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            return _services.GetServices(dependencyType);
        }
    }
}