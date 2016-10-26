using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class WorkSiteDto
    {
        [Required]
        public IEnumerable<int> WorkSiteTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public bool SCA { get; set; }

        [Required]
        public bool FederalContractWorkPerformed { get; set; }

        [Required]
        public int NumEmployees { get; set; }

        // TODO: validate Employees.Count == NumEmployees
        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
