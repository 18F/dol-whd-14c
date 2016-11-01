using System;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IApplicationRepository : IDisposable
    {
        IQueryable<ApplicationSubmission> Get();
        Task<int> AddAsync(ApplicationSubmission submission);
        Task<int> SaveChangesAsync();
    }
}