namespace MyInventoryApp.src.API.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionHandlingMiddleware>();

        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
            => app.UseMiddleware<SecurityHeadersMiddleware>();
    }
}
