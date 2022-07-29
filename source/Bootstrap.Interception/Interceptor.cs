using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Bootstrap.Interception;

/// <summary>
/// Parent Interceptor class for DryIoc
/// </summary>
[DebuggerStepThrough]
public abstract class Interceptor : IAsyncInterceptor, IInterceptor
{
    public void InterceptSynchronous(IInvocation invocation)
    {
        PreMethodProcess(invocation);
        TryCatchWrapper(invocation, invocation.Proceed);
        PostMethodProcess(invocation);
    }

    public void InterceptAsynchronous(IInvocation invocation) =>
        invocation.ReturnValue = InternalInterceptAsynchronous(invocation);

    public void InterceptAsynchronous<TResult>(IInvocation invocation) =>
        invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);

    public void Intercept(IInvocation invocation) => this.ToInterceptor().Intercept(invocation);

    /// <summary>
    /// Code logic to execute before the method is executed
    /// </summary>
    protected abstract void PreMethodProcess(IInvocation invocation);

    /// <summary>
    /// Code logic to execute after the method is executed
    /// </summary>
    /// <param name="invocation"></param>
    protected abstract void PostMethodProcess(IInvocation invocation);

    /// <summary>
    /// Code Logic to execute if method throws a exception
    /// </summary>
    /// <param name="invocation"></param>
    /// <param name="exception"></param>
    protected abstract void ExceptionProcess(IInvocation invocation, Exception exception);

    private async Task InternalInterceptAsynchronous(IInvocation invocation)
    {
        PreMethodProcess(invocation);

        await TryCatchWrapper(invocation, async () =>
        {
            invocation.Proceed();
            var task = (Task)invocation.ReturnValue;
            await task;
        });

        PostMethodProcess(invocation);
    }

    private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
    {
        PreMethodProcess(invocation);

        var result = await TryCatchWrapper(invocation, async () =>
        {
            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            return await task;
        });

        PostMethodProcess(invocation);

        return result;
    }

    private T TryCatchWrapper<T>(IInvocation invocation, Func<T> func)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            ExceptionProcess(invocation, e);
            throw;
        }
    }

    private void TryCatchWrapper(IInvocation invocation, Action func)
    {
        try
        {
            func();
        }
        catch (Exception e)
        {
            ExceptionProcess(invocation, e);
            throw;
        }
    }
}