using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class ApplicationSubmissionProvidingFacilitiesDeductionType
    {
        public Guid ApplicationSubmissionId { get; set; }
        public ApplicationSubmission ApplicationSubmission { get; set; }

        public int ProvidingFacilitiesDeductionTypeId { get; set; }
        public Response ProvidingFacilitiesDeductionType { get; set; }
    }
}
