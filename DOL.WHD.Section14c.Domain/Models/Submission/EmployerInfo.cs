using System;
using System.Collections.Generic;
using System.Linq;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class EmployerInfo : BaseEntity
    {
        public EmployerInfo()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string LegalName { get; set; }

        public bool? HasTradeName { get; set; }

        public string TradeName { get; set; }

        public bool? LegalNameHasChanged { get; set; }

        public string PriorLegalName { get; set; }

        public virtual Address PhysicalAddress { get; set; }

        public bool? HasMailingAddress { get; set; }

        public virtual Address MailingAddress { get; set; }

        public bool? HasParentOrg { get; set; }

        public string ParentLegalName { get; set; }

        public string ParentTradeName { get; set; }

        public virtual Address ParentAddress { get; set; }

        public bool? SendMailToParent { get; set; }

        public int? EmployerStatusId { get; set; }
        public virtual Response EmployerStatus { get; set; }

        public string EmployerStatusOther { get; set; }

        public bool? IsEducationalAgency { get; set; }

        public DateTime? FiscalQuarterEndDate { get; set; }

        public virtual WorkerCountInfo NumSubminimalWageWorkers { get; set; }

        public bool? PCA { get; set; }

        public int? SCAId { get; set; }
        public virtual Response SCA { get; set; }

        public int? SCACount { get; set; }

        //SCA Wage Determinations upload
        public Guid? SCAAttachmentId { get; set; }
        public virtual Attachment SCAAttachment { get; set; }

        public int? EO13658Id { get; set; }
        public virtual Response EO13658 { get; set; }

        public bool? RepresentativePayee { get; set; }

        public bool? TakeCreditForCosts { get; set; }

        public IEnumerable<int> ProvidingFacilitiesDeductionTypeId
        {
            set
            {
                if (value != null)
                {
                    ProvidingFacilitiesDeductionType = value.Select(
                        x =>
                            new EmployerInfoProvidingFacilitiesDeductionType
                            {
                                ProvidingFacilitiesDeductionTypeId = x,
                                EmployerInfoId = Id
                            }).ToList();
                }
            }
        }
        public virtual ICollection<EmployerInfoProvidingFacilitiesDeductionType> ProvidingFacilitiesDeductionType { get; set; }

        public bool? TemporaryAuthority { get; set; }

    }
}
