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

        private string _legalName;
        public string LegalName {
            get { return this._legalName; }
            set { this._legalName = value.TrimAndToLowerCase(); }
        }

        private string _ein;
        public string EIN {
            get { return this._ein; }
            set { this._ein = value.TrimAndToLowerCase(); }
        }

        public virtual Address PhysicalAddress { get; set; }

    }
}
