using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Pomodoro.WebApi.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
        )
    {
        _logger.LogError(exception, "An unhandled exception occured: {Message}", exception.Message);
        var (statusCode, title, errors) = exception switch
        {
            ValidationException validationException => (
                StatusCodes.Status400BadRequest,
                "",
                ExtractValidationErrors(validationException)
            ),
            InvalidOperationException => (
                StatusCodes.Status400BadRequest,
                "Bad Request",
                null
            ),
            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                "Not Found",
                null
            ),
            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                null
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Server Error",
                null
            )
        };
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };
        if (errors is not null)
            problemDetails.Extensions.Add("errors", errors);
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private static Dictionary<string, string[]> ExtractValidationErrors(ValidationException exception)
    {
        return exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
                );
    }
}