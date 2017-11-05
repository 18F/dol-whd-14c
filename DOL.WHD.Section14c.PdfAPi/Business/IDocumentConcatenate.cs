using DOL.WHD.Section14c.PdfApi.PdfHelper;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.PdfApi.Business
{
    public interface IDocumentConcatenate
    {
        byte[] Concatenate(List<ApplicationData> data);
    }
}
