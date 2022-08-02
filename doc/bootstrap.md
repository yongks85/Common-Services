# Boot strapper

A structured way to add common application level features to new applications. <br/>
This was created with the initial intention to quickly add IoC container to any sort of application. <br/>
Often for desktop applications, it is done in hind sight resulting in hard to maintain code. Refactoring to implement IoC is also seldom done due to high risk and low in business value.
<br/>

Using [DryIoc](https://github.com/dadhi/DryIoc) as the IoC container, bootstrapper helps by having
- Modular registration declaration (To Doc)
- Assembly scanning for registration (To Test, Doc)

## How to use: TODO
- Add reference of Bootstrap.Core to the executing project
- For every project add reference to Bootstrap.Abstractions
- Implement assembly marker and Module interface
- Add registration to start up.

## Add ons
Addons|Description
|--|--|
Interception (To Test, Doc) | Wrapper of [Castle Dynamic Proxy AsyncInterceptor](https://github.com/JSkimming/Castle.Core.AsyncInterceptor) to allow easier interception registration
Serilog (To Test, Doc) | Wrap with interception for implicit logging using [Serilog](https://serilog.net/)
MsDi (To Test, Doc) | Extended with Microsoft Dependency Injection to use both at the same time
MultiStart (WIP) | Allows starting of services on top of main execution
MVVM (WIP) | Add MVVM toolkit registrations. <br/> Easier registration of views and viewModels
ArgParser (TODO) | Setup with [CommandLineParser](https://github.com/commandlineparser/commandline)
Scoping (TODO) | Add Scopes handling in bootstrapper

---

## To create your own extensions:
TODO