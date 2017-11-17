using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DOL.WHD.Section14c.EmailApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Enable Cors Support
            config.EnableCors();

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
