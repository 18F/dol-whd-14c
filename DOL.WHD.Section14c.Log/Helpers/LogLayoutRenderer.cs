using NLog;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DOL.WHD.Section14c.Log.Helpers
{
    [LayoutRenderer("api-log")]
    public class LogLayoutRenderer: LayoutRenderer
    {
        public string Name { get; set; }
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                switch (Name)
                {
                    case "EIN":
                        builder.Append("My EIN ID");
                        break;
                    case "Exception":
                        if(logEvent.Exception != null)
                        {
                            builder.Append(logEvent.Exception.Message);
                        }                        
                        break;
                    case "StackTrace":
                        if (logEvent.Exception != null)
                        {
                            builder.Append(logEvent.Exception.StackTrace);
                        }
                        break;
                    case "UserName":
                        builder.Append("My User ID");
                        break;

                }
            }
        }
    }
}