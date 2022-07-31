using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap.MsDi;

public static class Extension
{
    public static IBootstrap WithMicrosoftDependencyInjection(this IBootstrap bootstrap, IContainer container)
    {
        var decorator = new BootstrapWithMsDi(bootstrap, container);
        return decorator;
    }
}