using System.Web.Http;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.Log.Areas.HelpPage
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
                "LogHelpPage_Default",
                "LogHelp/{action}/{apiId}",
                new { controller = "LogHelp", action = "Index", apiId = UrlParameter.Optional },
                namespaces: new[] { "DOL.WHD.Section14c.Log.Areas.HelpPage.Controllers" }
                );

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}