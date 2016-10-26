using System;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class EmployerInfoDto
    {
        [Required]
        public string LegalName { get; set; }

        [Required]
        public bool HasTradeName { get; set; }

        public string TradeName { get; set; }

        [Required]
        public bool LegalNameHasChanged { get; set; }

        public string PriorLegalName { get; set; }

        [Required]
        public Address PhysicalAddress { get; set; }

        [Required]
        public bool HasDifferentMailingAddress { get; set; }

        [Required]
        public bool HasParentOrg { get; set; }

        public string ParentLegalName { get; set; }

        public string ParentTradeName { get; set; }

        public Address ParentAddress { get; set; }

        public bool SendMailToParent { get; set; }

        [Required]
        public int EmployerStatusId { get; set; }

        // TODO: required if Status == Other
        public string EmployerStatusOther { get; set; }

        [Required]
        public bool IsEducationalAgency { get; set; }

        [Required]
        public DateTime FiscalQuarterEndDate { get; set; }

        [Required]
        public WorkerCountInfoDto NumSubminimalWageWorkers { get; set; }

        [Required]
        public bool PCA { get; set; }
        
        [Required]
        public int SCAId { get; set; }

        //SCA Wage Determinations upload
        public Guid? SCAAttachmentId { get; set; }

        [Required]
        public int EO13658Id { get; set; }

        [Required]
        public bool RepresentativePayee { get; set; }

        [Required]
        public bool TakeCreditForCosts { get; set; }

        [Required]
        public int ProvidingFacilitiesDeductionTypeId { get; set; }

        public string ProvidingFacilitiesDeductionTypeOther { get; set; }

        [Required]
        public bool TemporaryAuthority { get; set; }

    }
}
