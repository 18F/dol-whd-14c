using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IEmployerRepository : IDisposable
    {
        IEnumerable<Employer> Get();
    }
}
