using System;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Common;
using DOL.WHD.Section14c.DataAccess.Validators;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace DOL.WHD.Section14c.DataAccess.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store): base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new Section14cUserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
                RequireUniqueEINAdmin = false
            };

            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });

            manager.EmailService = new EmailService();

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
            manager.UserLockoutEnabledByDefault = AppSettings.Get<bool>("UserLockoutEnabledByDefault");
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(AppSettings.Get<double>("DefaultAccountLockoutTimeSpan"));
            manager.MaxFailedAccessAttemptsBeforeLockout = AppSettings.Get<int>("MaxFailedAccessAttemptsBeforeLockout");

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(AppSettings.Get<double>("EmailVeriryAndPaswordRestTokenExpireHours"))
                };
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

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var roleStore = new ApplicationRoleStore(context.Get<ApplicationDbContext>());
            return new ApplicationRoleManager(roleStore);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "False positive. IDisposable is inherited via UserStore")]
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole, string, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
