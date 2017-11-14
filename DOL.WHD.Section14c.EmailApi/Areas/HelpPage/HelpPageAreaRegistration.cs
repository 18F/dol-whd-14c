using System.Web.Http;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.EmailApi.Areas.HelpPage
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
                "EmailHelpPage_Default",
                "EmailHelp/{action}/{apiId}",
                new { controller = "EmailHelp", action = "Index", apiId = UrlParameter.Optional },
                namespaces: new[] { "DOL.WHD.Section14c.EmailApi.Areas.HelpPage.Controllers" }
                );

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}