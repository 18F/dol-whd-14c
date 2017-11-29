
using DOL.WHD.Section14c.Log.LogHelper;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace DOL.WHD.Section14c.Log
{
    /// <summary>
    /// Log API config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the log API handlers
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

            NLog.LayoutRenderers.LayoutRenderer.Register("api-log", typeof(DOL.WHD.Section14c.Log.Helpers.LogLayoutRenderer));
            config.Filters.Add(new DOL.WHD.Section14c.Log.ActionFilters.LoggingFilterAttribute());
            config.Filters.Add(new DOL.WHD.Section14c.Log.ActionFilters.GlobalExceptionAttribute());
        }
    }
}
