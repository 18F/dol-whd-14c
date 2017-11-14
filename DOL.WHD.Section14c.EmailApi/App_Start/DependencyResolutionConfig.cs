using DOL.WHD.Section14c.EmailApi.Business;
using DOL.WHD.Section14c.Log.DataAccess.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace DOL.WHD.Section14c.EmailApi.App_Start
{

    public class DependencyResolutionConfig
    {
        public static void Register()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.Register<IEmailService>(() => new EmailService(null), Lifestyle.Scoped);
            container.Register<IActivityLogRepository, ActivityLogRepository>(Lifestyle.Scoped);
            container.Register<IErrorLogRepository, ErrorLogRepository>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}