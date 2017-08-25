using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public sealed class ApplicationSave : BaseEntity
    {
        [Key]
        public string EIN { get; set; }

        [Required]
        public string ApplicationState { get; set; }
    }
}
