using System.Web.Http;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace DOL.WHD.Section14c.Api
{
    public static class DependencyResolutionConfig
    {
        public static void Register()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.Register<IResponseRepository, ResponseRepository>(Lifestyle.Scoped);
            container.Register<ISaveRepository, SaveRepository>(Lifestyle.Scoped);
            container.Register<ISaveService, SaveService>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }


    }
}