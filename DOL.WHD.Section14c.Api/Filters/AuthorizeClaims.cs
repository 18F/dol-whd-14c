using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DOL.WHD.Section14c.Api.Filters
{
    /// <summary>
    /// Attribute to authorize claims made on an API route
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeClaimsAttribute : AuthorizeAttribute
    {

        private string[] UserClaims { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthorizeClaimsAttribute()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="claims">
        /// The claims being made
        /// </param>
        public AuthorizeClaimsAttribute(params string[] claims)
        {
            UserClaims = claims;
        }

        /// <summary>
        /// Authorization handler. Throws an exception or sets
        /// the context's response to an error if the request
        /// is not authorized.
        /// </summary>
        /// <param name="actionContext"></param>
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

        /// <summary>
        /// Handle a request that isn't authorized
        /// </summary>
        /// <param name="actionContext"></param>
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