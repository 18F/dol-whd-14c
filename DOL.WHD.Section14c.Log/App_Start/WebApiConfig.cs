
using DOL.WHD.Section14c.Log.LogHelper;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace DOL.WHD.Section14c.Log
{
    public static class WebApiConfig
    {
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
        }
    }
}
