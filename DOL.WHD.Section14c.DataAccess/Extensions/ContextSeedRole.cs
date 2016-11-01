using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.DataAccess.Extensions
{
    internal static class ContextSeedRole
    {
        internal static void SeedRole(this ApplicationDbContext context, string roleName)
        {
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                var store = new ApplicationRoleStore(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = roleName };

                manager.Create(role);
            }
        }

        internal static void AddUserToRole(this ApplicationDbContext context, string userName, string roleName)
        {
            var store = new ApplicationUserStore(context);
            var manager = new ApplicationUserManager(store);

            var user = manager.FindByName(userName);

            if(!manager.IsInRole(user.Id, roleName)){
                manager.AddToRole(user.Id, roleName);
            }
        }
        
        internal static void SeedPassword(this ApplicationDbContext context, string userName, string password)
        {
            var store = new ApplicationUserStore(context);
            var manager = new ApplicationUserManager(store);

            var user = manager.FindByName(userName);

            manager.AddPassword(user.Id, password);
        }

        internal static void AddFeature(this ApplicationDbContext context, string feature, string description)
        {
            if (!context.Features.Any(x => x.Key == feature))
            {
                context.Features.Add(new Feature { Key = feature, Description = description });
            }
        }

        internal static void AddRoleFeature(this ApplicationDbContext context, string role, string feature)
        {
            if (!context.RoleFeatures.Any(x => x.ApplicationRole.Name == role && x.Feature.Key == feature))
            {
                var featureEntity = context.Features.SingleOrDefault(x => x.Key == feature);
                if (featureEntity == null)
                    throw new System.Exception(string.Format("Must create feature {0} before adding to role", feature));

                var identityRole = context.Roles.FirstOrDefault(x => x.Name == role);
                context.RoleFeatures.Add(new RoleFeature { ApplicationRole_Id = identityRole.Id, Feature_Id = featureEntity.Id });
            }
        }
    }
}
