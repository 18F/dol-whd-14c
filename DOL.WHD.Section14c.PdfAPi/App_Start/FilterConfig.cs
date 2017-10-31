using System.Web;
using System.Web.Mvc;

namespace DOL.WHD.Section14c.PdfApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
