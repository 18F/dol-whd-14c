using DOL.WHD.Section14c.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IUserActivityRepository : IDisposable
    {
        IEnumerable<UserActivity> Get();
        Task<int> AddAsync(UserActivity Activity);
        Task<int> SaveChangesAsync();

        void Add(UserActivity Activity);
        int SaveChanges();

    }
}
