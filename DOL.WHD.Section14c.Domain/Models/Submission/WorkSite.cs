using System;
using System.Collections.Generic;
using System.Linq;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WorkSite : BaseEntity
    {
        public WorkSite()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public IEnumerable<int> WorkSiteTypeId
        {
            set
            {
                WorkSiteType = value.Select(
                    x => new WorkSiteWorkSiteType {WorkSiteTypeId = x, WorkSiteId = Id}).ToList();
            }
        }
        public virtual ICollection<WorkSiteWorkSiteType> WorkSiteType { get; set; }

        public string Name { get; set; }

        public virtual Address Address { get; set; }

        public bool SCA { get; set; }

        public bool FederalContractWorkPerformed { get; set; }

        public int NumEmployees { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
