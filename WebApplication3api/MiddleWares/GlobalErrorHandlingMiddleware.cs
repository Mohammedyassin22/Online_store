using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
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
                if(context.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode=StatusCodes.Status404NotFound,
                        ErrorMessage=$"End point context request path is not found "
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
             
                context.Response.ContentType = "application/json";
                var respon = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message
                };
                respon.StatusCode = ex switch
                {
                    NotFoundException=>StatusCodes.Status404NotFound,
                    _ =>StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(respon);
            }
        }
    }
}
