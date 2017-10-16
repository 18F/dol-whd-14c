using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DOL.WHD.Section14c.Log.Models
{
    public class APIErrorLogs: LogDetails
    {
        [Key]
        public int Id { get; set; }

        public string CorrelationId { get; set; }

        public string LogTime { get; set; }

        public string StackTrace { get; set; }

    }
}