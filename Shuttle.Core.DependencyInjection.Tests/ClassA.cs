using System;

namespace Shuttle.Core.DependencyInjection.Tests;

public interface IClassA
{
    Guid Id { get; }
}

public interface IClassAFirst
{
    Guid Id { get; }
}

public class ClassA : IClassAFirst, IClassA
{
    public ClassA()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}

