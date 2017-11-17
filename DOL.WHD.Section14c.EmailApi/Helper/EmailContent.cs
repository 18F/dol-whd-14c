using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace DOL.WHD.Section14c.EmailApi.Helper
{
    /// <summary>
    /// Email Contents
    /// </summary>
    public class EmailContent
    {
        /// <summary>
        /// Email To address. This field allow multiple email addresses
        /// When multiple email addresses is required, use ";" to separate each addresses
        /// For example; test@test.com;test1@test.com
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Email CC address. This field allow multiple email addresses
        /// When multiple email addresses is required, use ";" to separate each addresses
        /// For example; test@test.com;test1@test.com
        /// </summary>
        public string CC { get; set; }
        /// <summary>
        /// Email Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Email Body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Email attachment
        /// The key is the document name and value is attachment byte array
        /// </summary>
        public Dictionary<string, byte[]> Attachments { get; set; }
    }
}