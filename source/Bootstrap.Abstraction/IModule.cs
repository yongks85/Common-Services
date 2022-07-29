using DryIoc;
using Microsoft.Extensions.DependencyInjection;

namespace Bootstrap.Abstraction;

/// <summary>
/// Interface for class registration
/// </summary>
public interface IModule
{
    /// <summary>
    /// Registration of classes
    /// </summary>
    /// <param name="registrator">Register using DryIoc</param>
    /// <param name="services">Register using Microsoft Dependency injection</param>
    void Load(IRegistrator registrator, IServiceCollection services);
}