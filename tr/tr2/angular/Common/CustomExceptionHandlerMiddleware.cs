using app.Common.Exceptions;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace angular.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Failures);
                    break;

                case AntiforgeryValidationException _:
                    code = HttpStatusCode.BadRequest;
                    result = "An error occurred while processing your request";
                    break;

                case DbUpdateException _:
                    code = HttpStatusCode.BadRequest;
                    var errorObject = new 
                    { 
                        error = "An error occurred while updating the entries" 
                    };
                    result = JsonConvert.SerializeObject(errorObject);
                    break;

                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
