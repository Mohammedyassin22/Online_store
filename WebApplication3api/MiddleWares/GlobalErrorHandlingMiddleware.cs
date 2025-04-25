using Azure;
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
                await HandlingNotFoundEndPoint(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandlingErrorAsync(context, ex);
            }
        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var respon = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message
            };
            respon.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                ValidationException=>HandlingValidationException((ValidationException) ex, respon),
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(respon);
        }

        private static async Task HandlingNotFoundEndPoint(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End point context request path is not found "
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
        private static int HandlingValidationException(ValidationException ex, ErrorDetails response)
        {
            response.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
