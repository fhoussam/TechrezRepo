using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EnvironmentExtensions;

namespace angularclient.Filters
{
    public class CustomAntiForgeryAttribute : Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var antiforgery = (IAntiforgery)serviceProvider.GetService(typeof(IAntiforgery));
            var webHostEnvironment = (IWebHostEnvironment)serviceProvider.GetService(typeof(IWebHostEnvironment));
            return new InternalAddHeaderFilter(antiforgery, webHostEnvironment);
        }

        private class InternalAddHeaderFilter : IResourceFilter
        {
            private readonly IAntiforgery _antiforgery;
            private readonly IWebHostEnvironment _webHostEnvironment;
            public InternalAddHeaderFilter(IAntiforgery antiforgery, IWebHostEnvironment webHostEnvironment)
            {
                _antiforgery = antiforgery;
                _webHostEnvironment = webHostEnvironment;
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {
                bool isJwtClient = false;
                //((HttpRequestHeaders)context.HttpContext.Request.Headers).HeaderAuthorization.ToString().StartsWith("Bearer ");
                if (!_webHostEnvironment.IsFrontDevMode() && !isJwtClient)
                {
                    bool isRequestValid = _antiforgery.IsRequestValidAsync(context.HttpContext).Result; //to remove later
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
