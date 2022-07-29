using System;
using System.Collections.Generic;
using DryIoc;

namespace Bootstrap;

internal class PreLoads
{
    private readonly Dictionary<Type, dynamic> _dictionary = new();
    
    internal void Add<T>(Action<T> execution)
    {
        _dictionary.Add(typeof(T), execution);
    }

    internal void Execute(IResolver resolver)
    {
        foreach (var kvp in _dictionary)
        {
            var objects = resolver.ResolveMany(kvp.Key);
            foreach (var obj in objects)
            {
                kvp.Value.Invoke(obj);
            }
        }
    }
}