using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Identity;

namespace DOL.WHD.Section14c.Business.Services
{
    public class SaveService : ISaveService
    {
        private readonly ISaveRepository _repository;
        private bool Disposed = false;

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
            var applicationSave = new ApplicationSave {EIN = EIN, ApplicationState = state};
            _repository.AddOrUpdate(applicationSave);
        }

        public void Remove(string EIN)
        {
            _repository.Remove(EIN);
            _repository.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _repository.Dispose();
                Disposed = true;
            }
        }
    }
}
