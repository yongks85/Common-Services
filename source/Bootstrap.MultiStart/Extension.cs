using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap.MultiStart;

public static class Extension
{
    public static IBootstrap WithMultiStart(this IBootstrap bootstrap)
    {
        var decorator = new BootstrapWithMultiStart(bootstrap);
        return decorator;
    }
}