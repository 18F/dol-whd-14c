using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WorkSite : BaseEntity
    {
        public WorkSite()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public virtual ICollection<WorkSiteWorkSiteType> WorkSiteType { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        public bool SCA { get; set; }

        [Required]
        public bool FederalContractWorkPerformed { get; set; }

        [Required]
        public int NumEmployees { get; set; }

        // TODO: validate Employees.Count == NumEmployees
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
