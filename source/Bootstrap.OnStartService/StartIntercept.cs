using System;
using System.Threading.Tasks;
using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap.Decorators;

internal class StartIntercept: IBootstrap
{
    private readonly PreLoads _preLoads = new();
    private readonly IBootstrap _bootstrap;

    internal StartIntercept(IBootstrap bootstrap) => _bootstrap = bootstrap;

    public IBootstrap WithRegistration(Action<IRegistrator> registration) => _bootstrap.WithRegistration(registration);

    public IBootstrap WithAssemblyScanning<T>(Action<ITypeRegistrator> includeType = null) where T : IAssemblyMarker =>
        _bootstrap.WithAssemblyScanning<T>(includeType);

    public IBootstrap WithAppLevelExceptionHandling(
        Action<object, UnhandledExceptionEventArgs> handleException) =>
        _bootstrap.WithAppLevelExceptionHandling(handleException);

    public void Start<T>(Func<T, Task> executionAction = null)
    {
        _bootstrap.Start<IResolver>(resolver =>
        {
            _preLoads.Execute(resolver);
            executionAction?.Invoke(resolver.Resolve<T>());
            return Task.CompletedTask;
        });
    }
    
    public IBootstrap OnStart<T>(Action<T> whenStart)
    {
        _preLoads.Add(whenStart);
        return this;
    }
    
}