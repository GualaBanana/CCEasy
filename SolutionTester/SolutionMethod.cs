﻿using System.Reflection;

namespace CCHelper;

internal abstract class SolutionMethod
{
    protected MethodInfo _method;
    readonly object _solutionContainer;
    object?[]? _arguments;
    protected Type? _resultType;

    internal object?[]? Arguments 
    { 
        get => _arguments;
        set
        {
            if (value is not null
                && !_method.GetParameters()
                          .Any(parameter => parameter.ParameterType.IsAssignableFrom(
                              value[parameter.Position]?.GetType())
                          ))
            {
                throw new ArgumentException("Types of provided arguments do not match the required parameters.", nameof(value));
            }
            _arguments = value;
        }
    }
    protected object? Result { get; set; }
    internal abstract Type ResultType { get; }

    internal SolutionMethod(MethodInfo method, object solutionContainer)
    {
        _method = method;
        _solutionContainer = solutionContainer;
    }
    
    internal object? Invoke()
    {
        var rawResult = _method.Invoke(_solutionContainer, Arguments);
        ProcessResult(rawResult);
        return Result;
    }
    protected abstract void ProcessResult(object? rawResult);
}
