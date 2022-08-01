namespace Shuttle.Core.DependencyInjection.Tests;

public interface IClassBSecond
{
}

public interface IClassBFirst
{
}

public class ClassB : IClassBFirst, IClassBSecond
{
    
}
