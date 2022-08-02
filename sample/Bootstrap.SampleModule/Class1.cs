using Bootstrap.Abstraction;

using DryIoc;

namespace Bootstrap.SampleModule;

public interface IMockAssemblyMarker : IAssemblyMarker { }

public interface IMockClass { }

public interface IAutoRegType { }

internal class MockClass : IMockClass { }

internal class AutoA : IAutoRegType { }

internal class MockModule : IModule
{
    public void Register(IRegistrator registrator)
    {
        registrator.Register<IAutoRegType, AutoA>();
    }
}
