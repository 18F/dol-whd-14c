using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using Microsoft.AspNet.Identity;

namespace DOL.WHD.Section14c.DataAccess.Validators
{
    public class Section14cUserValidator<TUser> : UserValidator<TUser, string>
        where TUser : ApplicationUser
    {
        private readonly UserManager<TUser, string> _manager;

        public bool RequireUniqueEINAdmin { get; set; }

        public Section14cUserValidator(UserManager<TUser, string> manager) : base(manager)
        {
            this._manager = manager;
        }

        public override async Task<IdentityResult> ValidateAsync(TUser item)
        {
            IdentityResult result = await base.ValidateAsync(item);

            if (RequireUniqueEINAdmin)
            {
                var errors = new List<string>(result.Errors);

                // check EIN (no more than one admin per EIN)
                var myAdminEINs = item.Organizations.Where(o => o.IsAdmin).Select(o => o.EIN);
                var otherUsers = _manager.Users.Where(u => u.Id != item.Id);
                var match = otherUsers.Any(u => u.Organizations.Where(o => o.IsAdmin).Any(o => myAdminEINs.Contains(o.EIN)));

                if (match)
                {
                    errors.Add("EIN is already registered");
                }

                result = errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
            }

            return result;
        }
    }
}