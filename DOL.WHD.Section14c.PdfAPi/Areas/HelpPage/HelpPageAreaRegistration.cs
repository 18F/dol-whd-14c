using System.Web.Http;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.PdfApi.Areas.HelpPage
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
                "PdfApiHelpPage_Default",
                "PdfApiHelp/{action}/{apiId}",
                new { controller = "PdfApiHelp", action = "Index", apiId = UrlParameter.Optional },
                namespaces: new[] { "DOL.WHD.Section14c.PdfApi.Areas.HelpPage.Controllers" }
                );

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}