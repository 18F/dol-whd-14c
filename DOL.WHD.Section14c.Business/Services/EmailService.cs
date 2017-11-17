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
        /// <param name="application">
        /// Application Data
        /// </param>
        /// <param name="htmlString">
        /// Email template string
        /// </param>
        /// <param name="receiver">
        /// Email receiver
        /// </param>
        /// <returns>Key and value pair of the receiver and content</returns>
        public Dictionary<string, EmailContent> PrepareApplicationEmailContents(ApplicationSubmission application, string certificationTeamEmailBodyTemplate, string employerEmailBodyTemplate, EmailReceiver receivers)
        {
            string certificationTeamEmailBody = FillTemplate(application, certificationTeamEmailBodyTemplate);
            string employerEmailBody = FillTemplate(application, employerEmailBodyTemplate);
            string emailSubject = AppSettings.Get<string>("ApplicationSubmittedEmailSubject");
            var emails = new Dictionary<string, EmailContent>();

            if (receivers == EmailReceiver.CertificationTeam || receivers == EmailReceiver.Both)
            {
                emails.Add(Constants.CertificationEmailKey, new EmailContent()
                {
                    Body = certificationTeamEmailBody,
                    To = AppSettings.Get<string>("CertificationTeamEmailAddress"),
                    Subject = string.Format("{0} {1}", application?.Employer?.PhysicalAddress?.State, emailSubject)
                });
            }
            if (receivers == EmailReceiver.Employer || receivers == EmailReceiver.Both)
            {
                emails.Add(Constants.EmployerEmailKey, new EmailContent()
                {
                    Body = employerEmailBody,
                    To = application.ContactEmail,
                    Subject = emailSubject
                });
            }

            return emails;
        }

        /// <summary>
        /// Process email template 
        /// </summary>
        /// <param name="application">
        /// Application Data
        /// </param>
        /// <param name="templateText">
        /// Email Template
        /// </param>
        /// <returns>Template string the application data</returns>
        private static string FillTemplate(ApplicationSubmission application, string templateText)
        {
            Handlebars.RegisterHelper("formatDateTime", (writer, context, parameters) =>
            {
                if (parameters[0].GetType() == typeof(DateTime))
                {
                    DateTime dateTime = (DateTime)parameters[0];
                    writer.WriteSafeString(dateTime.ToString("d"));
                }
            });

            var template = Handlebars.Compile(templateText);
            return template(application);
        }
    }
}
