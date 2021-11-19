using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DDDExample.Api.Middlewares
{
    public class HandleExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandleExceptionsMiddleware> _logger;

        public HandleExceptionsMiddleware(RequestDelegate next, ILogger<HandleExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                LogRequest(context.Request);
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await WriteResponseAsync(context.Response, e);
            }
        }

        private async Task WriteResponseAsync(HttpResponse response, Exception e)
        {
            response.Headers.ContentType = "application/json";
            response.StatusCode = 500;
            await response.WriteAsJsonAsync(new { e.Message }).ConfigureAwait(false);
        }

        private void LogRequest(HttpRequest request)
        {
            var message = $"{request.Method.ToUpper()} {request.Path} Headers={JsonSerializer.Serialize(request.Headers)}";
            _logger.LogTrace(message);
        }
    }
}
