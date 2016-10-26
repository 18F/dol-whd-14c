using System;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class ApplicationSubmissionEstablishmentType
    {
        public Guid ApplicationSubmissionId { get; set; }
        public ApplicationSubmission ApplicationSubmission { get; set; }

        public int EstablishmentTypeId { get; set; }
        public Response EstablishmentType { get; set; }
    }
}
