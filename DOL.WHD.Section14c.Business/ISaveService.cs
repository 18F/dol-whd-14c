using System;

namespace DOL.WHD.Section14c.Business
{
    public interface ISaveService : IDisposable
    {
        string GetSave(string userId, string EIN);
        void AddOrUpdate(string userId, string EIN, string state);
    }
}
