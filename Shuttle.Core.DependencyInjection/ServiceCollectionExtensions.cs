using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Shuttle.Core.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceDescriptorBuilder FromAssembly(this IServiceCollection services, Assembly assembly)
        {
            return new(services, assembly);
        }
    }
}