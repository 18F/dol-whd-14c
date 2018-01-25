using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<int> SubmitApplicationAsync(ApplicationSubmission submission)
        {
            return await _applicationRepository.AddAsync(submission);
        }

        public ApplicationSubmission GetApplicationById(Guid id)
        {
            return _applicationRepository.Get().SingleOrDefault(x => x.Id == id.ToString());
        }

        public IEnumerable<ApplicationSubmission> GetAllApplications()
        {
            return _applicationRepository.Get().ToList();
        }

        public async Task<int> ChangeApplicationStatus(ApplicationSubmission application, int newStatusId)
        {
            application.StatusId = newStatusId;
            return await _applicationRepository.ModifyApplication(application);
        }

        public void ProcessModel(ApplicationSubmission vm)
        {
            CleanupModel(vm);
            SetDefaults(vm);
        }

        private void CleanupModel(ApplicationSubmission model)
        {
            // clear out non-selected wage type
            if (model.PayTypeId == ResponseIds.PayType.Hourly)
            {
                model.PieceRateWageInfo = null;
            }
            else if (model.PayTypeId == ResponseIds.PayType.PieceRate)
            {
                model.HourlyWageInfo = null;
            }

            // clear out non-selected prevailing wage method
            CleanupWageTypeInfo(model.HourlyWageInfo);
            CleanupWageTypeInfo(model.PieceRateWageInfo);

            // clear out fields for initial application
            if (model.ApplicationTypeId == ResponseIds.ApplicationType.Initial)
            {
                model.Employer.FiscalQuarterEndDate = null;
                model.Employer.NumSubminimalWageWorkers = null;
                model.PayTypeId = null;
                model.HourlyWageInfo = null;
                model.PieceRateWageInfo = null;
                foreach (var workSite in model.WorkSites)
                {
                    workSite.NumEmployees = null;
                    workSite.Employees = null;
                }
            }
        }

        private void SetDefaults(ApplicationSubmission model)
        {
            // set status
            model.Status = null;
            model.StatusId = StatusIds.Pending;

            // default admin fields
            model.CertificateEffectiveDate = null;
            model.CertificateExpirationDate = null;
            model.CertificateNumber = null;

            // default checkboxes
            if (model.Employer != null)
            {
                if (model.Employer.HasParentOrg.GetValueOrDefault())
                {
                    model.Employer.SendMailToParent = model.Employer.SendMailToParent ?? false;
                }
                else
                {
                    model.Employer.SendMailToParent = null;
                }

                // default to false if no value was passed
                model.Employer.HasMailingAddress = model.Employer.HasMailingAddress ?? false;
                if (!model.Employer.HasMailingAddress.GetValueOrDefault())
                {
                    // remove mailing address if hasMailingAddress == false
                    model.Employer.MailingAddress = null;
                }
            }
        }

        private void CleanupWageTypeInfo(WageTypeInfo wageTypeInfo)
        {
            var prevailingWageMethod = wageTypeInfo?.PrevailingWageMethodId;
            if (prevailingWageMethod == ResponseIds.PrevailingWageMethod.PrevailingWageSurvey)
            {
                wageTypeInfo.AlternateWageData = null;
                wageTypeInfo.SCAAttachments = null;
            }
            else if (prevailingWageMethod == ResponseIds.PrevailingWageMethod.AlternateWageData)
            {
                wageTypeInfo.MostRecentPrevailingWageSurvey = null;
                wageTypeInfo.SCAAttachments = null;
            }
            else if (prevailingWageMethod == ResponseIds.PrevailingWageMethod.SCAWageDetermination)
            {
                wageTypeInfo.MostRecentPrevailingWageSurvey = null;
                wageTypeInfo.AlternateWageData = null;
            }
        }
    }
}
