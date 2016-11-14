using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class ApplicationSummary
    {
        public string StatusName { get; set; }
        public DateTime? CertificateEffectiveDate { get; set; }
        public DateTime? CertificateExpirationDate { get; set; }
        public string CertificateNumber { get; set; }
        public IEnumerable<string> CertificateType { get; set; }
        public string State { get; set; }
        public int NumWorkSites { get; set; }
        public int NumWorkers { get; set; }
        public string ApplicationType { get; set; }
    }
}
