using DOL.WHD.Section14c.PdfApi.Business;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class DocumentConcatenateMock: IDocumentConcatenate
    {
        private List<ApplicationData> applicationData;

        DocumentConcatenateMock()
        {
            applicationData = new List<ApplicationData>();
        }

        public byte[] Concatenate(List<ApplicationData> data)
        {
            return null;
        }
    }
}
