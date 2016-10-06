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
        public string Status { get; set; }

        [Required]
        public bool IsEducationalAgency { get; set; }

        [Required]
        public DateTime FiscalQuarterEndDate { get; set; }

        [Required]
        public virtual WorkerCountInfo NumSubminimalWageWorkers { get; set; }

        [Required]
        public bool PCA { get; set; }

        [Required]
        public YesNoIntend SCA { get; set; }

        // TODO: SCA Wage Determinations upload

        [Required]
        public YesNoIntend EO13658 { get; set; }

    }

    public enum YesNoIntend { Yes, No, Intend }
}
