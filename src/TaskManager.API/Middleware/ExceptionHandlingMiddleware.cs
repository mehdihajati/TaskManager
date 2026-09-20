using TaskManager.Application.Common.Exceptions;

namespace TaskManager.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ForbiddenException => StatusCodes.Status403Forbidden,
                ConflictException => StatusCodes.Status409Conflict,
                ValidationException => StatusCodes.Status400BadRequest,
                InvalidOperationException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
            var message = statusCode == StatusCodes.Status500InternalServerError ? "An unexpected error occurred" : ex.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new { message });
        }
    }
}
