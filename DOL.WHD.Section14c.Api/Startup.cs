using DOL.WHD.Section14c.Api;
using Microsoft.Owin;
using Owin;
using System.Web.Http.ExceptionHandling;

[assembly: OwinStartup(typeof(Startup))]

namespace DOL.WHD.Section14c.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
