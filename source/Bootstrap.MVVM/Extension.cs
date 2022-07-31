using System;
using System.Windows;
using System.Windows.Threading;
using Bootstrap.Abstraction;
using CommunityToolkit.Mvvm.Messaging;
using DryIoc;

namespace Bootstrap.MVVM;

public static class Extension
{
    /// <summary>
    /// Add Dispatcher exception handling
    /// </summary>
    public static IBootstrap HookDispatcherExceptionHandling(this IBootstrap bootstrap,
        Action<object, DispatcherUnhandledExceptionEventArgs> handleException)
    {
        Application.Current.DispatcherUnhandledException += (sender, args) => handleException(sender, args);
        return bootstrap;
    }
    
    public static IBootstrap WithMvvm(this IBootstrap bootstrap)
    {
        var decorator = new BootstrapWithMvvm(bootstrap);
        decorator.Register(reg => reg.Register<IMessenger, WeakReferenceMessenger>());
        decorator.Register(reg => reg.Register<ViewModelLocator>(Reuse.Singleton));
        return decorator;
    }
    
    /// <summary>
    /// Register views to IoC
    /// </summary>
    public static ITypeRegistrator IncludeViews(this ITypeRegistrator registrator, IReuse? reuse = null)
    {
        registrator.ObjectLifeCycle = reuse;
        registrator.Include<IView>();
        return registrator;
    }

    /// <summary>
    /// Register ViewModel to IoC
    /// </summary>
    public static ITypeRegistrator IncludeViewModels(this ITypeRegistrator registrator, IReuse? reuse = null)
    {
        registrator.ObjectLifeCycle = reuse;
        registrator.Include<IViewModel>();
        return registrator;
    }

}