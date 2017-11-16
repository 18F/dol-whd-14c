using DOL.WHD.Section14c.Common;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.EmailApi.Helper;
using HandlebarsDotNet;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DOL.WHD.Section14c.Business.Helper;

namespace DOL.WHD.Section14c.Business.Services
{
    public class EmailService: IEmailService
    {
        /// <summary>
        /// Prepare Email Contents from html template
        /// </summary>
        /// <param name="emailContents"></param>
        /// <returns></returns>
        public Dictionary<string, EmailContent> PrepareApplicationEmailContents(ApplicationSubmission application, string htmlString, EmailReceiver receiver)
        {
            htmlString = ProcessApplicationData(application, htmlString);
            var certificationEmailContents = ParseEmailContent(htmlString, Constants.CertificationTeamSection);
            var employerEmailContents = ParseEmailContent(htmlString,Constants.EmployerSection);
            var data = new Dictionary<string, EmailContent>();
            // Prepare email contants based on the EmailReceiver value 
            if (receiver == EmailReceiver.CertificationTeam || receiver == EmailReceiver.Both)
                data.Add(Constants.CertificationEmailKey, certificationEmailContents);
            if (receiver == EmailReceiver.Employer || receiver == EmailReceiver.Both)
                data.Add(Constants.EmployerEmailKey, employerEmailContents);
           
            return data;         
        }

        /// <summary>
        /// Get Email contents from html template string
        /// </summary>
        /// <param name="htmlString"></param>
        /// <returns></returns>
        private static EmailContent ParseEmailContent(string htmlString, string tagName)
        {
            EmailContent content = new EmailContent();
            if (!string.IsNullOrEmpty(htmlString))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlString);
                doc.OptionFixNestedTags = true;

                HtmlNode nodes = doc.DocumentNode.Descendants().FirstOrDefault(x => x.Name.ToLower() == tagName.ToLower());

                foreach (HtmlNode node in nodes.Descendants())
                {
                    if(node.Name == "email-to")
                        content.To = node.InnerHtml;
                    if (node.Name == "email-subject")
                        content.Subject = node.InnerHtml;
                    if (node.Name == "email-body")
                        content.Body = node.InnerHtml;
                }
            }
            return content;
        }

        /// <summary>
        /// Replace key with value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ProcessApplicationData(ApplicationSubmission application, string htmlString)
        {
            Handlebars.RegisterHelper("certificationTeamEmailAddress", (writer, context, parameters) =>
            {
                writer.WriteSafeString(AppSettings.Get<string>("CertificationTeamEmailAddress"));
            });

            Handlebars.RegisterHelper("formatDateTime", (writer, context, parameters) =>
            {
                if (parameters[0].GetType() == typeof(DateTime))
                {
                    DateTime dateTime = (DateTime)parameters[0];
                    writer.WriteSafeString(dateTime.ToString("d"));
                }
            });
            Handlebars.RegisterHelper("newline", (writer, context, parameters) =>
            {
                writer.WriteSafeString(Environment.NewLine);
            });

            var template = Handlebars.Compile(htmlString);
            return template(application);
        }
    }
}
