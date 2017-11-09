using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Log.DataAccess.Repositories;
using DOL.WHD.Section14c.Log.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class ActivityLogRepositoryMock: IActivityLogRepository
    {

        private bool _disposed;
        private readonly List<APIActivityLogs> _data;
        public bool AddShouldFail { get; set; } = false;
        public bool Disposed => _disposed;

        public ActivityLogRepositoryMock()
        {
            _data = new List<APIActivityLogs>
            {
                new APIActivityLogs {Id=1, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e02", EIN = "12-1234567", Exception = "My Test Exception 1", Level = "Fatal", Message="This a test message", User="test@test.com", UserId="12345", IsServiceSideLog=true, StackTrace=string.Empty, LogTime=DateTime.Now.ToString()},
                new APIActivityLogs {Id=2, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e03", EIN = "12-2234567", Exception = "My Test Exception 2", Level = "Error", Message="This a test message", User="test2@test.com", UserId="22345", IsServiceSideLog=true, StackTrace=string.Empty, LogTime=DateTime.Now.ToString()},
                new APIActivityLogs {Id=3, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e04", EIN = "12-3234567", Exception = "My Test Exception 3", Level = "Warn", Message="This a test message", User="test3@test.com", UserId="32345", IsServiceSideLog=true, StackTrace=string.Empty, LogTime=DateTime.Now.ToString()},
                new APIActivityLogs {Id=4, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e05", EIN = "12-4234567", Exception = "My Test Exception 4", Level = "Info", Message="This a test message", User="test4@test.com", UserId="42345", IsServiceSideLog=true, StackTrace=string.Empty, LogTime=DateTime.Now.ToString()},
                new APIActivityLogs {Id=5, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e06", EIN = "12-5234567", Exception = "My Test Exception 5", Level = "Debug", Message="This a test message", User="test5@test.com", UserId="52345", IsServiceSideLog=true, StackTrace=string.Empty, LogTime=DateTime.Now.ToString()},
                new APIActivityLogs {Id=6, CorrelationId="2edbc12f-4fd8-4fed-a848-b8bfff4d4e07", EIN = "12-6234567", Exception = "My Test Exception 6", Level = "Trace", Message="This a test message", User="test6@test.com", UserId="62345", IsServiceSideLog=true, StackTrace=string.Empty, LogTime=DateTime.Now.ToString()},
            };
        }

        public IQueryable<APIActivityLogs> GetAllLogs()
        {
            if (this.AddShouldFail)
            {
                return null;
            }
            return _data.AsQueryable();
        }

        public APIActivityLogs GetActivityLogByIDAsync(int id)
        {
            if (this.AddShouldFail)
            {
                return null;
            }

            return _data.AsQueryable().SingleOrDefault(x => x.Id == id);
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            _disposed = _disposed || disposing;
        }
    }
}
