using Favorite.Products.Application.Constants;

namespace Favorite.Products.API.Middleware
{
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
                _logger.LogError(ex, MessagesConstants.MiddlewareExceptionUnhandled);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = StatusCodes.Status500InternalServerError;
            var message = MessagesConstants.MiddlewareException;

            if (exception is ArgumentException)
            {
                code = StatusCodes.Status400BadRequest;
                message = exception.Message;
            }

            if (exception is InvalidOperationException)
            {
                code = StatusCodes.Status400BadRequest;
                message = exception.Message;
            }

            var response = new
            {
                error = message,
                statusCode = code
            };

            context.Response.ContentType = MessagesConstants.MiddlewareJson;
            context.Response.StatusCode = code;

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}