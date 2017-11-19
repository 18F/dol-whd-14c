using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DOL.WHD.Section14c.PdfApi.PdfHelper
{
    /// <summary>
    /// PDF Content Data
    /// </summary>
    [Serializable]
    [DataContract]
    public class PDFContentData
    {
        /// <summary>
        /// Html STring
        /// </summary>
        [DataMember]
        public List<string> HtmlString { get; set; }
        /// <summary>
        /// Byte Array
        /// </summary>
        [DataMember]
        public byte[] Buffer { get; set; }
        /// <summary>
        /// File Type
        /// </summary>
        [DataMember]
        public string Type { get; set; }
        /// <summary>
        /// File Name
        /// </summary>
        [DataMember]
        public string FileName { get; set; }
        /// <summary>
        /// File Location
        /// </summary>
        [DataMember]
        public List<string> FilePaths { get; set; }
    }
}