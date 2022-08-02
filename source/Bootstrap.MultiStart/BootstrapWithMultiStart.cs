using System;
using System.Threading.Tasks;
using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap.MultiStart;

internal class BootstrapWithMultiStart: IBootstrap
{
    private readonly IBootstrap _bootstrap;

    internal BootstrapWithMultiStart(IBootstrap bootstrap) => _bootstrap = bootstrap;

    public IBootstrap Register(Action<IRegistrator> registration) => _bootstrap.Register(registration);

    public IBootstrap ScanAssembly<T>(Action<ITypeRegistrator> includeType = null) where T : IAssemblyMarker
    {
        _bootstrap.ScanAssembly<T>(include =>
        {
            //todo: Regsiter all OnStartService
        });
        return this;
    }

    public IBootstrap HookAppLevelExceptionHandling(
        Action<object, UnhandledExceptionEventArgs> handleException) =>
        _bootstrap.HookAppLevelExceptionHandling(handleException);

    public void Start<T>(Func<T, Task> executionAction = null)
    {
        //todo: Initialize all onStartservice
        _bootstrap.Start<IResolver>(resolver =>
        {
            var services = resolver.ResolveMany<IOnStartService>();
            Parallel.ForEach(services, service => service.Initialize());
            executionAction?.Invoke(resolver.Resolve<T>());
            return Task.CompletedTask;
        });
    }
}