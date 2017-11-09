using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using DOL.WHD.Section14c.Domain.Models.Identity;
using DOL.WHD.Section14c.Test.RepositoryMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DOL.WHD.Section14c.PdfApi.PdfHelper;
using System.Collections.Generic;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Test.Business
{
    [TestClass]
    public class ApplicationFormTest
    {
        private readonly IFileRepository _fileRepositoryMock;
        private readonly IAttachmentRepository _attachmentRepositoryMock;
        private ApplicationSubmission application;
        private string htmlContent = @"<html class='ng-scope' lang='en'>
<body>
    <div class='admin-page'>
        <div class='dol-form-section-title'>
            <h2>Representations and Written Assurances</h2>
        </div>        
        <answer-field answer='Signature.FullName'>
            <span class='subtext'>Full Name</span>
        </answer-field>
    </div>
    <div class='admin-page'>
         <ul class='usa-unstyled-list'>            
            <answer-field-list answer='EstablishmentType'>
                <li ng-repeat='establishmentTypeObj in appData.establishmentType'>
                    {{ establishmentTypeObj.establishmentType.display }}
                </li>
            </answer-field-list>
        </ul>
        <answer-field answer='ContactName'>
            <span class='subtext'>Full Name</span>
        </answer-field>

        <answer-field answer='ContactPhone'>
            <span class='subtext'>Telephone Number</span>
        </answer-field>
    </div>
    <div class='admin-page'>
        <div class='dol-form-section-title'>
            <h2>Employer</h2>
        </div>

        <h3>Employer Information</h3>
        <hr>
        <div ng-show='Employer.TakeCreditForCosts'>
            <answer-field answer=''''>
                <span class='subtext'>Which type of deduction(s) did the employer take?</span>
            </answer-field>

            <ul class='usa-unstyled-list'>
                <answer-field-list answer='Employer.ProvidingFacilitiesDeductionType'>
                    <li ng-repeat='Deduction in Employer.ProvidingFacilitiesDeductionType'>
                        {{ Deduction.ProvidingFacilitiesDeductionType.Display }}
                    </li>
                </answer-field-list>
            </ul>
        </div>

        <answer-field answer='Employer.TemporaryAuthority'>
            <span class='subtext'>Is this a request for Temporary Authority?</span>
        </answer-field>
		<answer-field answer='Employer.SCAAttachment' attachment-field='true' ng-show='Employer.ScaId === Constants.Responses.Sca.Yes'>
            <span class='subtext'>SCA Wage Determination Attachment</span>
        </answer-field>
		<answer-field answer='Employer.PhysicalAddress' address-field='true'>
            <span class='subtext'>Physical Address of Employer’s Main Establishment</span>
        </answer-field>
		<answer-field answer='Employer.HasTradeName'>
            <span class='subtext'>Does the Employer have a Trade Name?</span>
        </answer-field>
    </div>
    <div class='admin-page'>
        <div class='dol-form-section-title'>
            <h2>Wage Data</h2>
        </div>
        <answer-field answer='PayType.Display' defaultValue='NOT PROVIDED'>
            <span class='subtext'>Pay Type</span>
        </answer-field>
        <div wage-data-hourly='true'> 
            <h3>Hourly</h3>
            <hr>
            <h3 ng-show='MostRecentPrevailingWageSurvey.SourceEmployers.Length'>Source Employers</h3>

            <div wage-data-repeat='true' ng-repeat='MostRecentPrevailingWageSurvey.SourceEmployers' ng-show='Data.MostRecentPrevailingWageSurvey.SourceEmployers.Length'>
                <wage-data-answer-repeat-field answer='Employer.EmployerName'>
                    <span class='subtext'>Source Employer Name</span>
                </wage-data-answer-repeat-field>
                <hr>
            </div>           
        </div>
        <div wage-data-piecerate='true'>
            <h3>Piece Rate</h3>
            <hr>          

            <h3 ng-show='MostRecentPrevailingWageSurvey.SourceEmployers.Length'>Source Employers</h3>

            <div wage-data-repeat='true' ng-repeat='MostRecentPrevailingWageSurvey.SourceEmployers' ng-show='Data.MostRecentPrevailingWageSurvey.SourceEmployers.Length'>
                <wage-data-answer-repeat-field answer='Employer.EmployerName'>
                    <span class='subtext'>Source Employer Name</span>
                </wage-data-answer-repeat-field>       
            </div>
        </div>
    </div>
    <div class='admin-page'>
        <h1>Work Sites & Employees</h1>
        <hr>
        <answer-field answer='TotalNumWorkSites'>
            <span class='subtext'>What is the total number of establishments and work sites to be covered by this certificate?</span>
        </answer-field>

        <div class='table-responsive'>
            <div worksites-section='true' answer='WorkSites'>
                <table cellpadding='0' cellspacing='0' border='1' style='width:100%'>
                    <thead>
                        <tr class='row_greyed'>
                            <th scope='col'>Work Site Name</th>
                            <th scope='col'>Number of Workers</th>
                        </tr>
                    </thead>
                    <tbody ng-repeat='WorkSites'>
                        <tr style='text-align:center'>
                            <td scope='row'>WorkSites.Name</td>
                            <td>WorkSites.Employee.Name</td>
                        </tr>
                    </tbody>
                </table>
                <div worksites-content>                    
                    <div worksites-sub='true' answer='WorkSites'>
                        <p><span class='title'>Work Site </span></p>
                        <hr>
                        <worksites-sub-field answer='WorkSiteType.Display'>
                            <span class='subtext'>What type of establishment is this?</span>
                        </worksites-sub-field>

                        <worksites-sub-field answer='Name'>
                            <span class='subtext'>Name of Establishment / Work Site</span>
                        </worksites-sub-field>

                        <worksites-sub-field answer='Address' address-field='true'>
                            <span class='subtext'>Address of Establishment / Work Site</span>
                        </worksites-sub-field>

                        <worksites-sub-sub-field answer='SCA'>
                            <span class='subtext'>Is Service Contract Act (SCA)-covered work performed at this establishment / work site?</span>
                        </worksites-sub-sub-field>

                        <worksites-sub-field answer='FederalContractWorkPerformed'>
                            <span class='subtext'>Is work performed at this establishment / work site pursuant to a Federal contract for services or concessions that was entered into on or after January 1, 2015, which indicates that the contract may be subject to Executive Order 13658 (Establishing a Minimum Wage for Contractors)?</span>
                        </worksites-sub-field>

                        <worksites-sub-field answer='NumEmployees'>
                            <span class='subtext'>Total number of employees who were employed at this establishment/work site at any time during the most recently completed fiscal quarter and received subminimum wages:</span>
                        </worksites-sub-field>
                    </div>
                    <div employee-sub='true' answer='WorkSites.Employee'>
                        <p><span class='title'>Employee</span></p>
                        <hr>
                        <table cellpadding='0' cellspacing='0' border='1' style='width:100%'>
                            <thead>
                                <tr>
                                    <th scope='col'>Name</th>
                                    <th scope='col'>Disability</th>
                                    <th scope='col'>Work Performed</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td employees-sub-field='' answer='Name'><span class='subtext'></span></td>
                                    <td employees-sub-field='' answer='PrimaryDisability.Display'><span class='subtext'></span></td>
                                    <td employees-sub-field='' answer='WorkType'><span class='subtext'></span></td>
                                </tr>
                                <tr>
                                    <td colspan='3' style='text-align:center'>
                                        <table cellpadding='0' cellspacing='0' border='0' class='employeeSubTable'>
                                            <thead>
                                                <tr>
                                                    <td></td>
                                                    <th style='font-size:11px'>Number of Jobs</th>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td employees-sub-field='' answer='NumJobs' style='font-size:11px'><span class='subtext'></span></td>
                                                </tr>
                                            </thead>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>        
    </div>
    <div class='admin-page'>
        <div class='dol-form-section-title'>
            <h2>Workforce Innovation and Opportunity Act (WIOA)</h2>
        </div>

        <answer-field answer='WIOA.HasVerifiedDocumentation'>
            <span class='subtext'>Has the employer reviewed and verified documentation that counseling and referrals have been provided to each worker paid at a subminimum wage, regardless of age, and each has been informed of available training opportunities as required by WIOA?</span>
        </answer-field>

        <answer-field answer='WIOA.HasWIOAWorkers'>
            <span class='subtext'>Were any workers paid a subminimum wage age 24 or younger?</span>
        </answer-field>

        <div ng-show='WIOA.HasWIOAWorkers'>
            <answer-field answer=''''>
                <span class='subtext'>Please list the name of each worker who is age 24 or younger and answer yes/no/not required to the following question for each worker listed.</span>
                <br><br>
                <span class='subtext'>Did the employer review, verify, and maintain documentation showing that the worker received all services and counseling required by WIOA before paying the worker a subminimum wage?</span>
            </answer-field>
            <table cellpadding='0' cellspacing='0' border='1' style='width:100%'>
                <thead>
                    <tr class='row_greyed'>
                        <th scope='col'>Name of Worker</th>
                        <th scope='col'>Response</th>
                    </tr>
                </thead>
                <tbody ng-repeat='WIOA.WIOAWorkers'>
                    <tr style='text-align:center'>
                        <td scope='row'>WIOAWorkers.FullName</td>
                        <td>WIOAWorkers.WIOAWorkerVerified.Display</td>
                    </tr>                    
                </tbody>
            </table>
        </div>
    </div>
</body>
</html>";
        
        public ApplicationFormTest()
        {
            _fileRepositoryMock = new FileRepository(@"TestUploads\");
            _attachmentRepositoryMock = new AttachmentRepositoryMock();
        }

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var einToTest = "41-9876543";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.txt";

            // Arrange
            var einToTest1 = "41-9876544";
            var fileName1 = "test1.txt";

            var einToTest2 = "41-9876545";
            var fileName2 = "test2.txt";

            var einToTest3 = "41-9876546";
            var fileName3 = "test3.txt";

            var einToTest4 = "41-9876547";
            var fileName4 = "test4.txt";


            //htmlContent = @"<html><body><p>Test Test</p></body></html>";
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            var sCAAttachment = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");
            var sCAWageDeterminationAttachment = service.UploadAttachment(einToTest1, memoryStream, fileName1, "text/plain");
            var pieceRateWageInfoAttachment = service.UploadAttachment(einToTest2, memoryStream, fileName2, "text/plain");
            var mostRecentPrevailingWageSurveyAttachment = service.UploadAttachment(einToTest3, memoryStream, fileName3, "text/plain");
            var hourlyWageInfoAttachment = service.UploadAttachment(einToTest4, memoryStream, fileName4, "text/plain");

            var address = new Section14c.Domain.Models.Address {
                City = "Test City",
                County = "Some County",
                State = "VA",
                StreetAddress = "123 Main St",
                ZipCode="12345"
            };
            var employerInfoProvidingFacilitiesDeductionType = new EmployerInfoProvidingFacilitiesDeductionType {
                ProvidingFacilitiesDeductionType = new Response {
                    Display = "Test"}
            };
            ICollection<EmployerInfoProvidingFacilitiesDeductionType> eProvidingFacilitiesDeductionType = 
                new List <EmployerInfoProvidingFacilitiesDeductionType >{ employerInfoProvidingFacilitiesDeductionType };
            EmployerInfo employer = new EmployerInfo
            {
                SCAAttachment = sCAAttachment,
                PhysicalAddress= address,
                TemporaryAuthority = true,
                HasTradeName= false,
                ProvidingFacilitiesDeductionType = eProvidingFacilitiesDeductionType
            };
            SourceEmployer sEmployee = new SourceEmployer { EmployerName="Some Name" };
            ICollection <SourceEmployer> sourceEmployer = new List<SourceEmployer> { sEmployee};
            PrevailingWageSurveyInfo prevailingWageSurveyInfo = new PrevailingWageSurveyInfo
            {
                Attachment = mostRecentPrevailingWageSurveyAttachment,
                SourceEmployers = sourceEmployer
            };
            PieceRateWageInfo pieceRateWageInfo = new PieceRateWageInfo
            {
                Attachment = pieceRateWageInfoAttachment,
                SCAWageDeterminationAttachment = sCAWageDeterminationAttachment,
                MostRecentPrevailingWageSurvey = prevailingWageSurveyInfo
            };
            HourlyWageInfo hourlyWageInfo = new HourlyWageInfo
            {
                Attachment = hourlyWageInfoAttachment,
                MostRecentPrevailingWageSurvey = prevailingWageSurveyInfo,
                SCAWageDeterminationAttachment = sCAWageDeterminationAttachment
            };
            Response res = new Response
            {
                Display = "Response Test 1",
                IsActive = true
            };
            WIOAWorker worker1 = new WIOAWorker
            {
                FullName = "Test 1",
                WIOAWorkerVerified = res,
            };
            ICollection<WIOAWorker> wIOAWorkerCol = null;
            wIOAWorkerCol = new List<WIOAWorker>() { worker1 };
            ICollection<Employee> employees = null;
            var emp = new Employee()
            {
                AvgHourlyEarnings = 1,
                AvgWeeklyHours = 1,
                CommensurateWageRate = "1",
                PrevailingWage = 1,
                Name = "test",
                NumJobs = 1,
                PrimaryDisability = new Response() { Display = "Sample Data" },
                ProductivityMeasure = 1.2,
                TotalHours = 1.0,
                WorkType = "Some Type"
            };
            employees = new List<Employee>() { emp };
            ICollection<WorkSite> workSites = null;
            var ws = new WorkSite() { Employees = employees };
            workSites = new List<WorkSite>() { ws };

            WIOA wIOA = new WIOA
            {
                WIOAWorkers = wIOAWorkerCol,
            };
            application = new ApplicationSubmission
            {
                Employer = employer,
                PieceRateWageInfo = pieceRateWageInfo,
                HourlyWageInfo = hourlyWageInfo,
                WIOA = wIOA,
                PayType = new Response() { Display = "Both" },
                WorkSites = workSites,
                EIN = "11-1111111",
                Id = "2222-2222-2222-2222-2222",
                TotalNumWorkSites = 1,
                ContactName = "Tester",
                ContactPhone = "123=345-1234",
                
            };
        }

        [TestMethod]
        public void DownloadApplicationAttachments()
        {
            // Arrange
            var einToTest = "40-9876543";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            var memoryStream = new MemoryStream(data);
            var fileName = "test.txt";

            // Arrange
            var einToTest1 = "40-9876544";
            var fileName1 = "test1.txt";

            var htmlContent = "<html><body><p>Test Test</p></body></html>";
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            var attachment = service.UploadAttachment(einToTest, memoryStream, fileName, "text/plain");
            var attachment1 = service.UploadAttachment(einToTest1, memoryStream, fileName1, "text/plain");

            List<Attachment> attachments = new List<Attachment>()
            {
                attachment,
                attachment1
            };

            using (var outMemoryStream = new MemoryStream())
            {
                List<ApplicationData> applicationDataArray = service.DownloadApplicationAttachments(attachments, htmlContent);

                string outText = Encoding.ASCII.GetString(applicationDataArray[1].Buffer);

                Assert.AreEqual(outText, testFileContents);
            }
        }

        [TestMethod]
        public void GetApplicationAttachments()
        {
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            using (var outMemoryStream = new MemoryStream())
            {
                List<Attachment> attachmentArray = service.GetApplicationAttachments(application);
                Assert.AreEqual(5, attachmentArray.Count);
            }
        }

        [TestMethod]
        public void ApplicationFormView()
        {
            var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
            string applicationFormHtmlContent = service.ApplicationFormView(application, htmlContent);
            Assert.IsNotNull(applicationFormHtmlContent);
        }
    }
}
