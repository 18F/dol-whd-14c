using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business
{
    public interface IOrganizationService
    {
        OrganizationMembership GetOrganizationMembershipByEmployer(Employer employer);        
        IEnumerable<OrganizationMembership> GetAllOrganizationMemberships();
        void UpdateOrganizationMembership(OrganizationMembership organizationMembership);
    }
}
