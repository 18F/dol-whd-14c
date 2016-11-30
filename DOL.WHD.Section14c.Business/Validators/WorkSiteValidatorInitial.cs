using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using FluentValidation;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class WorkSiteValidatorInitial : BaseValidator<WorkSite>, IWorkSiteValidatorInitial
    {
        public WorkSiteValidatorInitial(IAddressValidatorNoCounty addressValidatorNoCounty)
        {
            RuleFor(w => w.WorkSiteTypeId).NotNull().InclusiveBetween(ResponseIds.WorkSiteType.MainEstablishment, ResponseIds.WorkSiteType.SWEP);
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.Address).NotNull().SetValidator(addressValidatorNoCounty);
            RuleFor(w => w.SCA).NotNull();
            RuleFor(w => w.FederalContractWorkPerformed).NotNull();
        }
    }
}
