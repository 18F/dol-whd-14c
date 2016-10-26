using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class ApplicationSubmission : BaseEntity
    {
        public int Id { get; set; }

        #region Assurances

        [Required]
        public bool RepresentativePayeeSocialSecurityBenefits { get; set; }

        // TODO: required if RepresentativePayeeSocialSecurityBenefits == true
        public int NumEmployeesRepresentativePayee { get; set; }

        [Required]
        public bool ProvidingFacilities { get; set; }

        // TODO: required if ProvidingFacilities == true
        public ICollection<Response> ProvidingFacilitiesDeductionType { get; set; }

        // TODO: required if ProvidingFacilitiesDeductionType == Other
        public string ProvidingFacilitiesDeductionTypeOther { get; set; }

        [Required]
        public bool ReviewedDocumentation { get; set; }

        #endregion

        #region Application Info

        [Required]
        public string UserId { get; set; }

        [Required]
        public virtual Response ApplicationType { get; set; }

        [Required]
        public bool HasPreviousApplication { get; set; }

        [Required]
        public bool HasPreviousCertificate { get; set; }

        // TODO: required if HasPreviousCertificate
        public string CertificateNumber { get; set; }

        [Required]
        public virtual ICollection<Response> EstablishmentType { get; set; }

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
        public virtual EmployerInfo Employer { get; set; }

        #endregion

        #region Wage Data

        [Required]
        public virtual Response PayType { get; set; }

        public virtual HourlyWageInfo HourlyWageInfo { get; set; }

        public virtual PieceRateWageInfo PieceRateWageInfo { get; set; }

        #endregion

        #region Work Sites & Employees

        [Required]
        public int TotalNumWorkSites { get; set; }

        // TODO: validation to make sure WorkSites.Count matches TotalNumWorkSites
        public virtual ICollection<WorkSite> WorkSites { get; set; }

        #endregion

        #region WIOA

        [Required]
        public virtual WIOA WIOA { get; set; }

        #endregion
    }
}
