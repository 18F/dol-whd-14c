using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class WIOAWorkerDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public int WIOAWorkerVerifiedId { get; set; }
    }
}
