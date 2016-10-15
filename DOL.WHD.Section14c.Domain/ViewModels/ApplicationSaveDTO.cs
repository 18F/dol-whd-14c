using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace DOL.WHD.Section14c.Domain.ViewModels
{
    public class ApplicationSaveDTO
    {
        [Required]
        public string EIN { get; set; }

        public Guid ApplicationId { get; set; }

        [Required]
        public JObject State { get; set; }
    }
}
