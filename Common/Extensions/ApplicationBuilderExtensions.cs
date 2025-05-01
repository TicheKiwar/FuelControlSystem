using Common.Middlewares;

namespace Common.Extensions;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}