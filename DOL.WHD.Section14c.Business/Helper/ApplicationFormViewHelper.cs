using DOL.WHD.Section14c.Business.Extensions;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using HandlebarsDotNet;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business.Helper
{
    public class ApplicationFormViewHelper
    {
        /// <summary>
        /// Set Application Template Main 
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handlebarsTemplate"></param>
        /// <returns></returns>
        public static string PopulateHtmlTemplateWithApplicationData(ApplicationSubmission application, string handlebarsTemplate)
        {
            Handlebars.RegisterHelper("boolOrString", (writer, context, parameters) =>
            {
                string value = parameters[0].ToString();
                switch (value.ToLower())
                {
                    case "true":
                        value = "Yes";
                        break;

                    case "false":
                        value = "No";
                        break;

                    case "":
                        value = "NOT PROVIDED";
                        break;
                }
                writer.WriteSafeString(value);
            });

            Handlebars.RegisterHelper("formatAddress", (writer, context, parameters) =>
            {
                if (parameters[0].GetType() == typeof(Address))
                {
                    Address value = (Address)parameters[0];
                    writer.WriteSafeString($"{value.StreetAddress}<div>{value.City}, {value.State} {value.ZipCode}</div><div>{value.County}</div>");
                }
            });

            Handlebars.RegisterHelper("formatAttachment", (writer, context, parameters) =>
            {
                if (parameters[0].GetType() == typeof(Attachment))
                {
                    Attachment value = (Attachment)parameters[0];
                    writer.WriteSafeString($"<div>{value.OriginalFileName}</div>");
                }
            });

            var template = Handlebars.Compile(handlebarsTemplate);
            return template(application);
        }
    }
}
