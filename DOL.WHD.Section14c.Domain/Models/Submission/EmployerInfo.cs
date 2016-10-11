using System;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class EmployerInfo
    {
        public int Id { get; set; }

        [Required]
        public string LegalName { get; set; }

        [Required]
        public bool HasTradeName { get; set; }

        [Required]
        public bool LegalNameHasChanged { get; set; }

        [Required]
        public virtual Address PhysicalAddress { get; set; }

        [Required]
        public bool HasParentOrg { get; set; }

        [Required]
        public virtual Response EmployerStatus { get; set; }

        // TODO: required if Status == Other
        public string EmployerStatusOther { get; set; }

        [Required]
        public bool IsEducationalAgency { get; set; }

        [Required]
        public DateTime FiscalQuarterEndDate { get; set; }

        [Required]
        public virtual WorkerCountInfo NumSubminimalWageWorkers { get; set; }

        [Required]
        public bool PCA { get; set; }

        [Required]
        public virtual Response SCA { get; set; }

        // TODO: SCA Wage Determinations upload

        [Required]
        public virtual Response EO13658 { get; set; }

    }
}
