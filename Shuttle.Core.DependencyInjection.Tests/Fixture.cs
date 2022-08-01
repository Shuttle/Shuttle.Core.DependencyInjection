using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Shuttle.Core.DependencyInjection.Tests;

[TestFixture]
public class Fixture
{
    [Test]
    public void Should_be_able_add_all_components_when_not_filtered()
    {
        var services = new ServiceCollection();

        services.FromAssembly(typeof(Fixture).Assembly).Add();

        var serviceProvider = services.BuildServiceProvider();

        Assert.That(serviceProvider.GetService<IClassA>(), Is.Not.Null);

        var id = serviceProvider.GetRequiredService<IClassA>().Id;

        // ensure that they are both singleton
        Assert.That(serviceProvider.GetRequiredService<IClassA>().Id, Is.EqualTo(id));
        Assert.That(serviceProvider.GetRequiredService<IClassA>().Id, Is.EqualTo(id));
        
        Assert.That(serviceProvider.GetService<IClassBFirst>(), Is.Not.Null);
        Assert.That(serviceProvider.GetService<IClassBSecond>(), Is.Null);
    }

    [Test]
    public void Should_be_able_add_filtered_components_as_transient()
    {
        var services = new ServiceCollection();

        services.FromAssembly(typeof(Fixture).Assembly)
            .Filter(type => type.Name.Equals("ClassA", StringComparison.InvariantCultureIgnoreCase))
            .GetServiceType(type => typeof(IClassAFirst))
            .GetServiceLifetime(type => ServiceLifetime.Transient)
            .Add();

        var serviceProvider = services.BuildServiceProvider();

        Assert.That(serviceProvider.GetService<IClassA>(), Is.Null);
        Assert.That(serviceProvider.GetService<IClassAFirst>(), Is.Not.Null);

        var id = serviceProvider.GetRequiredService<IClassAFirst>().Id;

        // ensure that they are all transient, with their own id
        Assert.That(serviceProvider.GetRequiredService<IClassAFirst>().Id, Is.Not.EqualTo(id));
        Assert.That(serviceProvider.GetRequiredService<IClassAFirst>().Id, Is.Not.EqualTo(id));
        
        Assert.That(serviceProvider.GetService<IClassBFirst>(), Is.Null);
        Assert.That(serviceProvider.GetService<IClassBSecond>(), Is.Null);
    }
}