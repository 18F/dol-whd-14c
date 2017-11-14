using System.Web.Http;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.Api.Areas.HelpPage
{
    /// <summary>
    /// 
    /// </summary>
    public class HelpPageAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// 
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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