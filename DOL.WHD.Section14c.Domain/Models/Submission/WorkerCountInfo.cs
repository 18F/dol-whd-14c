using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class WorkerCountInfo : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public int Total { get; set; }

        [Required]
        public int WorkCenter { get; set; }

        [Required]
        public int PatientWorkers { get; set; }

        [Required]
        public int SWEP { get; set; }

        [Required]
        public int BusinessEstablishment { get; set; }
    }
}
