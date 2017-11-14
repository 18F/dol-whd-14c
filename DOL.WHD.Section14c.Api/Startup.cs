using DOL.WHD.Section14c.Api;
using Microsoft.Owin;
using Owin;
using System.Web.Http.ExceptionHandling;

[assembly: OwinStartup(typeof(Startup))]

namespace DOL.WHD.Section14c.Api
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
