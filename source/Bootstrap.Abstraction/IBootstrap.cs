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
    IBootstrap Register(Action<IRegistrator> registration);

    /// <summary>
    /// Scan the assembly of <see cref="IAssemblyMarker"/> and add registration of <see cref="IModule"/> 
    /// </summary>
    /// <param name="includeType">Any other type signature in assembly to register</param>
    /// <typeparam name="T">Assembly Marker type</typeparam>
    IBootstrap ScanAssembly<T>(Action<ITypeRegistrator> includeType = null) where T : IAssemblyMarker;

    /// <summary>
    /// Add handling for application level exception
    /// </summary>
    IBootstrap HookAppLevelExceptionHandling(Action<object, UnhandledExceptionEventArgs> handleException);

    /// <summary>
    /// Register all modules and Start the application
    /// </summary>
    /// <param name="executionAction"></param>
    void StartAsync<T>(Func<T, Task> executionAction = null);
    
    /// <summary>
    /// Register all modules and Start the application
    /// </summary>
    /// <param name="executionAction"></param>
    void Start<T>(Action<T> executionAction = null);
}
