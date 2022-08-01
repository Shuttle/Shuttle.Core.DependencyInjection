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
            Guard.AgainstNull(services, nameof(services));
            Guard.AgainstNull(assembly, nameof(assembly));

            _services = services;
            _assembly = assembly;
        }

        public ServiceDescriptorBuilder Filter(Func<Type, bool> func)
        {
            Guard.AgainstNull(func, nameof(func));

            _filter = func;

            return this;
        }

        public ServiceDescriptorBuilder GetServiceType(Func<Type, Type> func)
        {
            Guard.AgainstNull(func, nameof(func));

            _getServiceType = func;

            return this;
        }

        public ServiceDescriptorBuilder GetServiceLifetime(Func<Type, ServiceLifetime> func)
        {
            Guard.AgainstNull(func, nameof(func));

            _getServiceLifetime = func;

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

                _services.Add(new ServiceDescriptor(serviceType, type,
                    serviceLifetime.HasValue ? serviceLifetime.Value : _getServiceLifetime.Invoke(type)));
            }

            return _services;
        }
    }
}