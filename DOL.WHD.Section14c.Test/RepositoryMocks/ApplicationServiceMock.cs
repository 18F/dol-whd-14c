using DOL.WHD.Section14c.Business;
using DOL.WHD.Section14c.Business.Services;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.DataAccess.Repositories;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class ApplicationServiceMock: IApplicationService
    {
        private bool _disposed;
        private ApplicationSubmission applicationSubmission;
        private List<ApplicationSubmission> applicationSubmissionCollection;
        private readonly IFileRepository _fileRepositoryMock;
        private readonly IAttachmentRepository _attachmentRepositoryMock;

        public ApplicationServiceMock()
        {
            _fileRepositoryMock = new FileRepository(@"TestUploads\");
            _attachmentRepositoryMock = new AttachmentRepositoryMock();

            // Arrange
            var einToTest = "41-9876543";
            var testFileContents = "test";
            var data = Encoding.ASCII.GetBytes(testFileContents);
            using (var memoryStream = new MemoryStream(data))
            {
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

                var service = new AttachmentService(_fileRepositoryMock, _attachmentRepositoryMock);
                var sCAAttachment = service.UploadAttachment(einToTest, memoryStream.ToArray(), fileName, "text/plain");
                var sCAWageDeterminationAttachment = service.UploadAttachment(einToTest1, memoryStream.ToArray(), fileName1, "text/plain");
                var pieceRateWageInfoAttachment = service.UploadAttachment(einToTest2, memoryStream.ToArray(), fileName2, "text/plain");
                var mostRecentPrevailingWageSurveyAttachment = service.UploadAttachment(einToTest3, memoryStream.ToArray(), fileName3, "text/plain");
                var hourlyWageInfoAttachment = service.UploadAttachment(einToTest4, memoryStream.ToArray(), fileName4, "text/plain");

                var address = new Section14c.Domain.Models.Address
                {
                    City = "Test City",
                    County = "Some County",
                    State = "VA",
                    StreetAddress = "123 Main St",
                    ZipCode = "12345"
                };
                var employerInfoProvidingFacilitiesDeductionType = new EmployerInfoProvidingFacilitiesDeductionType
                {
                    ProvidingFacilitiesDeductionType = new Response
                    {
                        Display = "Test"
                    }
                };
                ICollection<EmployerInfoProvidingFacilitiesDeductionType> eProvidingFacilitiesDeductionType =
                    new List<EmployerInfoProvidingFacilitiesDeductionType> { employerInfoProvidingFacilitiesDeductionType };
                EmployerInfo employer = new EmployerInfo
                {
                    SCAAttachmentId = new List<string>() { "1234567890" },
                    SCAAttachment = new List<EmployerInfoSCAAttachment>() { new EmployerInfoSCAAttachment { SCAAttachment = sCAAttachment } },
                    PhysicalAddress = address,
                    TemporaryAuthority = true,
                    HasTradeName = false,
                    ProvidingFacilitiesDeductionType = eProvidingFacilitiesDeductionType
                };
                SourceEmployer sEmployee = new SourceEmployer { EmployerName = "Some Name" };
                ICollection<SourceEmployer> sourceEmployer = new List<SourceEmployer> { sEmployee };
                PrevailingWageSurveyInfo prevailingWageSurveyInfo = new PrevailingWageSurveyInfo
                {
                    AttachmentId = "1234567890",
                    Attachment = mostRecentPrevailingWageSurveyAttachment,
                    SourceEmployers = sourceEmployer
                };
                PieceRateWageInfo pieceRateWageInfo = new PieceRateWageInfo
                {
                    AttachmentId = "1234567890",
                    Attachment = pieceRateWageInfoAttachment,
                    //SCAWageDeterminationAttachmentId = "1234567890",
                    SCAAttachment = new List<WageTypeInfoSCAAttachment>() { new WageTypeInfoSCAAttachment { SCAAttachment = sCAWageDeterminationAttachment } } ,
                    MostRecentPrevailingWageSurvey = prevailingWageSurveyInfo
                };
                HourlyWageInfo hourlyWageInfo = new HourlyWageInfo
                {
                    AttachmentId = "1234567890",
                    Attachment = hourlyWageInfoAttachment,
                    MostRecentPrevailingWageSurvey = prevailingWageSurveyInfo,
                    SCAAttachment = new List<WageTypeInfoSCAAttachment>() { new WageTypeInfoSCAAttachment { SCAAttachment = sCAWageDeterminationAttachment } },
                };
                Response res = new Response
                {
                    Display = "Response Test 1",
                    IsActive = true
                };
                WIOAWorker worker1 = new WIOAWorker
                {
                    FirstName = "first name",
                    LastName = "last name",
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
                var ws = new WorkSite()
                {
                    Employees = employees,
                    WorkSiteType = new Response() { Display = "Work Site Type" },
                    Name = "This Is A Tribute",
                    SCA = false
                };
                workSites = new List<WorkSite>() { ws, ws, ws };

                WIOA wIOA = new WIOA
                {
                    WIOAWorkers = wIOAWorkerCol,
                };
                applicationSubmission = new ApplicationSubmission
                {
                    Employer = employer,
                    PieceRateWageInfo = pieceRateWageInfo,
                    HourlyWageInfo = hourlyWageInfo,
                    WIOA = wIOA,
                    PayType = new Response() { Display = "Both" },
                    WorkSites = workSites,
                    EIN = "11-1111111",
                    Id = "CE7F5AA5-6832-43FE-BAE1-80D14CD8F666",
                    TotalNumWorkSites = 1,
                    ContactFirstName = "Tester",
                    ContactLastName = "Tester",
                    ContactPhone = "123=345-1234",
                    ContactEmail ="test@test.com"
                };
                applicationSubmissionCollection = new List<ApplicationSubmission>();
                applicationSubmissionCollection.Add(applicationSubmission);
            }
        }

        public Task<int> SubmitApplicationAsync(ApplicationSubmission submission)
        {
            var value = Task<int>.Run(() => {
                int num = 1;
                return num;
            });
            return value;
        }
        public ApplicationSubmission GetApplicationById(Guid id)
        {
            return applicationSubmissionCollection.AsQueryable().SingleOrDefault(x => x.Id.ToLower() == id.ToString().ToLower());
        }
        public IEnumerable<ApplicationSubmission> GetAllApplications()
        {
            return applicationSubmissionCollection.AsQueryable();
        }
        public Task<int> ChangeApplicationStatus(ApplicationSubmission application, int newStatusId)
        {
            var value = Task<int>.Run(() => {
                int num = 1;
                return num;
            });
            return value;
        }
        public void ProcessModel(ApplicationSubmission vm)
        {
            
        }
       
        public void Dispose()
        {
            _disposed = true;
        }
    }
}
