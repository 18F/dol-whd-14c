using System;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.Business
{
    public interface ISaveService : IDisposable
    {
        ApplicationSave GetSave(string applicationId);

        void AddOrUpdate(string EIN, string applicationId, string state);

        void Remove(string applicationId);
    }
}
