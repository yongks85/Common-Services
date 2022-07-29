using System;
using System.Threading.Tasks;
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
    public static IBootstrap WithDispatcherExceptionHandling(this IBootstrap bootstrap,
        Action<object, DispatcherUnhandledExceptionEventArgs> handleException)
    {
        Application.Current.DispatcherUnhandledException += (sender, args) => handleException(sender, args);
        return bootstrap;
    }

    /// <summary>
    /// Add MVVM registration to IoC
    /// </summary>
    public static IBootstrap WithMvvm(this IBootstrap bootstrap)
    {
        bootstrap.WithRegistration(reg => reg.Register<IMessenger, WeakReferenceMessenger>());
        bootstrap.WithRegistration(reg => reg.Register<ViewModelLocator>(Reuse.Singleton));
        return bootstrap;
    }

    /// <summary>
    /// Add MVVM registration to IoC
    /// </summary>
    public static IBootstrap StartMvvm<T>(this IBootstrap bootstrap, Func<T, Task>? executionAction = null)
    {
        bootstrap.Start<IResolver>(resolver =>
        {
            Application.Current.Resources.Add("ViewModelLocator", resolver.Resolve<ViewModelLocator>());
            executionAction?.Invoke(resolver.Resolve<T>());
            return Task.CompletedTask;
        });

        return bootstrap;
    }

    /// <summary>
    /// Register views to IoC
    /// </summary>
    public static ITypeRegistrator IncludeViews(this ITypeRegistrator registrator, IReuse? reuse = null)
    {
        registrator.Include<IView>(reuse);
        return registrator;
    }

    /// <summary>
    /// Register ViewModel to IoC
    /// </summary>
    public static ITypeRegistrator IncludeViewModels(this ITypeRegistrator registrator, IReuse? reuse = null)
    {
        registrator.Include<IViewModel>(reuse);
        return registrator;
    }

}