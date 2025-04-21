using Shared.ErrorModels;

namespace OnlineStore.MiddleWares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;
        public GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var respon = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };
                await context.Response.WriteAsJsonAsync(respon);
            }
        }
    }
}
