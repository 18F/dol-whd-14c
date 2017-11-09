using System.Web.Mvc;

namespace DOL.WHD.Section14c.Api
{
    /// <summary>
    /// Filter configuration class
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register an error handler globally
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
