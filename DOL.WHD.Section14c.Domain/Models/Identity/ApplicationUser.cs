using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using DOL.WHD.Section14c.DataAccess;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IAuditedEntity
    {
        public ApplicationUser()
        {
            Organizations = new List<OrganizationMembership>();
            Id = Id ?? Guid.NewGuid().ToString();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, RoleManager<ApplicationRole> roleManager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            
            // Add custom user claims here
            foreach (var organization in Organizations)
            {
                var claim = new Claim("Id", organization.EIN);
                if (organization.ApplicationId != null)
                {
                    var applicationClaim = new Claim("APPID", organization.ApplicationId);
                    userIdentity.AddClaim(applicationClaim);
                }
                userIdentity.AddClaim(claim);                
            }

            var userRoles = Roles.Select(x => x.RoleId).ToList();


            if (userRoles.Count == 0)
            {
                // If the user is not in a role, they are an external and can complete an application.
                userIdentity.AddClaim(new Claim(ApplicationClaimTypes.SubmitApplication, true.ToString()));
            }
            else
            {
                // Add Add Application Feature claims based on role.
                var roles = roleManager.Roles.Where(x => userRoles.Contains(x.Id)).ToList();
                var features = roles.SelectMany(x => x.RoleFeatures.Select(f => f.Feature));
                foreach (var feature in features)
                {
                    userIdentity.AddClaim(new Claim(feature.Key, true.ToString()));
                }
            }


            return userIdentity;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public ICollection<OrganizationMembership> Organizations { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy_Id { get; set; }

        [ForeignKey("CreatedBy_Id")]
        public ApplicationUser CreatedBy { get; set; }

        public string LastModifiedBy_Id { get; set; }

        [ForeignKey("LastModifiedBy_Id")]
        public ApplicationUser LastModifiedBy { get; set; }
    }
}