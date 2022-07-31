using DryIoc;
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
    void Register(IRegistrator registrator);
}