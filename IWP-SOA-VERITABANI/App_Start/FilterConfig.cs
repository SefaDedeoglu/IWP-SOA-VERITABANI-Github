using System.Web;
using System.Web.Mvc;

namespace IWP_SOA_VERITABANI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
