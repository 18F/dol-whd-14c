using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class EmployerInfoProvidingFacilitiesDeductionType
    {
        public Guid EmployerInfoId { get; set; }
        public EmployerInfo EmployerInfo { get; set; }

        public int ProvidingFacilitiesDeductionTypeId { get; set; }
        public virtual Response ProvidingFacilitiesDeductionType { get; set; }
    }
}
