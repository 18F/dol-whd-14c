using System.IO;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IFileRepository
    {
        void Upload(byte[] bytes, string fileName);

        MemoryStream Download(MemoryStream memoryStream, string fileName);
    }
}
