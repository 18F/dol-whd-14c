using System.Web;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.Log
{
    /// <summary>
    /// Filter configuration class
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Add the HandleError attribute to the global filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
