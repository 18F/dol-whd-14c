using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace DOL.WHD.Section14c.EmailApi.Helper
{
    public class EmailContent
    {
        public string To { get; set; }

        public string CC { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<Attachment> attachments { get; set; }
    }
}