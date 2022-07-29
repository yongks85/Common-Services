using System;
using System.Threading.Tasks;
using Bootstrap.Abstraction;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;

namespace Bootstrap;

internal class Bootstrap : IBootstrap
{
    private readonly IContainer _container;
   
    internal Bootstrap(IContainer container)
    {
        _container = container;
    }

    public IBootstrap WithRegistration(Action<IRegistrator> registration)
    {
        registration(_container);
        return this;
    }

    public IBootstrap WithAssemblyScanning<T>(Action<ITypeRegistrator> includeType = null) where T : IAssemblyMarker
    {
        var types = typeof(T).Assembly.GetTypes();
        var typeInclude = new TypeRegistrator(_container, types);
        typeInclude.Include<IModule>();
        includeType?.Invoke(typeInclude);
        return this;
    }
        
    public IBootstrap WithAppLevelExceptionHandling(Action<object, UnhandledExceptionEventArgs> handleException)
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, arg) => handleException(sender, arg);
        return this;
    }

    public void Start<T>(Func<T, Task> executionAction = null)
    {
        Parallel.ForEach(_container.ResolveMany<IModule>(), module =>
        {
            var serviceCollection = new ServiceCollection();
            module.Load(_container, serviceCollection);
            _container.Populate(serviceCollection);
        });
        executionAction?.Invoke(_container.Resolve<T>());
    }
}