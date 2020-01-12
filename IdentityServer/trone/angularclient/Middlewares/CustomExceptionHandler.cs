using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Middlewares
{
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
            catch (Exception)
            {
                httpContext.Response.StatusCode = 500;
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

    public class NotFoundException : Exception { }
}
