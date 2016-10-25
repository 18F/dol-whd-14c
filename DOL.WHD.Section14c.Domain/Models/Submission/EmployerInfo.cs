using System;
using System.Collections.Generic;
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
        public string TradeName { get; set; }

        [Required]
        public bool LegalNameHasChanged { get; set; }

        [Required]
        public string PriorLegalName { get; set; }

        [Required]
        public virtual Address PhysicalAddress { get; set; }

        [Required]
        public bool HasDifferentMailingAddress { get; set; }

        [Required]
        public bool HasParentOrg { get; set; }

        public string ParentLegalName { get; set; }

        public string ParentTradeName { get; set; }

        public virtual Address ParentAddress { get; set; }

        public bool SendMailToParent { get; set; }

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

        //SCA Wage Determinations upload
        public Attachment SCAAttachment { get; set; }

        [Required]
        public virtual Response EO13658 { get; set; }

        [Required]
        public bool RepresentativePayee { get; set; }

        [Required]
        public bool TakeCreditForCosts { get; set; }

        [Required]
        public virtual Response ProvidingFacilitiesDeductionType { get; set; }

        public string ProvidingFacilitiesDeductionTypeOther { get; set; }

        [Required]
        public bool TemporaryAuthority { get; set; }

    }
}
