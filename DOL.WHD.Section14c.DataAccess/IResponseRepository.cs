using System;
using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IResponseRepository : IDisposable
    {
        IEnumerable<Response> GetResponses(string questionKey = null, bool onlyActive = true);
    }
}
