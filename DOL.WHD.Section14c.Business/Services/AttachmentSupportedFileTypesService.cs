using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business.Services
{
    public class AttachmentSupportedFileTypesService : IAttachmentSupportedFileTypesService
    {
        private readonly IAttachmentSupportedFileTypesRepository _repository;
        public AttachmentSupportedFileTypesService(IAttachmentSupportedFileTypesRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<AttachmentSupportedFileTypes> GetAllSupportedFileTypes()
        {
            return _repository.Get().ToList();
        }

        public AttachmentSupportedFileTypes GetSupportedFileTypes(int id)
        {
            return _repository.Get().SingleOrDefault(x => x.Id == id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
