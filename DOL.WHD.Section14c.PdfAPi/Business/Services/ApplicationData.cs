using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DOL.WHD.Section14c.PdfApi.Business
{
    [Serializable]
    [DataContract]
    public class ApplicationData
    {
        [DataMember]
        public string HtmlString { get; set; }

        [DataMember]
        public byte[] Buffer { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public List<string> FilePaths { get; set; }
    }
}