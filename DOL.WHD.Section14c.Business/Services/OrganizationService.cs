using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business.Services
{
    /// <summary>
    /// Organization Service
    /// </summary>
    public class OrganizationService: IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        /// <summary>
        ///  Default constructor
        /// </summary>
        /// <param name="organizationRepository"></param>
        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        /// <summary>
        /// Get Organization Membership By Employer
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        public OrganizationMembership GetOrganizationMembershipByEmployer(Employer employer)
        {
            return _organizationRepository.Get().SingleOrDefault(x => x.Employer.Id == employer.Id && x.IsAdmin == true);
        }

        /// <summary>
        /// Get All Organization Memberships
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrganizationMembership> GetAllOrganizationMemberships()
        {
            return _organizationRepository.Get();
        }
    }
}
