using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Domain.Models
{
    public class ExampleModel
    {
        [Key]
        [Required]
        public int Number { get; set; }
    }
}
