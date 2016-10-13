using System;
using System.Configuration;
using System.Threading.Tasks;
using DOL.WHD.Section14c.DataAccess.Validators;
using DOL.WHD.Section14c.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace DOL.WHD.Section14c.DataAccess.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            // Allows cors for the /token endpoint
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" }); 

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new Section14cUserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
                RequireUniqueEINAdmin = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new Section14cPasswordValidator()
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireZxcvbn = true
            };
            // Configure lockout
            manager.UserLockoutEnabledByDefault = Convert.ToBoolean(ConfigurationManager.AppSettings["UserLockoutEnabledByDefault"]);
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(Double.Parse(ConfigurationManager.AppSettings["DefaultAccountLockoutTimeSpan"]));
            manager.MaxFailedAccessAttemptsBeforeLockout = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"]);

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        protected override Task<IdentityResult> UpdatePassword(IUserPasswordStore<ApplicationUser, string> passwordStore, ApplicationUser user, string newPassword)
        {
            // update the LastPasswordChangedDate field
            user.LastPasswordChangedDate = DateTime.Now;

            return base.UpdatePassword(passwordStore, user, newPassword);
        }
    }
}
