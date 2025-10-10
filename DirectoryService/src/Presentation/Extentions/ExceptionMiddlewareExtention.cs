using Presentation.Middlewares;

namespace Presentation.Extentions;

public static class ExceptionMiddlewareExtention
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionMiddleware>();
}