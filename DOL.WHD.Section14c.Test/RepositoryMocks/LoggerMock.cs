using DOL.WHD.Section14c.Log.Helpers;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Tracing;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class LoggerMock : ITraceWriter
    {
        // Define a public property for the message. 
        public string LogToOperationsCalledWithMessage { get; set; }

        /// <summary>
        /// Implementation of TraceWriter to trace the logs.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <param name="level"></param>
        /// <param name="traceAction"></param>
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            LogToOperationsCalledWithMessage = category;
        }
    }
}
