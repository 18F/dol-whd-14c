using System;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface ISaveRepository : IDisposable
    {
        IQueryable<ApplicationSave> Get();

        void Add(ApplicationSave applicationSave);

        int SaveChanges();

        IQueryable<Attachment> GetAttachments();
    }
}
