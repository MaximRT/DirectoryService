﻿using System.Collections;

namespace Shared;

public class Errors : IEnumerable<Error>
{
    private readonly List<Error> _errors;

    public Errors(IEnumerable<Error> errors)
    {
        _errors = [..errors];
    }

    public static implicit operator Errors(List<Error> errors) => new Errors(errors);

    public static implicit operator Errors(Error error) => new Errors([error]);

    public IEnumerator<Error> GetEnumerator() => _errors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}