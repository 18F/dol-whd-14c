using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class AttachmentRepositoryMock : IAttachmentRepository
    {
        private bool _disposed;
        private readonly Dictionary<string, Attachment> _data;

        public bool Disposed => _disposed;

        public AttachmentRepositoryMock()
        {
            _data = new Dictionary<string, Attachment>();
            Add(new Attachment
            {
                EIN = "30-1234567"
            });
        }

        public void Dispose()
        {
            _disposed = true;
        }

        public IQueryable<Attachment> Get()
        {
            return _data.Values.AsQueryable();
        }

        public void Add(Attachment attachment)
        {
            _data.Add(attachment.EIN, attachment);
        }

        public int SaveChanges()
        {
            return 1;
        }
    }
}
