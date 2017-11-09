using System.Collections.Generic;
using System.Linq;
using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models.Submission;

namespace DOL.WHD.Section14c.Test.RepositoryMocks
{
    public class ResponseRepositoryMock : IResponseRepository
    {
        private bool _disposed;
        private readonly List<Response> _data;

        public bool Disposed => _disposed;

        public ResponseRepositoryMock()
        {
            _data = new List<Response>
            {
                new Response
                {
                    Id = 1,
                    QuestionKey = "ApplicationType",
                    Display = "Initial Application",
                    IsActive = true
                },
                new Response
                {
                    Id = 2,
                    QuestionKey = "ApplicationType",
                    Display = "Renewal Application",
                    IsActive = true
                },
                new Response
                {
                    Id = 3,
                    QuestionKey = "ApplicationType",
                    Display = "Inactive Application Type",
                    IsActive = false
                },
                new Response
                {
                    Id = 4,
                    QuestionKey = "EmployerStatus",
                    Display = "Public (State or Local Government)",
                    IsActive = true
                },
                new Response
                {
                    Id = 5,
                    QuestionKey = "EmployerStatus",
                    Display = "Private, For Profit",
                    IsActive = true
                },
                new Response
                {
                    Id = 6,
                    QuestionKey = "EmployerStatus",
                    Display = "Private, Not For Profit",
                    IsActive = true
                },
            };
        }

        public IQueryable<Response> Get()
        {
            return _data.AsQueryable();
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _disposed = !_disposed || disposing;
        }
    }
}
