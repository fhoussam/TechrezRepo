using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) 
        {
            try
            {
                await _next(httpContext);
            }
            catch (DbUpdateException) 
            {
                httpContext.Response.StatusCode = 400;
            }
            catch (Exception exp)
            {
                throw;
            }
        }
    }

    public static class ExceptionMiddlewareExtension 
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder applicationBuilder) 
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
            return applicationBuilder;
        }
    }
}
