using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class AddApplicationSave
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string EIN { get; set; }

        [Required]
        public JObject State { get; set; }
    }
}
