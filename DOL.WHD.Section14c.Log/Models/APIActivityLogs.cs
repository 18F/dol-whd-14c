using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Log.Models
{
    public class APIActivityLogs: LogDetails
    {
        [Key]
        public int Id { get; set; }

        public string CorrelationId { get; set; }

        public string LogTime { get; set; }

        public string StackTrace { get; set; }

        public bool IsServiceSideLog { get; set; }
    }
}
