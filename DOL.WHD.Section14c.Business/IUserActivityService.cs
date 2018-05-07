using DOL.WHD.Section14c.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business
{
    public interface IUserActivityService
    {
        IEnumerable<UserActivity> GetAllAccounts();

    }
}

