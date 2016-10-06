using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WorkSite
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

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
