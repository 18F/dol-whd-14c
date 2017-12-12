using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOL.WHD.Section14c.Domain.Models.Identity
{
    public sealed class ApplicationSave : BaseEntity
    {
        public ApplicationSave()
        {
            ApplicationId = ApplicationId ?? Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string ApplicationId { get; set; }

        public string Employer_Id { get; set; }
        
        [ForeignKey("Employer_Id")]
        public Employer Employer { get; set; }

        [Required]
        public string ApplicationState { get; set; }
    }
}
