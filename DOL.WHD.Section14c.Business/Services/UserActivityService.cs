using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Identity;
using DOL.WHD.Section14c.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business.Services
{
    public class UserActivityService: IUserActivityService
    {
        private readonly IUserActivityRepository _UserActivityRepository;
        private ApplicationUserManager _userManager;

        public UserActivityService(IUserActivityRepository UserActivityRepository)
        {
            _UserActivityRepository = UserActivityRepository;
        }

        public IEnumerable<UserActivity> GetAllAccounts()
        {           
            return _UserActivityRepository.Get().ToList();
        }

    }
}
