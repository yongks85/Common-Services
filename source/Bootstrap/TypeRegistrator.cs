using System;
using System.Linq;
using Bootstrap.Abstraction;
using DryIoc;

namespace Bootstrap;

internal class TypeRegistrator : ITypeRegistrator
{
    private readonly IContainer _container;
    private readonly Type[] _allTypes;
    
    internal TypeRegistrator(IContainer container, Type[] allTypes)
    {
        _container = container;
        _allTypes = allTypes;
    }

    public ITypeRegistrator Include<TType>(IReuse reuse = null) 
    {
        var modules = _allTypes.Where(type => typeof(TType).IsAssignableFrom(type)
                                              && type.IsPublic
                                              && type.IsClass
                                              && !type.IsAbstract);

        foreach (var module in modules)
        {
            _container.Register(typeof(TType), module, reuse);
            _container.Register(module, reuse);
        }
        return this;
    }
}