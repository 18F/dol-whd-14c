using DOL.WHD.Section14c.Log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace DOL.WHD.Section14c.Log.Repositories
{
    public class ErrorLogRepository: IErrorLogRepository
    {
        private readonly ApplicationLogContext _dbContext;
        private Boolean Disposed;

        public ErrorLogRepository()
        {
            _dbContext = new ApplicationLogContext();
        }

        public IQueryable<APIErrorLogs> GetAllLogs()
        {
            return _dbContext.ErrorLogs.AsQueryable();
        }

        public async Task<APIErrorLogs> GetActivityLogByIDAsync(int id)
        {
            return await _dbContext.ErrorLogs.FindAsync(id);
        }

        public async Task<APIErrorLogs> AddLogAsync(APIErrorLogs entity)
        {
            _dbContext.ErrorLogs.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public void Dispose()
        {
            if (!Disposed)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    Disposed = true;
                }
            }
        }
    }
}