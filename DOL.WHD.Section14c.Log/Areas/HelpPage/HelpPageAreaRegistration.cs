using System.Web.Http;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.Log.Areas.HelpPage
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
                "LogHelpPage_Default",
                "LogHelp/{action}/{apiId}",
                new { controller = "LogHelp", action = "Index", apiId = UrlParameter.Optional },
                namespaces: new[] { "DOL.WHD.Section14c.Log.Areas.HelpPage.Controllers" }
                );

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}