using DOL.WHD.Section14c.DataAccess;
using DOL.WHD.Section14c.Domain.Models;
using DOL.WHD.Section14c.Domain.Models.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business.Services
{
    /// <summary>
    /// Employer Service
    /// </summary>
    public class EmployerService: IEmployerService
    {
        private readonly IEmployerRepository _employerRepository;

        /// <summary>
        ///  Default constructor
        /// </summary>
        /// <param name="employerRepository"></param>
        public EmployerService(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        /// <summary>
        /// Add new Employer
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        public Task<int> AddEmployer(Employer employer)
        {
            return _employerRepository.AddAsync(employer);
        }

        /// <summary>
        /// Get Employer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employer GetEmployerById(Guid id)
        {
            return _employerRepository.Get().FirstOrDefault(x=> x.Id == id.ToString());
        }

        /// <summary>
        /// Get all employer
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employer> GetAllEmployers()
        {
            return _employerRepository.Get();
        }

        /// <summary>
        /// Get Employer By EIN, Employer Name and Employer Address
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        public Employer FindExistingEmployer(Employer employer)
        {
            // GEt all existing employers by EID
            var employers = _employerRepository.Get().Where(x => x.EIN == employer.EIN);
            foreach (var item in employers)
            {
                // Find employer by EIN,Employer Name and Employer Address
                if (item.LegalName == employer.LegalName &&
                    item.PhysicalAddress.StreetAddress.ToLower() == employer.PhysicalAddress.StreetAddress.ToLower() &&
                    item.PhysicalAddress.City.ToLower() == employer.PhysicalAddress.City.ToLower() &&
                    item.PhysicalAddress.State.ToLower() == employer.PhysicalAddress.State.ToLower() &&
                    item.PhysicalAddress.ZipCode.ToLower() == employer.PhysicalAddress.ZipCode.ToLower() &&
                    item.PhysicalAddress.County.ToLower() == employer.PhysicalAddress.County.ToLower()
                    )
                {
                    return item;
                }
            }
            // Return null if employer not found
            return null;
        }
    }
}
