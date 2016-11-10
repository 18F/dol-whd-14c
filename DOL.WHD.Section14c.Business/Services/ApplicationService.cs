using System;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.DataAccess;
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

        public Task<int> SubmitApplicationAsync(ApplicationSubmission submission)
        {
            return _applicationRepository.AddAsync(submission);
        }

        public ApplicationSubmission GetApplicationById(Guid id)
        {
            return _applicationRepository.Get().SingleOrDefault(x => x.Id == id);
        }

        public ApplicationSubmission CleanupModel(ApplicationSubmission vm)
        {
            var result = vm;

            // clear out non-selected wage type
            if (result.PayTypeId == 21) // hourly
            {
                result.PieceRateWageInfo = null;
            }
            else if (result.PayTypeId == 22) // piece rate
            {
                result.HourlyWageInfo = null;
            }

            // clear out non-selected prevailing wage method
            CleanupWageTypeInfo(result.HourlyWageInfo);
            CleanupWageTypeInfo(result.PieceRateWageInfo);

            return result;
        }

        private void CleanupWageTypeInfo(WageTypeInfo wageTypeInfo)
        {
            var prevailingWageMethod = wageTypeInfo?.PrevailingWageMethodId;
            if (prevailingWageMethod == 24) // Prevailing Wage Survey
            {
                wageTypeInfo.AlternateWageData = null;
                wageTypeInfo.SCAWageDeterminationId = null;
            }
            else if (prevailingWageMethod == 25) // Alternate Wage Data
            {
                wageTypeInfo.MostRecentPrevailingWageSurvey = null;
                wageTypeInfo.SCAWageDeterminationId = null;
            }
            else if (prevailingWageMethod == 26) // SCA Wage Determination
            {
                wageTypeInfo.MostRecentPrevailingWageSurvey = null;
                wageTypeInfo.AlternateWageData = null;
            }
        }
    }
}
