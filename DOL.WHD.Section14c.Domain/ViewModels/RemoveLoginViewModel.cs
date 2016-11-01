using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class RemoveLoginViewModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }
}
