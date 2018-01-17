using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Certificates : BaseEntity
    {
        public Certificates()
        {
            Id = Id ?? Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string CertificateNumber { get; set; }
    }
}
