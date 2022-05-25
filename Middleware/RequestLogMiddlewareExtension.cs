using Microsoft.AspNetCore.Builder;
using MoviesApp.Middleware;

namespace Middleware
{
    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}