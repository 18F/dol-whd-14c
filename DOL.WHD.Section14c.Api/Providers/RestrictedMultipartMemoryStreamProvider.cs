using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using DOL.WHD.Section14c.Common;

namespace DOL.WHD.Section14c.Api.Providers
{
    public class RestrictedMultipartMemoryStreamProvider : MultipartMemoryStreamProvider
    {
        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            var pattern = AppSettings.Get<string>("AllowedFileNamesRegex");
            var fileNameRegex = new Regex(pattern);
            var fileName = headers.ContentDisposition.FileName;

            if (fileName == null || string.IsNullOrEmpty(pattern))
                return Stream.Null;

            return fileNameRegex.IsMatch(headers.ContentDisposition.FileName.Replace("\"", "")) ? base.GetStream(parent, headers) : Stream.Null;
        }
    }
}