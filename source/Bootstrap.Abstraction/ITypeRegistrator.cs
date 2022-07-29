using System;
using DryIoc;

namespace Bootstrap.Abstraction;

/// <summary>
/// Add registration of specific type found in assembly
/// 
/// </summary>
public interface ITypeRegistrator
{
    /// <summary>
    /// Add type to register from assembly scanning
    /// </summary>
    /// <param name="addExecution">execution parameters</param>
    ITypeRegistrator Include<TType>(IReuse reuse = null); 
        
}