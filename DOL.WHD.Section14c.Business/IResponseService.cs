using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business
{
    public interface IResponseService : IDisposable
    {
        IEnumerable<Response> GetResponses(string questionKey = null, bool onlyActive = true);
    }
}
