using System;
using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class ApplicationSummary
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime? CertificateEffectiveDate { get; set; }
        public DateTime? CertificateExpirationDate { get; set; }
        public string CertificateNumber { get; set; }
        public IEnumerable<Response> CertificateType { get; set; }
        public string State { get; set; }
        public int NumWorkSites { get; set; }
        public int NumWorkers { get; set; }
        public string ApplicationType { get; set; }
        public string EmployerName { get; set; }
    }
}
