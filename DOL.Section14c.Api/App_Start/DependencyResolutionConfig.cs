using System.Web.Http;
using DOL.Section14c.Business;
using DOL.Section14c.Business.Services;
using DOL.Section14c.DataAccess;
using DOL.Section14c.DataAccess.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace DOL.Section14c.Api
{
    public static class DependencyResolutionConfig
    {
        public static void Register()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<IExampleService, ExampleService>(Lifestyle.Scoped);
            container.Register<IExampleRepository, ExampleRepository>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }


    }
}