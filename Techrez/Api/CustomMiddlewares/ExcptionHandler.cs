using System.Threading.Tasks;
using Dal.BusinessExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Api.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException)
            {
                httpContext.Response.StatusCode = 404;
            }
            catch (DbUpdateException)
            {
                httpContext.Response.StatusCode = 400;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExcptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomeExcptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
