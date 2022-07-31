using Bootstrap.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap.MsDi;

/// <summary>
/// Interface for class registration with Microsoft Dependency Injection
/// </summary>
public interface IModuleWithMsDi: IModule
{
    /// <summary>
    /// Registration of classes
    /// </summary>
    /// <param name="services">Register using Microsoft Dependency injection</param>
    void Register(IServiceCollection services);
}