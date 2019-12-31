using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace angularclient
{
    public class CustomAntiForgeryAttributeAttribute : Attribute, IFilterFactory
    {
        // Implement IFilterFactory
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var antiforgery = (IAntiforgery)serviceProvider.GetService(typeof(IAntiforgery));
            return new InternalAddHeaderFilter(antiforgery);
        }

        private class InternalAddHeaderFilter : IResourceFilter
        {
            private readonly IAntiforgery _antiforgery;
            public InternalAddHeaderFilter(IAntiforgery antiforgery)
            {
                _antiforgery = antiforgery;
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {
                //bool isJwtClient = ((HttpRequestHeaders)context.HttpContext.Request.Headers).HeaderAuthorization.ToString().StartsWith("Bearer ");
                bool isJwtClient = true;
                bool isChallengeRequest = context.HttpContext.Request.Path == "/Default/ChallengeUser";
                if (!isJwtClient && !isChallengeRequest)
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