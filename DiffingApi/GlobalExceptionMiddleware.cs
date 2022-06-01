using Application.Exceptions;
using Common.Exceptions;
using Newtonsoft.Json;

namespace DiffingApi
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            Console.Error.WriteLine($"Server handled exception: {ex.Message}.{(ex.InnerException != null ? $" Inner Exception:{ex.InnerException.Message}" : "") }");

            context.Response.ContentType = "application/json";
            object response = null;
            var statusCode = StatusCodes.Status500InternalServerError;

            switch (ex)
            {
                case ValidationException validationEx:
                    response = new { 
                        Errors = validationEx.Messages 
                    };
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    break;
                case Base64ParseEncodeFailureException:
                    response = new {
                        Error = "Sent value is not base64 encoded"
                    };
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
            }

            context.Response.StatusCode = statusCode;
            context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            return Task.FromResult(context.Response);
        }
    }
}
