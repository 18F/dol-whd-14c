using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {
            Organizations = new List<OrganizationMembership>();
            Id = Id ?? Guid.NewGuid().ToString();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            foreach (var organization in Organizations)
            {
                var claim = new Claim("EIN", organization.EIN);
                userIdentity.AddClaim(claim);
            }

            return userIdentity;
        }

        public DateTime LastPasswordChangedDate { get; set; }

        public ICollection<OrganizationMembership> Organizations { get; set; }

    }

}