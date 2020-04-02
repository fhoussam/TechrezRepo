using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace angular.Common
{
    public class CustomAntiForgeryAttribute : Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var antiforgery = (IAntiforgery)serviceProvider.GetService(typeof(IAntiforgery));
            var webHostEnvironment = (IWebHostEnvironment)serviceProvider.GetService(typeof(IWebHostEnvironment));
            var httpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));
            return new InternalAddHeaderFilter(antiforgery, webHostEnvironment, httpContextAccessor);
        }

        private class InternalAddHeaderFilter : IResourceFilter
        {
            private readonly IAntiforgery _antiforgery;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public InternalAddHeaderFilter(IAntiforgery antiforgery, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
            {
                _antiforgery = antiforgery;
                _webHostEnvironment = webHostEnvironment;
                _httpContextAccessor = httpContextAccessor;
            }

            public void OnResourceExecuting(ResourceExecutingContext context)
            {
                var isJwtClient = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer ");
                if (!isJwtClient)// && !_webHostEnvironment.IsFrontDevMode())
                {
                    bool isRequestValid = _antiforgery.IsRequestValidAsync(context.HttpContext).Result; 
                    if (!isRequestValid)
                        throw new AntiforgeryValidationException(string.Empty);
                    //_antiforgery.ValidateRequestAsync(context.HttpContext); //no idea why this is not throwing an exception when NO token is provided
                }
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
