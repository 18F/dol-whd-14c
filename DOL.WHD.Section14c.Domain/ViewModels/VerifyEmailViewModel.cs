using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class VerifyEmailViewModel
    {
        public string UserId { get; set; }

        public string Nounce { get; set; }

        public string ReCaptchaResponse { get; set; }
    }
}
