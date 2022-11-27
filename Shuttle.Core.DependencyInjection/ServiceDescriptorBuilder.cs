using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.DependencyInjection
{
    public class ServiceDescriptorBuilder
    {
        private readonly Assembly _assembly;
        private readonly IServiceCollection _services;
        private Func<Type, bool> _filter = type => true;

        private Func<Type, ServiceLifetime> _getServiceLifetime = type => ServiceLifetime.Singleton;

        private Func<Type, Type> _getServiceType = type =>
        {
            var interfaces = type.GetInterfaces();

            if (interfaces.Length == 0)
            {
                return null;
            }

            return interfaces.FirstOrDefault(item => item.Name.Equals($"I{type.Name}")) ?? interfaces.First();
        };

        public ServiceDescriptorBuilder(IServiceCollection services, Assembly assembly)
        {
            _services = Guard.AgainstNull(services, nameof(services));
            _assembly = Guard.AgainstNull(assembly, nameof(assembly));
        }

        public ServiceDescriptorBuilder Filter(Func<Type, bool> filter)
        {
            _filter = Guard.AgainstNull(filter, nameof(filter));

            return this;
        }

        public ServiceDescriptorBuilder GetServiceType(Func<Type, Type> getServiceType)
        {
            _getServiceType = Guard.AgainstNull(getServiceType, nameof(getServiceType));

            return this;
        }

        public ServiceDescriptorBuilder GetServiceLifetime(Func<Type, ServiceLifetime> getServiceLifetime)
        {
            _getServiceLifetime = Guard.AgainstNull(getServiceLifetime, nameof(getServiceLifetime));

            return this;
        }

        public IServiceCollection Add(ServiceLifetime? serviceLifetime = null)
        {
            foreach (var type in _assembly.GetTypes())
            {
                if (type.IsInterface || type.IsAbstract || !_filter.Invoke(type))
                {
                    continue;
                }

                var serviceType = _getServiceType.Invoke(type);

                if (serviceType == null)
                {
                    continue;
                }

                _services.Add(new ServiceDescriptor(serviceType, type, serviceLifetime ?? _getServiceLifetime.Invoke(type)));
            }

            return _services;
        }
    }
}