using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class ApplicationSubmission
    {
        public int Id { get; set; }

        #region Application Info

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ApplicationType { get; set; }

        [Required]
        public bool HasPreviousApplication { get; set; }

        [Required]
        public bool HasPreviousCertificate { get; set; }

        // TODO: required if HasPreviousCertificate
        public string CertificateNumber { get; set; }

        [Required]
        public bool TemporaryAuthority { get; set; }

        [Required]
        public IEnumerable<string> EstablishmentType { get; set; }

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
        public EmployerInfo Employer { get; set; }

        #endregion

        #region Wage Data

        [Required]
        public string PayType { get; set; }

        // TODO: Validation on what WageTypeInfo is based on PayType
        public WageTypeInfo WageTypeInfo { get; set; }

        #endregion

        #region Work Sites & Employees

        [Required]
        public int TotalNumWorkSites { get; set; }

        // TODO: validation to make sure WorkSites.Count matches TotalNumWorkSites
        public IEnumerable<WorkSite> WorkSites { get; set; }

        #endregion

        #region Assurances

        [Required]
        public bool RepresentativePayeeSocialSecurityBenefits { get; set; }

        // TODO: required if RepresentativePayeeSocialSecurityBenefits == true
        public int NumEmployeesRepresentativePayee { get; set; }

        [Required]
        public bool ProvidingFacilities { get; set; }

        // TODO: required if ProvidingFacilities == true
        public string ProvidingFacilitiesDeductionType { get; set; }

        [Required]
        public bool ReviewedDocumentation { get; set; }

        #endregion
    }
}
