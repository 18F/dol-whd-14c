using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Tracing;
using NLog;
using System.Net.Http;
using System.Text;
using DOL.WHD.Section14c.Log.LogHelper;
using NLog.Config;
using NLog.LayoutRenderers;
using System.Threading;
using System.Web.Http.Controllers;

namespace DOL.WHD.Section14c.Log.Helpers
{
    /// <summary>
    /// Public class to log Error/info messages to the access log file
    /// </summary>
    public sealed class NLogger : ITraceWriter
    {

        #region Private member variables.
        private static ILogger ClassLogger { get; set; }
        private static readonly Lazy<Dictionary<TraceLevel, Action<string>>> LoggingMap = new Lazy<Dictionary<TraceLevel, Action<string>>>(() => new Dictionary<TraceLevel, Action<string>> { { TraceLevel.Info, ClassLogger.Info }, { TraceLevel.Debug, ClassLogger.Debug }, { TraceLevel.Error, ClassLogger.Error }, { TraceLevel.Fatal, ClassLogger.Fatal }, { TraceLevel.Warn, ClassLogger.Warn } });
        #endregion

        #region Private properties.
        /// <summary>
        /// Get property for Logger
        /// </summary>
        private Dictionary<TraceLevel, Action<string>> Logger
        {
            get { return LoggingMap.Value; }
        }
        #endregion

        #region Public member methods.
        public NLogger(ILogger logger)
        {
            ClassLogger = logger;
        }

        /// <summary>
        /// Implementation of TraceWriter to trace the logs.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <param name="level"></param>
        /// <param name="traceAction"></param>
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                if (traceAction != null && traceAction.Target != null)
                {
                    var traceActionTargetJsonString = string.Empty;
                    try
                    {
                        traceActionTargetJsonString = traceAction.Target.ToJSON(); // Catch JavaScriptSerializer error
                    }
                    catch (Exception e)
                    {
                        level = TraceLevel.Error;
                        traceActionTargetJsonString = string.Format("Message: {0} , StackTrace: {1}, InnerException: {2}", e.Message, e.StackTrace, e.InnerException == null ? string.Empty : e.InnerException.Message);
                    }
                    category = category + Environment.NewLine + "Action Parameters : " + traceActionTargetJsonString;

                }
                var record = new TraceRecord(request, category, level);
                if (traceAction != null) traceAction(record);

                Log(record);
            }
        }
        #endregion
        #region Private member methods.
        /// <summary>
        /// Logs info/Error to Log file
        /// </summary>
        /// <param name="record"></param>
        private void Log(TraceRecord record)
        {
            var message = new StringBuilder();
            LogEventInfo eventInfo = new LogEventInfo();
            eventInfo.LoggerName = Constants.LoggerName;
            eventInfo.Properties[Constants.CorrelationId] = getCorrelationId(record);
            // Set Log Property Is Service Side true.
            eventInfo.Properties[Constants.IsServiceSideLog] = 1;

            if (!string.IsNullOrWhiteSpace(record.Message))
                message.Append(record.Message + Environment.NewLine);

            /// Get request Information and append to the message
            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    message.Append("Method: ").Append(record.Request.Method).Append(Environment.NewLine);

                if (record.Request.RequestUri != null)
                    message.Append("URL: ").Append(record.Request.RequestUri).Append(Environment.NewLine);

                if (record.Request.Headers != null && record.Request.Headers.Contains("Token") && record.Request.Headers.GetValues("Token") != null && record.Request.Headers.GetValues("Token").FirstOrDefault() != null)
                    message.Append("Token: ").Append(record.Request.Headers.GetValues("Token").FirstOrDefault()).Append(Environment.NewLine);
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
                message.Append(record.Category);

            if (!string.IsNullOrWhiteSpace(record.Operator))
                message.Append(record.Operator).Append(" ").Append(record.Operation);

            // Get exception information; append to the message details and Event exception object
            if (record.Exception != null && !string.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message))
            {
                eventInfo.Exception = record.Exception;

                var apiException = record.Exception as BaseApiException;
                if (apiException != null)
                {
                    message.Append("Error: ").Append(apiException.ErrorDescription).Append(Environment.NewLine);
                    message.Append("Error Code: ").Append(apiException.ErrorCode).Append(Environment.NewLine);
                }
                else
                {
                    message.Append("Error: ").Append(record.Exception.GetBaseException().Message).Append(Environment.NewLine);
                }                
            }

            // Not every request will have user details. Unauthenticated users will not return anything
            var currentUserName = TryToFindUserName(record);
            eventInfo.Properties[Constants.UserName] = currentUserName;           

            eventInfo.Message = Convert.ToString(message);
            eventInfo.Level = LogLevel.FromString(record.Level.ToString());

            // Log to the database
            ClassLogger.Log(eventInfo);
        }

        /// <summary>
        /// Get correlation Id from request object
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private string getCorrelationId(TraceRecord record)
        {
            string correlationId = string.Empty;
            if (record != null  && record.Request != null)
            {
                object correlationIdObject;
                record.Request.Properties.TryGetValue(Constants.CorrelationId, out correlationIdObject);
                if (correlationIdObject != null)
                    correlationId = correlationIdObject.ToString();
            }
            return correlationId;
        }

        /// <summary>
        /// Find Current Login User Name
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private string TryToFindUserName(TraceRecord record)
        {
            string temp = string.Empty;
            try
            {
                if (record.Request != null && record.Request.Properties.ContainsKey(Constants.MSRequestContext))
                {
                    var context = record.Request.Properties[Constants.MSRequestContext] as HttpRequestContext;
                    temp = GetUserName(context);
                }
            }

            catch (Exception ex)
            {
                // Do nothing if user not found.
            }
            return temp;
        }
   
        /// <summary>
        /// Get User Name
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetUserName(HttpRequestContext context)
        {
            var userName = string.Empty;
            if (context != null && context.Principal != null && context.Principal.Identity.IsAuthenticated)
            {
                userName = context.Principal.Identity.Name;
            }
            else
            {
                var threadPincipal = Thread.CurrentPrincipal;
                if (threadPincipal != null && threadPincipal.Identity.IsAuthenticated)
                {
                    userName = threadPincipal.Identity.Name;
                }
            }
            return userName;
        }
        #endregion
    }
}