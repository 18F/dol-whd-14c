using System;
using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business
{
    public interface IStatusService : IDisposable
    {
        IEnumerable<Status> GetAllStatuses();
        Status GetStatus(int id);
    }
}
