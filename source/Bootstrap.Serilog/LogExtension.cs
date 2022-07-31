using System;
using Serilog;
using DryIoc;
using Bootstrap.Abstraction;
using Bootstrap.Interception;

namespace Bootstrap.Serilog;

/// <summary>
/// Logging extensions
/// </summary>
public static class LogExtension
{
    /// <summary>
    /// Add Logging to Bootstrapper
    /// </summary>
    public static IBootstrap WithLogging(this IBootstrap bootstrap, Action<LoggerConfiguration> setupLogging)
    {
        var config = new LoggerConfiguration();
        setupLogging(config);
        Log.Logger = config.CreateLogger();
        bootstrap.Register(register => register.Register<LogInterceptor>());
        return bootstrap;
    }

    /// <summary>
    /// Intercept type with logging
    /// </summary>
    public static void InterceptWithLogging<TService>(this IRegistrator registrator, object serviceKey = null) =>
        registrator.Intercept<TService, LogInterceptor>(serviceKey);

    /// <summary>
    /// Intercept type with logging
    /// </summary>s
    public static void InterceptWithLogging(this IRegistrator registrator, Type serviceType,
        object serviceKey = null) =>
        registrator.Intercept<LogInterceptor>(serviceType, serviceKey);
}