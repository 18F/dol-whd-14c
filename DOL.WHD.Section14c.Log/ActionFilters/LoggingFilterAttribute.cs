using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.Tracing;
using System.Web.Http;
using DOL.WHD.Section14c.Log.Helpers;
using DOL.WHD.Section14c.Log.LogHelper;
using System.Net;
using NLog;
using System.Configuration;

namespace DOL.WHD.Section14c.Log.ActionFilters
{
    /// <summary>
    /// Filter to mark a class for error logging
    /// </summary>
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Action execution handler
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            try
            {
                if (!ActionsToIgnore(filterContext.ActionDescriptor.ActionName))
                {
                    var correlationId = Guid.NewGuid().ToString();

                    GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger(logger));
                    var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                    filterContext.Request.Properties[Constants.CorrelationId] = correlationId;
                    trace.Info(filterContext.Request,
                        "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
                        Environment.NewLine +
                        "Action : " + filterContext.ActionDescriptor.ActionName,
                        "JSON", filterContext.ActionArguments);
                }
            }
            catch(Exception ex)
            {
                throw new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }

        private bool ActionsToIgnore(string actionName)
        {
            var ignore = false;
            var appSettingValue = ConfigurationManager.AppSettings["ApiLogActionsToIgnore"];
            if (!string.IsNullOrEmpty(appSettingValue))
            {
                foreach (var name in appSettingValue.Split(','))
                {
                    if(actionName.ToLower() == name.ToLower())
                    {
                        ignore = true;
                    }
                }                    
            }
            return ignore;
        }
    }
}