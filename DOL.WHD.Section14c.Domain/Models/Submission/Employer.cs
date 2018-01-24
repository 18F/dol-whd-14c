using DOL.WHD.Section14c.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Employer : BaseEntity
    {
        public Employer()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string CertificateNumber { get; set; }
        
        public string LegalName { get; set; }
        
        public string EIN { get; set; }

        public virtual Address PhysicalAddress { get; set; }

    }
}
