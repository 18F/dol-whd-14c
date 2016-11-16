using System;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WorkSite : BaseEntity
    {
        public WorkSite()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int? WorkSiteTypeId { get; set; }
        public virtual Response WorkSiteType { get; set; }

        public string Name { get; set; }

        public virtual Address Address { get; set; }

        public bool? SCA { get; set; }

        public bool? FederalContractWorkPerformed { get; set; }

        public int? NumEmployees { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
