using System;
using System.Threading.Tasks;
using System.Windows;
using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap.MVVM;

internal class BootstrapWithMvvm : IBootstrap
{
    private readonly IBootstrap _bootstrap;

    public BootstrapWithMvvm(IBootstrap bootstrap)
    {
        _bootstrap = bootstrap;
    }

    public void Start<T>(Action<T>? executionAction = null)
    {
        _bootstrap.Start<IResolver>(resolver =>
        {
            Application.Current.Resources.Add("ViewModelLocator", resolver.Resolve<ViewModelLocator>());
            executionAction?.Invoke(resolver.Resolve<T>());
        });
    }
    
    public void StartAsync<T>(Func<T, Task>? executionAction = null)
    {
        _bootstrap.StartAsync<IResolver>(resolver =>
        {
            Application.Current.Resources.Add("ViewModelLocator", resolver.Resolve<ViewModelLocator>());
            executionAction?.Invoke(resolver.Resolve<T>());
            return Task.CompletedTask;
        });
    }

    public IBootstrap HookAppLevelExceptionHandling(Action<object, UnhandledExceptionEventArgs> handleException) =>
        _bootstrap.HookAppLevelExceptionHandling(handleException);

    public IBootstrap ScanAssembly<T>(Action<ITypeRegistrator>? includeType = null) where T : IAssemblyMarker =>
        _bootstrap.ScanAssembly<T>(includeType);

    public IBootstrap Register(Action<IRegistrator> registration) => _bootstrap.Register(registration);
}