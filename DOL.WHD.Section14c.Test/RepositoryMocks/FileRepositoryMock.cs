using System.Collections.Generic;
using System.IO;
using DOL.WHD.Section14c.DataAccess;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    class FileRepositoryMock : IFileRepository
    {
        private Dictionary<string, MemoryStream> _fileRepo;

        FileRepositoryMock()
        {
            _fileRepo = new Dictionary<string, MemoryStream>();
        }

        public void Upload(byte[] bytes, string fileName)
        {
            _fileRepo.Add( fileName, new MemoryStream(bytes));
        }

        public MemoryStream Download(MemoryStream memoryStream, string fileName)
        {
            _fileRepo.TryGetValue(fileName, out memoryStream);
            return memoryStream;
        }
    }
}
