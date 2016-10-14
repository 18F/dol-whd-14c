using System;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.Business
{
    public interface ISaveService : IDisposable
    {
        ApplicationSave GetSave(string EIN);
        void AddOrUpdate(string EIN, string state);
    }
}
