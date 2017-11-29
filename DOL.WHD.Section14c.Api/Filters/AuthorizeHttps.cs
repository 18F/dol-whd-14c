using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using DOL.WHD.Section14c.Common;

namespace DOL.WHD.Section14c.Api.Filters
{
    /// <summary>
    /// Authorization attribute to enforce HTTPS if the app
    /// is configured for that
    /// </summary>
    public class AuthorizeHttps : AuthorizeAttribute
    {
        /// <summary>
        /// Enforce HTTPS as configured
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps  && AppSettings.Get<bool>("RequireHttps"))
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "HTTPS Required"
                };
            }
            else
            {
                if (SkipAuthorization(actionContext))
                    return;

                base.OnAuthorization(actionContext);
            }
        }

        // from AuthorizeAttribute
        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            if (!actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>())
                return actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>();
            return true;
        }
    }
}