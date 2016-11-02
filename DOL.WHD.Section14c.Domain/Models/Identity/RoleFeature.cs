using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public class RoleFeature : BaseEntity
    {
        [Key]
        public int RoleFeatureId { get; set; }
        
        public string ApplicationRole_Id { get; set; }

        [ForeignKey("ApplicationRole_Id")]
        public ApplicationRole ApplicationRole { get; set; }

        public int Feature_Id { get; set;  }

        [ForeignKey("Feature_Id")]
        public virtual Feature Feature { get; set; }
    }
}
