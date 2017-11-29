using System;
using System.Linq;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System.Collections.Generic;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IApplicationRepository : IDisposable
    {
        IEnumerable<ApplicationSubmission> Get();
        Task<int> AddAsync(ApplicationSubmission submission);
        Task<int> ModifyApplication(ApplicationSubmission submission);
        Task<int> SaveChangesAsync();
    }
}