# Shuttle.Core.DependencyInjection

```
PM> Install-Package ShuttleShuttle.Core.DependencyInjection
```

The `NinjectComponentContainer` implements both the `IComponentRegistry` and `IComponentResolver` interfaces.  

```c#
IServiceCollection services = new ServiceCollection();

var registry = new ServiceCollectionComponentRegistry(services);

// register all dependencies

var resolver = new ServiceProviderComponentResolver(services.BuildServiceProvider());
```

However, in a typical `dotnet` application one would make use of the [dependency injection](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) instances that are available.