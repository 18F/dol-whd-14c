using System.Web.Http;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.Api.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional },
                namespaces: new[] { "DOL.WHD.Section14c.Api.Areas.HelpPage.Controllers" }
            );

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}