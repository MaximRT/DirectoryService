﻿using Shared;

namespace Presentation.EndpointsResult;

public class ErrorsResult : IResult
{
    private readonly Errors _errors;

    public ErrorsResult(Error error)
    {
        _errors = error.ToErrors();
    }

    public ErrorsResult(Errors errors)
    {
        _errors = errors;
    }



    public Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        if (!_errors.Any())
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return httpContext.Response.WriteAsJsonAsync(Envelope.Error(_errors));
        }

        var distinctErrorsTypes = _errors
            .Select(x => x.Type)
            .Distinct()
            .ToList();

        int statusCode = distinctErrorsTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeForErrorType(distinctErrorsTypes.First());

        var envelope = Envelope.Error(_errors);
        httpContext.Response.StatusCode = statusCode;

        return httpContext.Response.WriteAsJsonAsync(envelope);
    }

    private static int GetStatusCodeForErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.VALIDATION => StatusCodes.Status400BadRequest,
            ErrorType.NOT_FOUND => StatusCodes.Status404NotFound,
            ErrorType.CONFLICT => StatusCodes.Status409Conflict,
            ErrorType.FAILURE => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
}