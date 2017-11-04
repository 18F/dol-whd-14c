using DOL.WHD.Section14c.PdfApi.Business;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace DOL.WHD.Section14c.PdfApi.App_Start
{
    public class DependencyResolutionConfig
    {
        public static void Register()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<IDocumentConcatenate, DocumentConcatenate>(Lifestyle.Singleton);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}