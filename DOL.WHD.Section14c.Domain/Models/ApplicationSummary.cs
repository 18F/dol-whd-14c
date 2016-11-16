using System;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class ApplicationSummary
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        public DateTime? CertificateEffectiveDate { get; set; }
        public DateTime? CertificateExpirationDate { get; set; }
        public string CertificateNumber { get; set; }
        public IEnumerable<string> CertificateType { get; set; }
        public string State { get; set; }
        public int NumWorkSites { get; set; }
        public int NumWorkers { get; set; }
        public string ApplicationType { get; set; }
        public string EmployerName { get; set; }
    }
}
