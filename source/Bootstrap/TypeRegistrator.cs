using System;
using System.Collections.Generic;
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

    public bool PublicClassesOnly { set; get; }

    public bool IncludeRegistrationOfClassType { set; get; }

    public IReuse ObjectLifeCycle { set; get; }

    public IList<Type> SubTypesToRegister { get; } = new List<Type>();

    public void Include<T>()
    {
        var classes = _allTypes.Where(type => typeof(T).IsAssignableFrom(type)
                                            && type.IsClass
                                            && !type.IsAbstract);

        if (PublicClassesOnly) classes = classes.Where(type => type.IsPublic);

        foreach (var module in classes)
        {
            _container.Register(typeof(T), module, ObjectLifeCycle);
            if (IncludeRegistrationOfClassType) _container.Register(module, module, ObjectLifeCycle);
            foreach(var subType in SubTypesToRegister.Where(st=> st.IsAssignableFrom(module)))
            {
                _container.Register(subType, module, ObjectLifeCycle);
            }

        }
        return this;
    }
}