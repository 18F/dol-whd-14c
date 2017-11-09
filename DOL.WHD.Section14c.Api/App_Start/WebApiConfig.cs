using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.ExceptionHandling;
using DOL.WHD.Section14c.Log.LogHelper;

namespace DOL.WHD.Section14c.Api
{
    /// <summary>
    /// API web config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers API web config
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Enable Cors Support
            config.EnableCors();
            
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new DOL.WHD.Section14c.Log.ActionFilters.LoggingFilterAttribute());
            config.Filters.Add(new DOL.WHD.Section14c.Log.ActionFilters.GlobalExceptionAttribute());
            
        }

        
    }
}
