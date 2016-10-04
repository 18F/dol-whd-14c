using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DOL.WHD.Section14c.Business.Validators
{
    public class Section14cPasswordValidator : PasswordValidator
    {
        public bool RequireZxcvbn { get; set; }

        public override async Task<IdentityResult> ValidateAsync(string item)
        {
            var result = await base.ValidateAsync(item);

            if (RequireZxcvbn)
            {
                var errors = new List<string>(result.Errors);
                var zxcvbnResult = Zxcvbn.Zxcvbn.MatchPassword(item);
                if (zxcvbnResult.Score <= 1)
                {
                    errors.Add("Password does not meet complexity requirements.");
                }

                result = errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
            }

            return result;
        }
    }
}
