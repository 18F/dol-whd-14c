﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DOL.WHD.Section14c.Log.ActionFilters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string Token = "Token";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var correlationId = Guid.NewGuid().ToString();
            //  Get API key provider
            //var provider = filterContext.ControllerContext.Configuration
            //    .DependencyResolver.GetService(typeof(ITokenServices)) as ITokenServices;


            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();

                // Validate Token
                //    if (provider != null && !provider.ValidateToken(tokenValue))
                //    {
                //        var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                //        filterContext.Response = responseMessage;
                //    }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }



            base.OnActionExecuting(filterContext);

        }
        
    }
}