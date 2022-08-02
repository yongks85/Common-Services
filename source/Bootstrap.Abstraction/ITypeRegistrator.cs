using System;
using System.Collections.Generic;

using DryIoc;

namespace Bootstrap.Abstraction;

/// <summary>
/// Add registration of specific type found in assembly
/// 
/// </summary>
public interface ITypeRegistrator
{
    /// <summary>
    /// Only register types that is public classes, default is false
    /// </summary>
    bool PublicClassesOnly { set; }

    /// <summary>
    /// include registeration of concrete class type. does not include inheriting types
    /// </summary>
    bool IncludeRegistrationOfClassType { set; }

    /// <summary>
    /// object life cycle, default is transient
    /// </summary>
    IReuse ObjectLifeCycle { set; }

    /// <summary>
    /// Subtype of register if the classes is also the specified type.
    /// </summary>
    IList<Type> SubTypesToRegister { get; }

    /// <summary>
    /// Add type to register from assembly scanning. MUST BE NON ABSTRACT CLASS!
    /// </summary>
    void Include<T>();

}