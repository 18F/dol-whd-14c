using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class ApplicationSubmissionDto
    {
        #region Assurances

        [Required]
        public bool RepresentativePayeeSocialSecurityBenefits { get; set; }

        // TODO: required if RepresentativePayeeSocialSecurityBenefits == true
        public int NumEmployeesRepresentativePayee { get; set; }

        [Required]
        public bool ProvidingFacilities { get; set; }

        // TODO: required if ProvidingFacilities == true
        public IEnumerable<int> ProvidingFacilitiesDeductionTypeId { get; set; }

        // TODO: required if ProvidingFacilitiesDeductionType == Other
        public string ProvidingFacilitiesDeductionTypeOther { get; set; }

        [Required]
        public bool ReviewedDocumentation { get; set; }

        #endregion

        #region Application Info

        [Required]
        public string EIN { get; set; }

        [Required]
        public int ApplicationTypeId { get; set; }

        [Required]
        public bool HasPreviousApplication { get; set; }

        [Required]
        public bool HasPreviousCertificate { get; set; }

        // TODO: required if HasPreviousCertificate
        public string CertificateNumber { get; set; }

        [Required]
        public IEnumerable<int> EstablishmentTypeId { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        [Phone]
        public string ContactPhone { get; set; }

        [Phone]
        public string ContactFax { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        #endregion

        #region Employer

        [Required]
        public EmployerInfoDto Employer { get; set; }

        #endregion

        #region Wage Data

        [Required]
        public int PayTypeId { get; set; }

        public HourlyWageInfoDto HourlyWageInfo { get; set; }

        public PieceRateWageInfoDto PieceRateWageInfo { get; set; }

        #endregion

        #region Work Sites & Employees

        [Required]
        public int TotalNumWorkSites { get; set; }

        // TODO: validation to make sure WorkSites.Count matches TotalNumWorkSites
        public IEnumerable<WorkSiteDto> WorkSites { get; set; }

        #endregion

        #region WIOA

        [Required]
        public WIOADto WIOA { get; set; }

        #endregion
    }
}
