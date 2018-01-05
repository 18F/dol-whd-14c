using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class EmployerRepositoryMock : IEmployerRepository
    {
        private bool _disposed;
        private readonly List<Employer> _data;

        public bool Disposed => _disposed;

        /// <summary>
        /// Sets up the mock with fake data
        /// </summary>
        public EmployerRepositoryMock()
        {
            _data = new List<Employer>
            {
                new Employer {
                    Id = "",
                    EIN = "10-1212111",
                    CertificateNumber ="12-12123123",
                    LegalName ="Test Employer",
                    PhysicalAddress = new Section14c.Domain.Models.Address()
                    {
                        StreetAddress = "123 Main Street",
                        City = "My City",
                        State = "VA",
                        County = "My County",
                        ZipCode = "12345"
                    }                
                }
            };
        }

        /// <summary>
        /// Get data
        /// </summary>
        /// <returns>Data</returns>
        public IEnumerable<Employer> Get()
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
