using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.Business.Helper
{
    /// <summary>
    /// Applcaition Information for User
    /// </summary>
    public class UserApplications
    {
        /// <summary>
        /// Application Id and Employer Name
        /// Key and Value Pair
        /// </summary>
        public Dictionary<string, string> Id { get; set; }

        /// <summary>
        /// Employer Id and Employer Name
        /// </summary>
        public string EmployerName { get; set; }
    }
}
