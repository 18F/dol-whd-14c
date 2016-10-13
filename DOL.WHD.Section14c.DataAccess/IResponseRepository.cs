using System;
using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IResponseRepository : IDisposable
    {
        IQueryable<Response> Get();
    }
}
