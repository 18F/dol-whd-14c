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
            if (!string.IsNullOrEmpty(Name) && logEvent != null)
            {
                switch (Name)
                {
                    case "EIN":
                        if(logEvent.Properties.ContainsKey(Constants.EIN) && 
                            logEvent.Properties[Constants.EIN] != null)
                            builder.Append(logEvent.Properties[Constants.EIN].ToString());
                        break;
                    case "Exception":
                        if(logEvent.Exception != null)
                        {
                            builder.Append(logEvent.Exception.GetBaseException().Message);
                        }                        
                        break;
                    case "StackTrace":
                        if (logEvent.Exception != null)
                        {
                            builder.Append(logEvent.Exception.StackTrace);
                        }
                        break;
                    case "UserId":
                        if (logEvent.Properties.ContainsKey(Constants.UserId) && 
                            logEvent.Properties[Constants.UserId] != null)
                            builder.Append(logEvent.Properties[Constants.UserId].ToString());
                        break;
                    case "UserName":
                        if (logEvent.Properties.ContainsKey(Constants.UserName) && 
                            logEvent.Properties[Constants.UserName] != null)
                            builder.Append(logEvent.Properties[Constants.UserName].ToString());
                        break;
                    case "CorrelationId":
                        if (logEvent.Properties.ContainsKey(Constants.CorrelationId) &&
                            logEvent.Properties[Constants.CorrelationId] != null)
                            builder.Append(logEvent.Properties[Constants.CorrelationId].ToString());
                        break;
                }
            }
        }
    }
}