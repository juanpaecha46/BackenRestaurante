using AuthService.Domain.Exceptions;
using FluentValidation;

namespace AuthService.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (statusCode, message) = ex switch
        {
            InvalidCredentialsException => (StatusCodes.Status401Unauthorized, ex.Message),
            UserNotFoundException => (StatusCodes.Status404NotFound, ex.Message),
            EmailAlreadyExistsException => (StatusCodes.Status409Conflict, ex.Message),
            ValidationException ve => (StatusCodes.Status400BadRequest,
                string.Join(" | ", ve.Errors.Select(e => e.ErrorMessage))),
            _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new { error = message, statusCode });
    }
}
