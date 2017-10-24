using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DOL.WHD.Section14c.Common;

namespace DOL.WHD.Section14c.Business.Services
{
    public class AttachmentSupportedFileTypesService : IAttachmentSupportedFileTypesService
    {
        public IEnumerable<string> GetAllSupportedFileTypes()
        {
            String[] types = null;
            try
            {
                var supportedFileTypesPattern = AppSettings.Get<string>("AllowedFileNamesRegex");

                if (!string.IsNullOrEmpty(supportedFileTypesPattern))
                {
                    var resultString = Regex.Match(supportedFileTypesPattern, @"(?<=\().+?(?=\))").Value;
                    types = resultString.Split('|');
                }
            }
            catch(Exception ex)
            {
                // To Do throw API Business exception for logging and error handling
            }
            return types;
        }     
    }
}
