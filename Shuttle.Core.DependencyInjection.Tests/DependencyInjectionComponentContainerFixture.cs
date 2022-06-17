using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shuttle.Core.Container.Tests;

namespace Shuttle.Core.DependencyInjection.Tests
{
    [TestFixture]
    public class DependencyInjectionComponentContainerFixture : ContainerFixture
    {
        [Test]
        public void Should_be_able_resolve_all_instances()
        {
            IServiceCollection services = new ServiceCollection();

            var registry = new ServiceCollectionComponentRegistry(services);

            RegisterCollection(registry);

            var resolver = new ServiceProviderComponentResolver(services.BuildServiceProvider());

            ResolveCollection(resolver);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_singleton()
        {
            IServiceCollection services = new ServiceCollection();

            var registry = new ServiceCollectionComponentRegistry(services);

            RegisterSingleton(registry);

            var resolver = new ServiceProviderComponentResolver(services.BuildServiceProvider());

            ResolveSingleton(resolver);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_transient_components()
        {
            IServiceCollection services = new ServiceCollection();

            var registry = new ServiceCollectionComponentRegistry(services);

            RegisterTransient(registry);

            var resolver = new ServiceProviderComponentResolver(services.BuildServiceProvider());

            ResolveTransient(resolver);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_an_open_generic_singleton()
        {
            IServiceCollection services = new ServiceCollection();

            var registry = new ServiceCollectionComponentRegistry(services);

            RegisterSingletonGeneric(registry);

            var resolver = new ServiceProviderComponentResolver(services.BuildServiceProvider());

            ResolveSingletonGeneric(resolver);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_transient_open_generic_components()
        {
            IServiceCollection services = new ServiceCollection();

            var registry = new ServiceCollectionComponentRegistry(services);

            RegisterTransientGeneric(registry);

            var resolver = new ServiceProviderComponentResolver(services.BuildServiceProvider());

            ResolveTransientGeneric(resolver);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_a_multiple_singleton()
        {
            IServiceCollection services = new ServiceCollection();

            var registry = new ServiceCollectionComponentRegistry(services);

            RegisterMultipleSingleton(registry);

            var resolver = new ServiceProviderComponentResolver(services.BuildServiceProvider());

            ResolveMultipleSingleton(resolver);
        }

        [Test]
        public void Should_be_able_to_register_and_resolve_multiple_transient_components()
        {
            IServiceCollection services = new ServiceCollection();

            var registry = new ServiceCollectionComponentRegistry(services);

            RegisterMultipleTransient(registry);

            var resolver = new ServiceProviderComponentResolver(services.BuildServiceProvider());

            ResolveMultipleTransient(resolver);
        }
    }
}