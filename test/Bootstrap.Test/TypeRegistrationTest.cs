using DryIoc;
using Bootstrap.Abstraction;
using FluentAssertions;

namespace Bootstrap.Test;

public class TypeRegistrationTest
{
    private readonly Type[] _types = new Type[]
    {
        typeof(MockClass), 
        typeof(IMockClass),
        typeof(AutoA),
        typeof(IAutoRegType),
        typeof(MockModule),
        typeof(IModule),
        typeof(IMockAssemblyMarker),
        typeof(IMockClass),
        typeof(IAutoRegType),
        typeof(A),
        typeof(B),
    };
    
    [Fact]
    public void RegisterDefault()
    {
        var container = new Container();
        var typeRegister = new TypeRegistrator(container, _types);

        typeRegister.Include<IModule>();

        container.Resolve<IModule>().Should().BeOfType<MockModule>();
        container.Invoking(c => c.Resolve<MockModule>())
            .Should().Throw<Exception>();
    }
    
    [Fact]
    public void RegisterIncludeClassType()
    {
        var container = new Container();
        var typeRegister = new TypeRegistrator(container, _types)
        {
            IncludeRegistrationOfClassType = true
        };

        typeRegister.Include<IModule>();

        container.Resolve<IModule>().Should().BeOfType<MockModule>();
        container.Resolve<MockModule>().Should().BeOfType<MockModule>();
    }
    
    [Fact]
    public void RegisterPublicClassType()
    {
        var container = new Container();
        var typeRegister = new TypeRegistrator(container, _types)
        {
            PublicClassesOnly = true
        };

        typeRegister.Include<IModule>();

        container.Invoking(c => c.Resolve<IModule>())
            .Should().Throw<Exception>();
    }
    
    public class A: IMockClass { }
    public class B: A { }
    
    [Fact]
    public void RegisterSubTypes()
    {
        var container = new Container();
        var typeRegister = new TypeRegistrator(container, _types);
        typeRegister.SubTypesToRegister.Add(typeof(A));
        
        typeRegister.Include<IMockClass>();

        container.ResolveMany<IMockClass>().Count().Should().Be(3);
        container.ResolveMany<A>().Count().Should().Be(2);
    }
}


