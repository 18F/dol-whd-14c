using System.IO;

namespace DOL.WHD.Section14c.DataAccess
{
    public interface IFileRepository
    {
        void Upload(MemoryStream memoryStream, string fileName);

        MemoryStream Download(MemoryStream memoryStream, string fileName);
    }
}
