using DOL.WHD.Section14c.Common;
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

        public void Upload(byte[] bytes, string fileName)
        {
            var file = new FileInfo(_rootFolder + fileName);
            var bufferSize = AppSettings.Get<int>("AttachmentFileDownloadBufferSize");
            file.Directory?.Create();
            using (FileStream fileStream = new FileStream(_rootFolder + fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[bufferSize];

                int incomingOffset = 0;

                while (incomingOffset < bytes.Length)
                {
                    int length = Math.Min(buffer.Length, bytes.Length - incomingOffset);
                    Buffer.BlockCopy(bytes, incomingOffset, buffer, 0, length);
                    incomingOffset += length;

                    // Write file
                    fileStream.Write(buffer, 0, length);
                }
            }
        }

        public MemoryStream Download(MemoryStream memoryStream, string fileName)
        {
            var bufferSize = AppSettings.Get<int>("AttachmentFileDownloadBufferSize");

            using (var fileSteam = new FileStream(_rootFolder + fileName, FileMode.Open))
            {
                fileSteam.CopyTo(memoryStream, bufferSize);
            }
            memoryStream.Position = 0;
            return memoryStream;

        }
    }
}
