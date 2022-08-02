// See https://aka.ms/new-console-template for more information

using DryIoc;
using Setup = Bootstrap.Setup;

var container = new Container();

// Setup.UsingContainer()
Setup.Using(container)
    .Register(reg => reg.Register<SomeInterface, SomeClass>())
    .ScanAssembly<Bootstrap.SampleModule.IMockAssemblyMarker>(typeReg =>
    {
        typeReg.PublicClassesOnly = true;
        typeReg.ObjectLifeCycle = Reuse.Singleton;
        typeReg.IncludeRegistrationOfClassType = false;
    })
    .HookAppLevelExceptionHandling(ErrorHandling)
    .Start<SomeInterface>(resolved => resolved.Run());


void ErrorHandling(object o, UnhandledExceptionEventArgs args)
{
    Console.WriteLine((Exception) args.ExceptionObject);
}

public interface SomeInterface
{
    void Run();
}

public class SomeClass: SomeInterface
{
    public void Run() {}
}