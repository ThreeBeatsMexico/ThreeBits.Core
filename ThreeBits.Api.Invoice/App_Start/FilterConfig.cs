using System.Web;
using System.Web.Mvc;

namespace ThreeBits.Api.Invoice
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
