using Bootstrap.Abstraction;

using CommandLine;

using DryIoc;
using System;
using System.Threading.Tasks;

namespace Bootstrap.ArgParser;

/// <summary>
/// Argument Parser Library extenstion
/// </summary>
public static class Extension
{
    /// <summary>
    /// Start Program with argument parser
    /// </summary>
    public static ParserResult<TOptions> StartWithArgParser<TEntry, TOptions>(this IBootstrap bootstrap, string[] args, Action<TEntry>? executionAction = null, Action? noArgExecution = null)
    {
        var result = CommandLine.Parser.Default.ParseArguments<TOptions>(args);

        result = result.WithParsed(options =>
        {
            bootstrap.Register(reg => reg.RegisterInstance(options));
            bootstrap.Start(executionAction);
        });

        result = result.WithNotParsed(options =>
        {
            if (noArgExecution != null) noArgExecution();
            else bootstrap.Start(executionAction);
        });
        return result;
    }

    /// <summary>
    /// Start Program async with argument parser
    /// </summary>
    public static async Task<ParserResult<TOptions>> StartAsyncWithArgParser<TEntry, TOptions>(this IBootstrap bootstrap, string[] args, Func<TEntry, Task>? executionAction = null, Func<Task>? noArgExecution = null)
    {
        var result = CommandLine.Parser.Default.ParseArguments<TOptions>(args);

        result = await result.WithParsedAsync(async options =>
            {
                bootstrap.Register(reg => reg.RegisterInstance(options));
                await bootstrap.StartAsync(executionAction);

            });

       await result.WithNotParsedAsync(async options =>
       {
           if (noArgExecution != null) await noArgExecution();
           else await bootstrap.StartAsync(executionAction);
       });

       return result;
    }
}