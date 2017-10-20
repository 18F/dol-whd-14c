using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.Repositories;
using DOL.WHD.Section14c.Log.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using DOL.WHD.Section14c.Log;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class ErrorLogRepositoryMock: IErrorLogRepository
    {
        private bool _disposed;
        private readonly List<APIErrorLogs> _data;

        public bool Disposed => _disposed;


        public ErrorLogRepositoryMock()
        {
            _data = new List<APIErrorLogs>
            {
                new APIErrorLogs {Id=1, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e02", EIN = "12-1234567", Exception = "My Test Exception 1", Level = "Fatal", Message="This a test message", User="test@test.com", UserId="12345"},
                new APIErrorLogs {Id=2, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e03", EIN = "12-2234567", Exception = "My Test Exception 2", Level = "Error", Message="This a test message", User="test2@test.com", UserId="22345"},
                new APIErrorLogs {Id=3, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e04", EIN = "12-3234567", Exception = "My Test Exception 3", Level = "Warn", Message="This a test message", User="test3@test.com", UserId="32345"},
                new APIErrorLogs {Id=4, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e05", EIN = "12-4234567", Exception = "My Test Exception 4", Level = "Info", Message="This a test message", User="test4@test.com", UserId="42345"},
                new APIErrorLogs {Id=5, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e06", EIN = "12-5234567", Exception = "My Test Exception 5", Level = "Debug", Message="This a test message", User="test5@test.com", UserId="52345"},
                new APIErrorLogs {Id=6, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e07", EIN = "12-6234567", Exception = "My Test Exception 6", Level = "Trace", Message="This a test message", User="test6@test.com", UserId="62345"},
            };
        }

        public IQueryable<APIErrorLogs> GetAllLogs()
        {
            return _data.AsQueryable();
        }

        public LogDetails AddLog(LogDetails entity)
        {
            if (entity != null)
            {
                LogEventInfo eventInfo = new LogEventInfo();

                eventInfo.Properties["EIN"] = string.IsNullOrEmpty(entity.EIN) ? string.Empty : entity.EIN;
                eventInfo.LoggerName = "NLog";
                if (string.IsNullOrEmpty(entity.Message))
                {
                    throw new ArgumentException("Message cannot be null or empty string", "Log Message");
                }

                eventInfo.Message = entity.Message;

                if (!string.IsNullOrEmpty(entity.Exception))
                {
                    eventInfo.Exception = new Exception(entity.Exception);
                    eventInfo.SetStackTrace((new System.Diagnostics.StackTrace(new Exception(entity.Exception), false)), 1);
                }


                eventInfo.Level = LogLevel.FromString(entity.Level);
                eventInfo.Properties["UserId"] = entity.UserId;
                eventInfo.Properties["UserName"] = entity.User;

                eventInfo.Properties[Constants.IsServiceSideLog] = true;
            }
            return entity;
        }

        public void Dispose()
        {
            _disposed = true;
        }
    
    }
}
