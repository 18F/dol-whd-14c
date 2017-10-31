using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.PdfApi.Business
{
    public interface IDocumentConcatenate
    {
        string Concatenate(List<byte[]> documentContentByteArrays);
        string Concatenate(List<string> filePaths);
    }
}
