using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DOL.WHD.Section14c.PdfApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "PDF Help Area",
                "",
                new { controller = "PdfApiHelp", action = "Index" },
                namespaces: new[] { "DOL.WHD.Section14c.PdfApi.Areas.HelpPage.Controllers" }
            ).DataTokens = new RouteValueDictionary(new { area = "HelpPage" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
