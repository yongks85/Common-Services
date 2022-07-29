using System;
using System.Threading.Tasks;

using DryIoc;

namespace Bootstrap.Abstraction;

/// <summary>
/// Application Bootstrapper Builder
/// </summary>
public interface IBootstrap 
{
    /// <summary>
    /// For loose or custom registration
    /// </summary>
    IBootstrap WithRegistration(Action<IRegistrator> registration);

    /// <summary>
    /// Scan the assembly of <see cref="IAssemblyMarker"/>
    /// Add registration of<see cref="IModule"/> 
    /// </summary>
    /// <param name="includeType">Any other type signature in assembly to register</param>
    /// <typeparam name="T">Assembly Marker type</typeparam>
    IBootstrap WithAssemblyScanning<T>(Action<ITypeRegistrator> includeType = null) where T : IAssemblyMarker;

    /// <summary>
    /// Add handling for application level exception
    /// </summary>
    IBootstrap WithAppLevelExceptionHandling(Action<object, UnhandledExceptionEventArgs> handleException);

    /// <summary>
    /// Register all modules and Start the application
    /// </summary>
    /// <param name="executionAction"></param>
    void Start(Func<IResolver, Task> executionAction = null);
}
