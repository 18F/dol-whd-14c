using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using DOL.WHD.Section14c.Common;
using System.Web.Http;
using System.Net;

namespace DOL.WHD.Section14c.Api.Providers
{
    /// <summary>
    /// A multipart memory stream provider that performs file type validateion
    /// based on the app config.
    /// </summary>
    public class RestrictedMultipartMemoryStreamProvider : MultipartMemoryStreamProvider
    {
        /// <summary>
        /// Get a stream from the HTTP request if the file type is supported
        /// </summary>
        /// <param name="parent">
        /// The HTTP content from the request
        /// </param>
        /// <param name="headers">
        /// The HTTP headers from the request
        /// </param>
        /// <returns></returns>
        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            var pattern = AppSettings.Get<string>("AllowedFileNamesRegex");
            var fileNameRegex = new Regex(pattern);
            var fileName = headers.ContentDisposition.FileName;
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            if (fileName == null || string.IsNullOrEmpty(pattern))
                return Stream.Null;

            // File type validation
            fileName = fileName.Replace("\"", string.Empty);
            var allowedFileType = fileNameRegex.IsMatch(fileName.ToLower());
            if (!allowedFileType)
            {                
                var fileExtension = Regex.Match(fileName, @"\..*").Value;

                response.Content = new StringContent(string.Format("The {0} file type is not supported.", 
                    string.IsNullOrEmpty(fileExtension) ? fileName : fileExtension.ToUpper()));

                throw new HttpResponseException(response);
            }            

            return base.GetStream(parent, headers);
        }
    }
}