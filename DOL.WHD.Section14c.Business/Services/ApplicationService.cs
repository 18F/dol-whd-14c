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

        public Task<int> SubmitApplicationAsync(ApplicationSubmission submission)
        {
            return _applicationRepository.AddAsync(submission);
        }

        public ApplicationSubmission CleanupModel(ApplicationSubmission vm)
        {
            var result = vm;

            // clear out non-selected wage type
            if (result.PayTypeId == ResponseIds.PayType.Hourly)
            {
                result.PieceRateWageInfo = null;
            }
            else if (result.PayTypeId == ResponseIds.PayType.PieceRate)
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
            if (prevailingWageMethod == ResponseIds.PrevailingWageMethod.PrevailingWageSurvey)
            {
                wageTypeInfo.AlternateWageData = null;
                wageTypeInfo.SCAWageDeterminationId = null;
            }
            else if (prevailingWageMethod == ResponseIds.PrevailingWageMethod.AlternateWageData)
            {
                wageTypeInfo.MostRecentPrevailingWageSurvey = null;
                wageTypeInfo.SCAWageDeterminationId = null;
            }
            else if (prevailingWageMethod == ResponseIds.PrevailingWageMethod.SCAWageDetermination)
            {
                wageTypeInfo.MostRecentPrevailingWageSurvey = null;
                wageTypeInfo.AlternateWageData = null;
            }
        }
    }
}
