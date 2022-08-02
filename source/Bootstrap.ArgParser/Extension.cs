using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap.ArgParser;

public static class Extension
{
    //todo: Extend Arg parser error handling
    //todo: Code for
    //Parser.Default.ParseArguments<Options>(args)
    //               .WithParsed<Options>(o =>
    //               {
    //                   if (o.Verbose)
    //                   {
    //                       Console.WriteLine($"Verbose output enabled. Current Arguments: -v {o.Verbose}");
    //                       Console.WriteLine("Quick Start Example! App is in Verbose mode!");
    //                   }
    //                   else
    //                   {
    //                       Console.WriteLine($"Current Arguments: -v {o.Verbose}");
    //                       Console.WriteLine("Quick Start Example!");
    //                   }
    //               });

    //public static IBootstrap WithArgumentParser<T>(this IBootstrap bootstrap)
    //{
    //    bootstrap.Register(reg=> reg.RegisterDelegate<T>(_ => { }, Reuse.Singleton));
    //}
}