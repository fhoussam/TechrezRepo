using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EnvironmentExtensions;
using Microsoft.AspNetCore.Http;

namespace angularclient.Filters
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

            public void OnResourceExecuted(ResourceExecutedContext context)
            {
                var isJwtClient = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer ");
                if (!_webHostEnvironment.IsFrontDevMode() && !isJwtClient)
                {
                    //bool isRequestValid = _antiforgery.IsRequestValidAsync(context.HttpContext).Result; //just a showing off a trick here, nothing more
                    _antiforgery.ValidateRequestAsync(context.HttpContext);
                }
            }

            public void OnResourceExecuting(ResourceExecutingContext context)
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
