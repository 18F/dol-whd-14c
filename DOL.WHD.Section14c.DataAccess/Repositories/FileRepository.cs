using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace DOL.WHD.Section14c.DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly string _rootFolder;

        public FileRepository(string rootFolder)
        {
            _rootFolder = rootFolder;
        }

        private const string RootFolderException = "RootFolder must be set.";

        public void Upload(MemoryStream memoryStream, string fileName)
        {
            var file = new FileInfo(_rootFolder + fileName);
            file.Directory?.Create();
            using (FileStream fileStream = new FileStream(_rootFolder + fileName, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fileStream);
            }
        }

        public MemoryStream Download(MemoryStream memoryStream, string fileName)
        {
            using (var fileSteam = new FileStream(_rootFolder + fileName, FileMode.Open))
            {
                fileSteam.CopyTo(memoryStream);
            }
            memoryStream.Position = 0;
            return memoryStream;

        }
    }
}
