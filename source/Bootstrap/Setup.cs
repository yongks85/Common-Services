using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap;

/// <summary>
/// Bootstrap Entry class
/// </summary>
public static class Setup
{
    /// <summary>
    /// Instantiate and start a new configuration
    /// </summary>
    public static IBootstrap Using(IContainer container) => new Bootstrap(container);
}