﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.Tracing;
using System.Web.Http;
using DOL.WHD.Section14c.Log.Helpers;
using DOL.WHD.Section14c.Log.Controllers;
using DOL.WHD.Section14c.Log.Models;

namespace DOL.WHD.Section14c.Log.ActionFilters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(filterContext.Request, "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, "JSON", filterContext.ActionArguments);
            
            //APIActivityLogs log = new APIActivityLogs {
            //    Message = "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action: " + filterContext.ActionDescriptor.ActionName+ "JSON" + filterContext.ActionArguments
            //};
            //var listOfFiles = new ActivityLogsController().NewActivityLog(log);
        }
    }
}