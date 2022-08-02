using Bootstrap.Abstraction;

using DryIoc;

using System;
using System.Threading.Tasks;
using DryIoc.Microsoft.DependencyInjection;

namespace Bootstrap.MsDi;

internal class BootstrapWithMsDi : IBootstrap
{
    private readonly IBootstrap _bootstrap;
    private readonly IContainer _container;

    internal BootstrapWithMsDi(IBootstrap bootstrap, IContainer container)
    {
        _bootstrap = bootstrap;
        _container = container;
    }

    public void Start<T>(Func<T, Task> executionAction = null)
    {
        Parallel.ForEach(_container.ResolveMany<IModule>(), module =>
        {
            var serviceCollection = new ServiceCollection();
            module.Register(_container);
            _container.Populate(serviceCollection);
        });
        executionAction?.Invoke(_container.Resolve<T>());
    }

    public IBootstrap HookAppLevelExceptionHandling(Action<object, UnhandledExceptionEventArgs> handleException) =>
        _bootstrap.HookAppLevelExceptionHandling(handleException);

    public IBootstrap ScanAssembly<T>(Action<ITypeRegistrator> includeType = null) where T : IAssemblyMarker =>
        _bootstrap.ScanAssembly<T>(includeType);

    public IBootstrap Register(Action<IRegistrator> registration) => _bootstrap.Register(registration);
}