using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business
{
    public interface IAttachmentSupportedFileTypesService : IDisposable
    {
        IEnumerable<AttachmentSupportedFileTypes> GetAllSupportedFileTypes();
        AttachmentSupportedFileTypes GetSupportedFileTypes(int id);
    }
}
