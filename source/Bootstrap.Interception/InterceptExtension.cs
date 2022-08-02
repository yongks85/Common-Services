using System;
using Castle.DynamicProxy;
using DryIoc;
using DryIoc.ImTools;

namespace Bootstrap.Interception;

/// <summary>
/// Extension for code interception in registration
/// </summary>
public static class InterceptExtension
{
    private static readonly DefaultProxyBuilder ProxyBuilder = new();

    /// <summary>
    /// Intercept particular class with particular interceptor
    /// </summary>
    public static void Intercept<TService, TInterceptor>(this IRegistrator registrator, object serviceKey = null)
        where TInterceptor : class, IInterceptor =>
        Intercept<TInterceptor>(registrator, typeof(TService), serviceKey);

    /// <summary>
    /// Intercept particular class with particular interceptor
    /// </summary>
    public static void Intercept<TInterceptor>(this IRegistrator registrator,
        Type serviceType, object serviceKey = null) where TInterceptor : class, IInterceptor
    {

        Type proxyType;
        if (serviceType.IsInterface)
        {
            proxyType = ProxyBuilder.CreateInterfaceProxyTypeWithTargetInterface(
                serviceType, ArrayTools.Empty<Type>(), ProxyGenerationOptions.Default);
        }
        else if (serviceType.IsClass)
        {
            proxyType = ProxyBuilder.CreateClassProxyTypeWithTarget(
                serviceType, ArrayTools.Empty<Type>(), ProxyGenerationOptions.Default);
        }
        else
        {
            throw new ArgumentException(
                $"Intercepted service type {serviceType} is not a supported as it is not a class nor an interface");
        }

        registrator.Register(serviceType, proxyType,
            made: Made.Of(pt => pt.PublicConstructors().FindFirst(ctor => ctor.GetParameters().Length != 0),
                Parameters.Of.Type<IInterceptor[]>(typeof(TInterceptor[]))),
            setup: DryIoc.Setup.DecoratorOf(useDecorateeReuse: true, decorateeServiceKey: serviceKey));
    }
}