# Boot strapper

A structured way to add common application level features to new applications. <br/>
This was created with the initial intention to quickly add IoC container to any sort of application. Often for desktop applications, it is done in hind sight resulting in hard to maintain code. Refactoring to implement IoC is also seldom done due to high risk and low in business value.
<br/>

Using [DryIoc](https://github.com/dadhi/DryIoc) as the container, the code includes the following features:
- Modular registration declaration
- Mixing of Microsoft Dependency Injection extensions used by other libraries
- Code interception extensions
- Application level exception handling
- Assembly scanning for registration
- Starting multiple service with argument parser

## How to use:
- Add reference of Bootstrap.Core to the executing project
- For every project add reference to Bootstrap.Abstractions
- Implement assembly marker and Module interface
- Add registration to start up.

## Extensions
- Code intercepting logging via [Serilog](https://serilog.net/)

## To create your own extensions:
See Logging folder in the bootstrapper project as example