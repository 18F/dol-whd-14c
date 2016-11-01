using System;
using System.Collections.Generic;
using System.Linq;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class ApplicationSubmission : BaseEntity
    {
        public ApplicationSubmission()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        #region Assurances

        public bool? RepresentativePayeeSocialSecurityBenefits { get; set; }

        public int? NumEmployeesRepresentativePayee { get; set; }

        public bool? ProvidingFacilities { get; set; }

        public IEnumerable<int> ProvidingFacilitiesDeductionTypeId
        {
            set
            {
                if (value != null)
                {
                    ProvidingFacilitiesDeductionType = value.Select(
                        x =>
                            new ApplicationSubmissionProvidingFacilitiesDeductionType
                            {
                                ProvidingFacilitiesDeductionTypeId = x,
                                ApplicationSubmissionId = Id
                            }).ToList();
                }
            }
        }
        public ICollection<ApplicationSubmissionProvidingFacilitiesDeductionType> ProvidingFacilitiesDeductionType { get; set; }

        public string ProvidingFacilitiesDeductionTypeOther { get; set; }

        public bool? ReviewedDocumentation { get; set; }

        #endregion

        #region Application Info

        public string EIN { get; set; }

        public int? ApplicationTypeId { get; set; }
        public virtual Response ApplicationType { get; set; }

        public bool? HasPreviousApplication { get; set; }

        public bool? HasPreviousCertificate { get; set; }

        public string CertificateNumber { get; set; }

        public IEnumerable<int> EstablishmentTypeId
        {
            set
            {
                if (value != null)
                {
                    EstablishmentType =
                        value.Select(
                            x =>
                                new ApplicationSubmissionEstablishmentType
                                {
                                    EstablishmentTypeId = x,
                                    ApplicationSubmissionId = Id
                                }).ToList();
                }
            }
        }
        public virtual ICollection<ApplicationSubmissionEstablishmentType> EstablishmentType { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string ContactFax { get; set; }

        public string ContactEmail { get; set; }

        #endregion

        #region Employer

        public virtual EmployerInfo Employer { get; set; }

        #endregion

        #region Wage Data

        public int? PayTypeId { get; set; }
        public virtual Response PayType { get; set; }

        public virtual HourlyWageInfo HourlyWageInfo { get; set; }

        public virtual PieceRateWageInfo PieceRateWageInfo { get; set; }

        #endregion

        #region Work Sites & Employees

        public int? TotalNumWorkSites { get; set; }

        public virtual ICollection<WorkSite> WorkSites { get; set; }

        #endregion

        #region WIOA

        public virtual WIOA WIOA { get; set; }

        #endregion
    }
}
