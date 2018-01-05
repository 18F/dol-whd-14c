using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class OrganizationRepositoryMock: IOrganizationRepository
    {
        private bool _disposed;
        private readonly List<OrganizationMembership> _data;

        public bool Disposed => _disposed;

        /// <summary>
        /// Sets up the mock with fake data
        /// </summary>
        public OrganizationRepositoryMock()
        {
            _data = new List<OrganizationMembership>
            {
                new OrganizationMembership {
                    ApplicationId = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e32",
                    IsPointOfContact = true,
                    Employer = new Section14c.Domain.Models.Submission.Employer(){ Id= "123456" },
                    Employer_Id ="123456",
                    ApplicationStatus = new Status(){Id = 1, Name = "InProgress", IsActive = true},
                    ApplicationStatusId = 1
                },
                new OrganizationMembership {
                    ApplicationId = "2edbc12f-4fd9-4fed-a848-b8bfff4d4e33",
                    IsPointOfContact = true,
                    Employer = new Section14c.Domain.Models.Submission.Employer(){ Id= "323465" },
                    Employer_Id ="323465",
                    ApplicationStatus = new Status(){Id = 1, Name = "InProgress", IsActive = true},
                    ApplicationStatusId = 1
                }
            };
        }

        /// <summary>
        /// Get data
        /// </summary>
        /// <returns>Data</returns>
        public IEnumerable<OrganizationMembership> Get()
        {
            return _data.AsQueryable();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _disposed = _disposed || disposing;
        }
    }
}
