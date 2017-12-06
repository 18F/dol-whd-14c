using System;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business
{
    public interface ISaveService : IDisposable
    {
        ApplicationSave GetSave(string applicationId);

        void AddOrUpdate(string EIN, string applicationId, Employer employer, string state);

        void Remove(string applicationId);
    }
}
