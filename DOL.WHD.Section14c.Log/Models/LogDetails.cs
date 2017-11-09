using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DOL.WHD.Section14c.Log.Models
{
    public class LogDetails
    {
        [Required]
        [Column(TypeName = "CLOB")]
        public string Message { get; set; }

        public string EIN { get; set; }

        public string UserId { get; set; }

        public string User { get; set; }        

        [Required]
        public string Level { get; set; }

        public string Exception { get; set; }
    }
}