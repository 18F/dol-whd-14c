using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class ApplicationSave
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string EIN { get; set; }

        [Required]
        public string ApplicationState { get; set; }
    }
}
