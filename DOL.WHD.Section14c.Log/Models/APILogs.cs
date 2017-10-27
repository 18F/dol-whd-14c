using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOL.WHD.Section14c.Log.Models
{
    /// <summary>
    /// Base API Logs
    /// </summary>
    public class APILogs : LogDetails
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Custom GUID
        /// </summary>
        public string CorrelationId { get; set; }
        /// <summary>
        /// Log Time
        /// </summary>
        public string LogTime { get; set; }
        /// <summary>
        /// Stack Trace
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// Service side client side log
        /// </summary>
        public bool IsServiceSideLog { get; set; }
    }
}