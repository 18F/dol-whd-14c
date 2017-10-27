using System.Web.Mvc;
using System.Web.Routing;

namespace DOL.WHD.Section14c.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Help Area",
                "",
                new { controller = "Help", action = "Index" },
                namespaces: new[] { "DOL.WHD.Section14c.Api.Areas.HelpPage.Controllers" }
            ).DataTokens = new RouteValueDictionary(new { area = "HelpPage" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
