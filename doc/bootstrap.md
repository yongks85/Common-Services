# Boot strapper

A structured way to add common application level features to new applications. <br/>
This was created with the initial intention to quickly add IoC container to any sort of application. <br/>
Often for desktop applications, it is done in hind sight resulting in hard to maintain code. Refactoring to implement IoC is also seldom done due to high risk and low in business value.
<br/>

Using [DryIoc](https://github.com/dadhi/DryIoc) as the IoC container, bootstrapper helps by having
- Modular registration declaration
- Assembly scanning for registration

<br/>

## How to use:
To allow adding registration and resolving the object to execute, start by adding the following to the start of program
```csharp
Setup.UsingContainer()
    .Register(reg =>
    {
        reg.Register<SomeInterface, SomeClass>()
    })
    .Start<SomeInterface>(resolved => resolved.Run());
```

<br/>

As your program increase in features and functionality, you can use the assembly scanning functionality. <br/>
Start by adding a interface inheriting `IAssemblyMarker` in each project you wish the assembly scanning to happen. <br/>
The scanning will register classes created inheriting `IModule`

```csharp
Setup.UsingContainer()
    .ScanAssembly<Bootstrap.SampleModule.ISampleAssemblyMarker>()
    .Start<SomeInterface>(resolved => resolved.Run());

internal class SampleModule : IModule
{
    public void Register(IRegistrator registrator)
    {
        registrator.Register<SomeInterface, SomeClass>();
    }
}

public interface ISampleAssemblyMarker : IAssemblyMarker { }
```

<br/>

You can add your own registrations of the same type as well as options control like so: <br/>
*Note: This would not affect the `IModule` registration*
```csharp
ScanAssembly<Bootstrap.SampleModule.IMockAssemblyMarker>(typeReg =>
{
    typeReg.PublicClassesOnly = true;
    typeReg.ObjectLifeCycle = Reuse.Singleton;
    typeReg.IncludeRegistrationOfClassType = false;
    typeReg.Include<CommonInterface>();
})
```

Convenience method to add application exception handling can be done using `HookAppLevelExceptionHandling`. <br/>
You can also start asynchronously by using `StartAsync<T>`

<br/>

---

## Add ons

Addons|Description
|--|--|
[Code Interception](#code-interception) | Wrapper of [Castle Dynamic Proxy AsyncInterceptor](https://github.com/JSkimming/Castle.Core.AsyncInterceptor) to allow easier interception registration
Serilog (To Test, Doc) | Using code interception for implicit logging using [Serilog](https://serilog.net/)
MsDi (To Test, Doc) | Extended with Microsoft Dependency Injection to use both at the same time
MultiStart (WIP) | Allows starting of services on top of main execution
MVVM (WIP) | Add MVVM toolkit registrations. <br/> Easier registration of views and viewModels
ArgParser (TODO) | Setup with [CommandLineParser](https://github.com/commandlineparser/commandline)
Scoping (TODO) | Add Scopes handling in bootstrapper

<br/>

### Code Interception
[DryIoc code interception](https://github.com/dadhi/DryIoc/blob/master/docs/DryIoc.Docs/Interception.md) can be used by adding reference to Interception package and inherit `Interceptor` for your interception classes.
You can see the code for Serilog on how to use the generic wrapper,


---