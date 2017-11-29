using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DOL.WHD.Section14c.Log.Models
{
    /// <summary>
    /// Model representing a log entry
    /// </summary>
    public class LogDetails
    {
        /// <summary>
        /// Log message
        /// </summary>
        [Required]
        [Column(TypeName = "CLOB")]
        public string Message { get; set; }

        /// <summary>
        /// EIN associated with the log entry
        /// </summary>
        public string EIN { get; set; }

        /// <summary>
        /// User ID associated with the log entry
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User associated with the log entry
        /// </summary>
        public string User { get; set; }        

        /// <summary>
        /// Log level
        /// </summary>
        [Required]
        public string Level { get; set; }

        /// <summary>
        /// Exception message for the log entry, if appropriate
        /// </summary>
        public string Exception { get; set; }
    }
}