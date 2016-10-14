using System;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class ApplicationSave
    {
        public ApplicationSave()
        {
            ApplicationId = Guid.NewGuid();
        }

        [Key]
        public string EIN { get; set; }

        public Guid ApplicationId { get; set; }

        [Required]
        public string ApplicationState { get; set; }
    }
}
