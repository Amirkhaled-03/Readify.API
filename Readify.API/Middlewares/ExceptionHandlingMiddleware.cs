using Readify.API.HandleResponses;
using System.Net;
using System.Text.Json;

namespace Readify.API.Middlewares
{

    /// <summary>
    /// Middleware to handle exceptions globally.
    /// Captures unhandled exceptions, logs them, and provides a consistent API error response format.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        /// <summary>
        /// Middleware pipeline method to catch exceptions and handle them gracefully.
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Process the next middleware in the pipeline.
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the error with its details.
                _logger.LogError(ex, "An unhandled exception occurred");

                // Handle the exception and return an appropriate response.
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Processes the exception and sends a formatted JSON response.
        /// </summary>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set response content type to JSON.
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Prepare the response based on the environment.
            var response = _environment.IsDevelopment()
                ? new ApiExceptionResponse(
                    (int)HttpStatusCode.InternalServerError,
                    exception.Message,
                    exception.StackTrace?.ToString())
                : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, "An unexpected error occurred");

            // Configure the JSON serializer options.
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // Serialize the response to JSON.
            var json = JsonSerializer.Serialize(response, options);

            // Write the JSON to the response body.
            await context.Response.WriteAsync(json);
        }
    }
}
