using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Container;
using Shuttle.Core.Contract;

namespace Shuttle.Core.DependencyInjection
{
    public class ServiceCollectionComponentRegistry : ComponentRegistry
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionComponentRegistry(IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));

            _services = services;
        }

        public override IComponentRegistry Register(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(implementationType, "implementationType");

            base.Register(dependencyType, implementationType, lifestyle);

            try
            {
                switch (lifestyle)
                {
                    case Lifestyle.Transient:
                        {
                            _services.AddTransient(dependencyType, implementationType);

                            break;
                        }
                    default:
                        {
                            _services.AddSingleton(dependencyType, implementationType);

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }

        public override IComponentRegistry RegisterCollection(Type dependencyType, IEnumerable<Type> implementationTypes, Lifestyle lifestyle)
        {
            Guard.AgainstNull(implementationTypes, "implementationTypes");

            base.RegisterCollection(dependencyType, implementationTypes, lifestyle);

            foreach (var implementationType in implementationTypes)
            {
                try
                {
                    switch (lifestyle)
                    {
                        case Lifestyle.Transient:
                            {
                                _services.AddTransient(dependencyType, implementationType);

                                break;
                            }
                        default:
                            {
                                _services.AddSingleton(dependencyType, implementationType);

                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    throw new TypeRegistrationException(ex.Message, ex);
                }
            }

            return this;
        }

        public override IComponentRegistry RegisterInstance(Type dependencyType, object instance)
        {
            Guard.AgainstNull(dependencyType, "dependencyType");
            Guard.AgainstNull(instance, "instance");

            base.RegisterInstance(dependencyType, instance);

            try
            {
                _services.AddSingleton(dependencyType, instance);
            }
            catch (Exception ex)
            {
                throw new TypeRegistrationException(ex.Message, ex);
            }

            return this;
        }

        public override IComponentRegistry RegisterGeneric(Type dependencyType, Type implementationType, Lifestyle lifestyle)
        {
            return Register(dependencyType, implementationType, lifestyle);
        }
    }
}