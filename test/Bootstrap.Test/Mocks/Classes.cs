using Bootstrap.Abstraction;

using DryIoc;

namespace Bootstrap.Test.Mocks;

internal class MockClass: IMockClass { }

internal class AutoA : IAutoRegType { }

internal class MockModule : IModule
{
    public void Register(IRegistrator registrator)
    {
        registrator.Register<IAutoRegType, AutoA>();
    }
}