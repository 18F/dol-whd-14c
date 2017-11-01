using System.Web.Http;
using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Business.Factories;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.Business.Validators;
using DOL.WHD.Section14c.Common;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using DOL.WHD.Section14c.Log.DataAccess.Repositories;
using DOL.WHD.Section14c.PdfApi.Business;

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
            container.Register<IResponseService, ResponseService>(Lifestyle.Scoped);
            container.Register<ISaveRepository, SaveRepository>(Lifestyle.Scoped);
            container.Register<ISaveService, SaveService>(Lifestyle.Scoped);
            container.Register<IIdentityService, IdentityService>(Lifestyle.Scoped);
            container.Register<IFileRepository>(() => new FileRepository(AppSettings.Get<string>("AttachmentRepositoryRootFolder")), Lifestyle.Scoped);
            container.Register<IApplicationRepository, ApplicationRepository>(Lifestyle.Scoped);
            container.Register<IApplicationService, ApplicationService>(Lifestyle.Scoped);
            container.Register<IApplicationSummaryFactory, ApplicationSummaryFactory>(Lifestyle.Scoped);
            container.Register<IStatusRepository, StatusRepository>(Lifestyle.Scoped);
            container.Register<IStatusService, StatusService>(Lifestyle.Scoped);
            container.Register<IAttachmentRepository, AttachmentRepository>(Lifestyle.Scoped);
            container.Register<IAttachmentService, AttachmentService>(Lifestyle.Scoped);

            // FluentValidation validators (make this singletons since the overhead of spinning up is high and they have no state)
            container.Register<IApplicationSubmissionValidator, ApplicationSubmissionValidator>(Lifestyle.Singleton);
            container.Register<IEmployerValidatorInitial, EmployerValidatorInitial>(Lifestyle.Singleton);
            container.Register<IEmployerValidatorRenewal, EmployerValidatorRenewal>(Lifestyle.Singleton);
            container.Register<IHourlyWageInfoValidator, HourlyWageInfoValidator>(Lifestyle.Singleton);
            container.Register<IPieceRateWageInfoValidator, PieceRateWageInfoValidator>(Lifestyle.Singleton);
            container.Register<IWIOAValidator, WIOAValidator>(Lifestyle.Singleton);
            container.Register<IAddressValidator, AddressValidator>(Lifestyle.Singleton);
            container.Register<IWorkerCountInfoValidator, WorkerCountInfoValidator>(Lifestyle.Singleton);
            container.Register<IPrevailingWageSurveyInfoValidator, PrevailingWageSurveyInfoValidator>(Lifestyle.Singleton);
            container.Register<IAlternateWageDataValidator, AlternateWageDataValidator>(Lifestyle.Singleton);
            container.Register<ISourceEmployerValidator, SourceEmployerValidator>(Lifestyle.Singleton);
            container.Register<IWorkSiteValidatorInitial, WorkSiteValidatorInitial>(Lifestyle.Singleton);
            container.Register<IWorkSiteValidatorRenewal, WorkSiteValidatorRenewal>(Lifestyle.Singleton);
            container.Register<IEmployeeValidator, EmployeeValidator>(Lifestyle.Singleton);
            container.Register<IWIOAWorkerValidator, WIOAWorkerValidator>(Lifestyle.Singleton);
            container.Register<IAddressValidatorNoCounty, AddressValidatorNoCounty>(Lifestyle.Singleton);
            container.Register<ISignatureValidator, SignatureValidator>(Lifestyle.Singleton);
            container.Register<IDocumentConcatenate, DocumentConcatenate>(Lifestyle.Singleton);
            container.Register<IPdfDownloadService, PdfDownloadService>(Lifestyle.Singleton);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Register<IActivityLogRepository, ActivityLogRepository>(Lifestyle.Scoped);
            container.Register<IErrorLogRepository, ErrorLogRepository>(Lifestyle.Scoped);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}