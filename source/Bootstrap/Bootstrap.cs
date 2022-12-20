﻿using System;
using System.Threading.Tasks;
using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap;

internal class Bootstrap : IBootstrap
{
    private readonly IContainer _container;

    internal Bootstrap(IContainer container)
    {
        _container = container;
    }

    public IBootstrap Register(Action<IRegistrator> registration)
    {
        registration(_container);
        return this;
    }

    public IBootstrap ScanAssembly<T>(Action<ITypeRegistrator> includeType = null) where T : IAssemblyMarker
    {
        var types = typeof(T).Assembly.GetTypes();
        var typeInclude = new TypeRegistrator(_container, types);
        typeInclude.Include<IModule>();

        includeType?.Invoke(typeInclude);
        return this;
    }

    public IBootstrap HookAppLevelExceptionHandling(Action<object, UnhandledExceptionEventArgs> handleException)
    {
        AppDomain.CurrentDomain.UnhandledException += (sender, arg) => handleException(sender, arg);
        return this;
    }

    public async Task StartAsync<T>(Func<T, Task> executionAction)
    {
        Parallel.ForEach(_container.ResolveMany<IModule>(), module =>
        {
            module.Register(_container);
        });
        await executionAction(_container.Resolve<T>());
    }

    public void Start<T>(Action<T> executionAction)
    {
        Parallel.ForEach(_container.ResolveMany<IModule>(), module =>
        {
            module.Register(_container);
        });

        if (executionAction == null) return;
        executionAction(_container.Resolve<T>());
    }
}