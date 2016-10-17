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

        public ApplicationSave GetSave(string EIN)
        {
            return _repository.Get().SingleOrDefault(x => x.EIN == EIN);
        }

        public void AddOrUpdate(string EIN, string state)
        {
            var applicationSave = GetSave(EIN);
            if (applicationSave != null)
            {
                // if save already exists just update the state
                applicationSave.ApplicationState = state;

                _repository.SaveChanges();
            }
            else
            {
                applicationSave = new ApplicationSave
                {
                    EIN = EIN,
                    ApplicationState = state
                };

                _repository.Add(applicationSave);
            }
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
