using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }

        public string Key { get; set; }

        public string Description { get; set; }

        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }
    }
}
