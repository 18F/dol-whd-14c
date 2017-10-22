using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    [TestClass]
    public class AttachmentSupportedFileTypesRepositoryMock : IAttachmentSupportedFileTypesRepository
    {
        private bool _disposed;
        private readonly List<AttachmentSupportedFileTypes> _data;

        public bool Disposed => _disposed;

        public AttachmentSupportedFileTypesRepositoryMock()
        {
            _data = new List<AttachmentSupportedFileTypes>
            {
                new AttachmentSupportedFileTypes {Id=1, Name = "doc", IsActive = true },
                new AttachmentSupportedFileTypes {Id=2, Name = "docx", IsActive = true },
                new AttachmentSupportedFileTypes {Id=3, Name = "xls", IsActive = true },
                new AttachmentSupportedFileTypes {Id=4, Name = "pdf", IsActive = true },
                new AttachmentSupportedFileTypes {Id=5, Name = "jpg", IsActive = true },
                new AttachmentSupportedFileTypes {Id=6, Name = "jpeg", IsActive = true },
                new AttachmentSupportedFileTypes {Id=7, Name = "png", IsActive = true },
                new AttachmentSupportedFileTypes {Id=8, Name = "csv", IsActive = true }
        };
        }

        public IQueryable<AttachmentSupportedFileTypes> Get()
        {
            return _data.AsQueryable();
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
