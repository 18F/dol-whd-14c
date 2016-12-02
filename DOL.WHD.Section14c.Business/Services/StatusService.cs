using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Business.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _repository;
        public StatusService(IStatusRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            return _repository.Get().ToList();
        }

        public Status GetStatus(int id)
        {
            return _repository.Get().SingleOrDefault(x => x.Id == id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
