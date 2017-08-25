using System;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Domain.ViewModels.Summary
{
    public class ApplicationSummary
    {
        public Guid Id { get; set; }
        public StatusSummary Status { get; set; }
        public DateTime? CertificateEffectiveDate { get; set; }
        public DateTime? CertificateExpirationDate { get; set; }
        public string CertificateNumber { get; set; }
        public IEnumerable<ResponseSummary> CertificateType { get; set; }
        public string State { get; set; }
        public int NumWorkSites { get; set; }
        public int NumWorkers { get; set; }
        public ResponseSummary ApplicationType { get; set; }
        public string EmployerName { get; set; }
    }
}
