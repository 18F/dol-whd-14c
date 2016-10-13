using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;

namespace DOL.WHD.Section14c.Business.Services
{
    public class SaveService : ISaveService
    {
        private readonly ISaveRepository _repository;

        public SaveService(ISaveRepository repository)
        {
            _repository = repository;
        }

        public string GetSave(string userId, string EIN)
        {
            string saveState = null;
            var applicationSave = _repository.Get().SingleOrDefault(x => x.UserId == userId && x.EIN == EIN);
            if (applicationSave != null)
            {
                saveState = applicationSave.ApplicationState;
            }

            return saveState;
        }

        public void AddOrUpdate(string userId, string EIN, string state)
        {
            var applicationSave = new ApplicationSave
            {
                UserId = userId,
                EIN = EIN,
                ApplicationState = state
            };

            _repository.AddOrUpdate(applicationSave);
        }
    }
}
