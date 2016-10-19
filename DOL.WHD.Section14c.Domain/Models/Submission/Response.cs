using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models.Submission
{
    public class Response
    {
        public int Id { get; set; }
        
        [Required]
        public string QuestionKey { get; set; }

        [Required]
        public string Display { get; set; }

        public string SubDisplay { get; set; }

        public string OtherValueKey { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
