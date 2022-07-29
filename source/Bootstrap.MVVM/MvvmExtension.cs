using Bootstrap.Abstraction;

namespace Bootstrap.MVVM;

public static class MvvmExtension
{
    /// <summary>
    /// Add Logging to Bootstrapper
    /// </summary>
    public static IBootstrap WithExceptionHandling(this IBootstrap bootstrap)
    {
        // TODO: Hook exception
        return bootstrap;
    }

    /// <summary>
    /// Add Logging to Bootstrapper
    /// </summary>
    public static IBootstrap WithMvvm(this IBootstrap bootstrap)
    {
        //TODO: Register messenger
        return bootstrap;
    }

    //TODO: include type IViewModel and IView?


    /**
     Helper class to do this:
        Current.Resources.Add("ViewModelLocator", new ViewModelLocator(scope.ServiceProvider));
        Current.MainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
        Current.MainWindow.Show();
     * **/
}