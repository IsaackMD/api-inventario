using System.Net;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using MyInventoryApp.src.Domain.Exceptions;

namespace MyInventoryApp.src.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogWarning(exception, "An exception occurred after the response started.");
                ExceptionDispatchInfo.Capture(exception).Throw();
            }

            var statusCode = GetStatusCode(exception);

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception, "Unhandled exception processing {Method} {Path}. TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.TraceIdentifier);
            }
            else
            {
                _logger.LogWarning(exception, "Handled exception processing {Method} {Path}. TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.TraceIdentifier);
            }

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse(
                context.Response.StatusCode,
                GetTitle(statusCode),
                GetDetail(exception, statusCode),
                context.TraceIdentifier,
                context.Request.Path);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
        }

        private static HttpStatusCode GetStatusCode(Exception exception)
            => exception switch
            {
                DomainException => HttpStatusCode.BadRequest,
                ArgumentException => HttpStatusCode.BadRequest,
                InvalidOperationException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

        private static string GetTitle(HttpStatusCode statusCode)
            => statusCode switch
            {
                HttpStatusCode.BadRequest => "Bad Request",
                HttpStatusCode.Unauthorized => "Unauthorized",
                HttpStatusCode.NotFound => "Not Found",
                _ => "Internal Server Error"
            };

        private string GetDetail(Exception exception, HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.InternalServerError || _environment.IsDevelopment())
            {
                return exception.Message;
            }

            return "An unexpected error occurred.";
        }

        private sealed record ErrorResponse(
            int StatusCode,
            string Title,
            string Detail,
            string TraceId,
            string Path);
    }
}
