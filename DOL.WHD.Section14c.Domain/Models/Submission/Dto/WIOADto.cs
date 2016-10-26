using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Domain.Models.Submission.Dto
{
    public class WIOADto
    {
        [Required]
        public bool HasVerfiedDocumentaion { get; set; }

        [Required]
        public bool HasWIOAWorkers { get; set; }

        public IEnumerable<WIOAWorkerDto> WIOAWorkers { get; set; }

    }
}
