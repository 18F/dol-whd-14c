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

namespace DOL.WHD.Section14c.Log.ActionFilters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            try
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
            catch(Exception ex)
            {
                throw new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }
    }
}