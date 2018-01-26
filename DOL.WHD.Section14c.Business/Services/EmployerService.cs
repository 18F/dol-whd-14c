using DOL.WHD.Section14c.Common.Extensions;
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
        /// Validate employer data
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        public bool ValidateEmployer(Employer employer)
        {
            bool validated = true;
            if (string.IsNullOrEmpty(employer.EIN) || string.IsNullOrEmpty(employer.LegalName) || employer.PhysicalAddress == null ||
                string.IsNullOrEmpty(employer.PhysicalAddress.StreetAddress) || string.IsNullOrEmpty(employer.PhysicalAddress.City) ||
                string.IsNullOrEmpty(employer.PhysicalAddress.State) || string.IsNullOrEmpty(employer.PhysicalAddress.ZipCode) ||
                string.IsNullOrEmpty(employer.PhysicalAddress.County))
                validated = false;
            return validated;
        }

        /// <summary>
        /// Get Employer By Id, Employer Name and Employer Address
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        public Employer FindExistingEmployer(Employer employer)
        {
            // GEt all existing employers by EID
            var employers = _employerRepository.Get().Where(x => x.EIN.TrimAndToLowerCase() == employer.EIN.TrimAndToLowerCase());
            foreach (var item in employers)
            {
                // Find employer by Id,Employer Name and Employer Address
                if (item.LegalName.TrimAndToLowerCase() == employer.LegalName.TrimAndToLowerCase() &&
                    item.PhysicalAddress.StreetAddress.TrimAndToLowerCase() == employer.PhysicalAddress.StreetAddress.TrimAndToLowerCase() &&
                    item.PhysicalAddress.City.TrimAndToLowerCase() == employer.PhysicalAddress.City.TrimAndToLowerCase() &&
                    item.PhysicalAddress.State.TrimAndToLowerCase() == employer.PhysicalAddress.State.TrimAndToLowerCase() &&
                    item.PhysicalAddress.ZipCode.TrimAndToLowerCase() == employer.PhysicalAddress.ZipCode.TrimAndToLowerCase() &&
                    item.PhysicalAddress.County.TrimAndToLowerCase() == employer.PhysicalAddress.County.TrimAndToLowerCase()
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
