using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DOL.WHD.Section14c.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeClaimsAttribute : AuthorizeAttribute
    {

        private string[] UserClaims { get; set; }

        public AuthorizeClaimsAttribute()
        { }

        public AuthorizeClaimsAttribute(params string[] claims)
        {
            UserClaims = claims;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (UserClaims == null || !UserClaims.Any()) return;

            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            if (!CheckIfClaimExists(actionContext))
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        private bool CheckIfClaimExists(HttpActionContext httpContext)
        {
            var identity = ClaimsPrincipal.Current.Identities.First();
            return identity != null && identity.Claims.Any(x => UserClaims.Contains(x.Type));
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent("Access is denied.")
            };

        }
    }
}