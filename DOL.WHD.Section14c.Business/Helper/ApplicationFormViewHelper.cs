using DOL.WHD.Section14c.Business.Extensions;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
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
        /// <param name="htmlString"></param>
        /// <returns></returns>
        public static string PopulateApplicationData(ApplicationSubmission application, string htmlString)
        {
            string tempString = htmlString;
            if (!string.IsNullOrEmpty(htmlString))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlString);
                doc.OptionFixNestedTags = true;
                
                doc = SetTemplateAnswerFields(application, doc);
                doc = SetTemplateAnswerListField(application, doc);
                doc = SetTemplateAnswerTableField(application, doc);
                doc = SetTemplateWageDataField(application, doc);
                doc = SetTemplateWorkerSiteField(application, doc);

                tempString = doc.DocumentNode.InnerHtml;
            }
            return tempString;
        }

        /// <summary>
        /// Set Template Answer Fields based on the answer object
        /// </summary>
        /// <param name="application"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static HtmlDocument SetTemplateAnswerFields(ApplicationSubmission application, HtmlDocument doc)
        {
            List<HtmlNode> tdNodes = FindHtmlNodeByTagAndAttribute(doc, "answer-field", "answer");

            foreach (HtmlNode node in tdNodes)
            {
                var isAddressField = node.GetAttributeValue("address-field", "");
                var isAttachementField = node.GetAttributeValue("attachment-field", "");
                var answerAttribute = node.GetAttributeValue("answer", "");

                if (!string.IsNullOrEmpty(answerAttribute))
                {
                    string stringValue = string.Empty;
                    // Set Addesss Type
                    if (isAddressField.ToLower() == "true")
                    {
                        stringValue = GetAddressPropertyValue(application, answerAttribute);
                    }
                    else
                    {   // Set Attachment 
                        if(isAttachementField.ToLower() == "true")
                            stringValue = GetAttachmentPropertyValue(application, node, answerAttribute);
                        else
                            stringValue = GetStringPropertyValue(application, node, answerAttribute);
                    }
                    // Replace html content with form values
                    node.InnerHtml = string.Format("<p>{0}<div class='subtextanswer'>{1}</div></p>", node.InnerHtml, YesNoField(stringValue));
                }
            }
            return doc;
        }

        /// <summary>
        /// Set html table value
        /// </summary>
        /// <param name="application"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static HtmlDocument SetTemplateAnswerTableField (ApplicationSubmission application, HtmlDocument doc)
        {
            List<HtmlNode> tdNodes = FindHtmlNodeByTagAndAttribute(doc, "tbody", "ng-repeat"); 

            foreach (HtmlNode node in tdNodes)
            {
                var answerAttribute = node.GetAttributeValue("ng-repeat", "");
                if (!string.IsNullOrEmpty(answerAttribute))
                {
                    if(answerAttribute == "WIOA.WIOAWorkers")
                        node.InnerHtml = SetTemplateWIOATableValue(application, answerAttribute);
                }
            }
            return doc;
        }

        /// <summary>
        /// Set repeat value for list type
        /// </summary>
        /// <param name="application"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static HtmlDocument SetTemplateAnswerListField(ApplicationSubmission application, HtmlDocument doc)
        {
            List<HtmlNode> tdNodes = FindHtmlNodeByTagAndAttribute(doc, "answer-field-list", "answer");

            foreach (HtmlNode node in tdNodes)
            {
                var answerAttribute = node.GetAttributeValue("answer", "");
                if (!string.IsNullOrEmpty(answerAttribute))
                {
                    if (answerAttribute == "EstablishmentType")
                        node.InnerHtml = GetEstablishmenttypePropertyValue(application, node, answerAttribute);
                    if (answerAttribute == "Employer.ProvidingFacilitiesDeductionType")
                        node.InnerHtml = GetProvidingFacilitiesDeductionTypePropertyValue(application, node, answerAttribute);
                }
            }
            return doc;
        }

        /// <summary>
        /// Set Html Template Wage Data Section
        /// </summary>
        /// <param name="application"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static HtmlDocument SetTemplateWageDataField(ApplicationSubmission application, HtmlDocument doc)
        {
            HtmlNode wageDataHourly = FindHtmlNodeByTagAndAttribute(doc, "div", "wage-data-hourly").FirstOrDefault();
            HtmlNode wagedataPieceRate = FindHtmlNodeByTagAndAttribute(doc, "div", "wage-data-PieceRate").FirstOrDefault();
            // Set Hourly Section
            if (wageDataHourly != null)
            {
                GetHourlyWageInfoPropertyValue(application.HourlyWageInfo, wageDataHourly);
            }
            // Set Piece Rate section
            if (wagedataPieceRate != null)
            {
                GetPieceRateWageInfoPropertyValue(application.PieceRateWageInfo, wagedataPieceRate);
            }

            return doc;
        }

        /// <summary>
        /// Return string value from Database based on the property
        /// </summary>
        /// <param name="application"></param>
        /// <param name="node"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string GetStringPropertyValue(ApplicationSubmission application, HtmlNode node, string property)
        {
            string value = string.Empty;
            try
            {
                value = GetPropertyValueExtensions.GetStringPropValue<string>(application, property);
                // Get default value if it exist
                if (string.IsNullOrEmpty(value))
                {
                    value = node.GetAttributeValue("defaultValue", "");
                }
            }
            catch (Exception e)
            {
                // Unable to find value
            }
            return value;
        }

        /// <summary>
        /// Return Establishment type from sibmitted dataset
        /// </summary>
        /// <param name="application"></param>
        /// <param name="node"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string GetEstablishmenttypePropertyValue(ApplicationSubmission application, HtmlNode node, string property)
        {
            string value = string.Empty;
            try
            {
                var returnValue = GetPropertyValueExtensions.GetPropValue<ICollection<ApplicationSubmissionEstablishmentType>>(application, property);
                foreach(var item in returnValue) {
                    value = string.Format("{0}<li>{1}</li>", value, item.EstablishmentType.Display);
                }
            }
            catch (Exception e)
            {
                // Unable to find value
            }
            return value;
        }

        /// <summary>
        /// Return Providing Facilities Deduction data type from sibmitted dataset
        /// </summary>
        /// <param name="application"></param>
        /// <param name="node"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string GetProvidingFacilitiesDeductionTypePropertyValue(ApplicationSubmission application, HtmlNode node, string property)
        {
            string value = string.Empty;
            try
            {
                var returnValue = GetPropertyValueExtensions.GetPropValue<ICollection<EmployerInfoProvidingFacilitiesDeductionType>>(application, property);
                foreach (var item in returnValue)
                {
                    value = string.Format("{0}<li>{1}</li>", value, item.ProvidingFacilitiesDeductionType.Display);
                }
            }
            catch (Exception e)
            {
                // Unable to find value
            }
            return value;
        }

        /// <summary>
        /// Set Wage Piece Rate Section 
        /// </summary>
        /// <param name="wageInfo"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetPieceRateWageInfoPropertyValue(PieceRateWageInfo wageInfo, HtmlNode node)
        {
            string value = string.Empty;
            try
            {
                HtmlNode wageDataRepeat = FindHtmlNodeChildrenByTagAndAttribute(node, "div", "wage-data-repeat").FirstOrDefault();
                List<HtmlNode> nodes = FindHtmlNodeChildrenByTagAndAttribute(wageDataRepeat, "wage-data-answer-repeat-field", "answer");

                // Hide Remove Html content from application form if data is null
                if (wageInfo?.MostRecentPrevailingWageSurvey?.SourceEmployers == null)
                    return wageDataRepeat.InnerHtml = string.Empty;
                // Update Wage Data Source Employers
                foreach (var employee in wageInfo?.MostRecentPrevailingWageSurvey?.SourceEmployers)
                {
                    foreach (var item in nodes)
                    {
                        var answerAttribute = item.GetAttributeValue("answer", "");
                        var returnValue =GetPropertyValueExtensions.GetStringPropValue<string>(employee, answerAttribute);
                        item.InnerHtml = string.Format("{0}<div>{1}</div>", item.InnerHtml, returnValue);
                    }
                }
            }
            catch (Exception e)
            {
                // Unable to find value
            }
            return value;
        }

        /// <summary>
        /// Set Wage Data Hourly Section
        /// </summary>
        /// <param name="wageInfo"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetHourlyWageInfoPropertyValue(HourlyWageInfo wageInfo, HtmlNode node)
        {
            string value = string.Empty;
            try
            {
                HtmlNode wageDataRepeat = FindHtmlNodeChildrenByTagAndAttribute(node, "div", "wage-data-repeat").FirstOrDefault();
                List<HtmlNode> nodes = FindHtmlNodeChildrenByTagAndAttribute(wageDataRepeat, "wage-data-answer-repeat-field", "answer");

                // Hide Remove Html content from application form if data is null
                if (wageInfo?.MostRecentPrevailingWageSurvey?.SourceEmployers == null)
                    return wageDataRepeat.InnerHtml = string.Empty;

                foreach (var employee in wageInfo?.MostRecentPrevailingWageSurvey?.SourceEmployers)
                {
                    foreach (var item in nodes)
                    {
                        var answerAttribute = item.GetAttributeValue("answer", "");
                        var returnValue = GetPropertyValueExtensions.GetStringPropValue<string>(employee, answerAttribute);
                        item.InnerHtml = string.Format("{0}<div>{1}</div>", item.InnerHtml, returnValue);
                    }
                }
            }
            catch (Exception e)
            {
                // Unable to find value
            }
            return value;
        }

        /// <summary>
        /// Update Template Address Property
        /// </summary>
        /// <param name="application"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string GetAddressPropertyValue(ApplicationSubmission application, string property)
        {
            string value = string.Empty;
            try
            {
                var address = GetPropertyValueExtensions.GetPropValue<Address>(application, property);
                value = string.Format("{0}<div>{1}{2}{3}</div><br/><div>{4}</div>", 
                                    address?.StreetAddress != null ? address.StreetAddress: string.Empty, 
                                    address?.City != null ? address.City : string.Empty, 
                                    address?.State != null ? ", " + address.State : string.Empty, 
                                    address?.ZipCode != null ? address.ZipCode : string.Empty, 
                                    address?.County != null ? "County: " + address.County : string.Empty);
            }
            catch (Exception e)
            {
                // Unable to find value
            }
            return value;
        }

        /// <summary>
        /// Update Template Attachement Property
        /// </summary>
        /// <param name="application"></param>
        /// <param name="node"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string GetAttachmentPropertyValue(ApplicationSubmission application, HtmlNode node, string property)
        {
            string value = string.Empty;
            try
            {
                var attachment = GetPropertyValueExtensions.GetPropValue<Attachment>(application, property);
                value = string.Format("{0}<div>{1}</div>",node.InnerHtml, attachment.OriginalFileName);
            }
            catch (Exception e)
            {
                // Unable to find value
            }
            return value;
        }

        /// <summary>
        /// Set WIOA Section Data
        /// </summary>
        /// <param name="application"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string SetTemplateWIOATableValue(ApplicationSubmission application, string property)
        {
            string template = string.Empty;

            // Get WIOA Workers
            var wioaWorkers = GetPropertyValueExtensions.GetPropValue<ICollection<WIOAWorker>>(application, property);
            foreach (var worker in wioaWorkers)
            {
                template += string.Format("<tr style='text-align:center'><td scope='row'>{0}</td>", worker.FullName);
                template += string.Format("<td>{0}</td></tr>", worker.WIOAWorkerVerified.Display);
            }

            return template;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static HtmlDocument SetTemplateWorkerSiteField(ApplicationSubmission application, HtmlDocument doc)
        {
            // Get Work Site Section Html
            HtmlNode workSitesNode = FindHtmlNodeByTagAndAttribute(doc, "div", "worksites-section").FirstOrDefault();

            // Get Work Site Table
            var allWorkSitesAndEmployeesTableNode = FindHtmlNodeChildrenByTagAndAttribute(workSitesNode, "tbody", "ng-repeat").FirstOrDefault();
            // Get Work Site Html
            var WorkSiteTableNode = FindHtmlNodeChildrenByTagAndAttribute(workSitesNode, "div", "worksites-sub", "answer").FirstOrDefault();
            // Get Emplayee Section Html
            var employeesTableNode = FindHtmlNodeChildrenByTagAndAttribute(workSitesNode, "div", "employee-sub", "answer").FirstOrDefault();
            // Get repeate Section for Work Site
            var answerAttribute = allWorkSitesAndEmployeesTableNode?.GetAttributeValue("ng-repeat", "");

            if (!string.IsNullOrEmpty(answerAttribute))
            {
                var workerList = GetPropertyValueExtensions.GetPropValue<ICollection<WorkSite>>(application, answerAttribute);
                // Set Work Site Table Value
                allWorkSitesAndEmployeesTableNode.InnerHtml = SetTemplateAllWorkSitesEmployeesTable(workerList);
                // Set Work Site and Employee Section Data
                // Create Section For Each Work Site
                // Create Section For Each Employee Section
                var workerSiteBaseHtml = WorkSiteTableNode.InnerHtml;
                var employeeBaseHtml = employeesTableNode.InnerHtml;
                foreach (var workSite in workerList)
                {
                    // Reset back to base 
                    WorkSiteTableNode.InnerHtml = workerSiteBaseHtml;
                    workSitesNode.InnerHtml += WorkSitesTable(workSite, WorkSiteTableNode);
                    foreach (var employee in workSite.Employees)
                    {
                        employeesTableNode.InnerHtml = employeeBaseHtml;
                        workSitesNode.InnerHtml += EmployeesTable(employee, employeesTableNode);
                    }
                }
                // Work Site Content Section Used to Create Each Repeat Section for Work Site and Employee Section
                // Remove Html Template Section After Work Site & Employee Section Completed
                var worksiteTemplate = FindHtmlNodeChildrenByTagAndAttribute(workSitesNode, "div", "worksites-content").FirstOrDefault();
                worksiteTemplate.InnerHtml = string.Empty;
            }
            return doc;
        }

        /// <summary>
        /// Set Work Site and Employee Main Table
        /// </summary>
        /// <param name="workerSites"></param>
        /// <returns></returns>
        private static string SetTemplateAllWorkSitesEmployeesTable(ICollection<WorkSite> workerSites)
        {
            string template = string.Empty;

            foreach (var worker in workerSites)
            {
                template += string.Format("<tr style='text-align:center'><td scope='row'>{0}</td>", worker.Name);
                template += string.Format("<td>{0}</td></tr>", String.Join("<br/>", worker.Employees.Select(x=> x.Name).ToArray()));
            }

            return template;
        }

        /// <summary>
        /// Set Work Site Sub Table Section
        /// </summary>
        /// <param name="workerSite"></param>
        /// <param name="WorkSitesAndEmployeesTableNode"></param>
        /// <returns></returns>
        private static string WorkSitesTable(WorkSite workerSite, HtmlNode WorkSitesAndEmployeesTableNode)
        {
            var WorkSitesAndEmployeesFields = FindHtmlNodeChildrenByTagAndAttribute(WorkSitesAndEmployeesTableNode, "worksites-sub-field", "answer");
            
            foreach (HtmlNode node in WorkSitesAndEmployeesFields)
            {
                var answerAttribute = node.GetAttributeValue("answer", "");
                var stringValue = GetPropertyValueExtensions.GetStringPropValue<string>(workerSite, answerAttribute);
                node.InnerHtml = string.Format("<p>{0}<div class='subtextanswer'>{1}</div></p>", node.InnerHtml, YesNoField(stringValue));
            }
            return WorkSitesAndEmployeesTableNode.InnerHtml;
        }

        /// <summary>
        /// Set Work Site Employee Table Section
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="employeesTableNode"></param>
        /// <returns></returns>
        private static string EmployeesTable(Employee employee, HtmlNode employeesTableNode)
        {
            var employeesFields = FindHtmlNodeChildrenByTagAndAttribute(employeesTableNode, "td", "employees-sub-field", "answer");

            foreach (HtmlNode node in employeesFields)
            {
                var answerAttribute = node.GetAttributeValue("answer", "");
                var stringValue = GetPropertyValueExtensions.GetStringPropValue<string>(employee, answerAttribute);
                node.InnerHtml = string.Format("{0}", YesNoField(stringValue));
            }
            return employeesTableNode.InnerHtml;
        }

        /// <summary>
        /// Find and return list of Html Nodes
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="htmlTag"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private static List<HtmlNode> FindHtmlNodeByTagAndAttribute(HtmlDocument doc, string htmlTag, string attribute)
        {
            return doc?.DocumentNode?.Descendants()?.Where(x => x.Name == htmlTag && x.Attributes.Contains(attribute)).ToList();
        }

        /// <summary>
        /// Find and return List of html nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="htmlTag"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private static List<HtmlNode> FindHtmlNodeChildrenByTagAndAttribute(HtmlNode node, string htmlTag, string attribute)
        {
            return node?.Descendants()?.Where(x => x.Name == htmlTag && x.Attributes.Contains(attribute)).ToList();
        }

        /// <summary>
        /// Find and return List of html nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="htmlTag"></param>
        /// <param name="attribute"></param>
        /// <param name="attribute2"></param>
        /// <returns></returns>
        private static List<HtmlNode> FindHtmlNodeChildrenByTagAndAttribute(HtmlNode node, string htmlTag, string attribute, string attribute2)
        {
            return node?.Descendants()?.Where(x => x.Name == htmlTag && x.Attributes.Contains(attribute) && x.Attributes.Contains(attribute2)).ToList();
        }

        /// <summary>
        /// Replace Boolean with Yes and No Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string YesNoField (string value)
        {
            string temp = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                switch (value.ToLower())
                {
                    case "false":
                        temp = "No";
                        break;
                    case "true":
                        temp = "Yes";
                        break;
                    default:
                        temp = value;
                        break;
                }
            }
            return temp;
        }
    }
}
