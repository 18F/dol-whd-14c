using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace DOL.WHD.Section14c.Api
{
    public static class WebApiConfig
    {
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
            //config.Filters.Add(new DOL.WHD.Section14c.Log.ActionFilters.AuthorizationRequiredAttribute());
        }

        //public static void RegisterNotFound(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "Error404",
        //        routeTemplate: "{*url}",
        //        defaults: new { controller = "Error", action = "Handle404" }
        //    );
        //}
    }
}
