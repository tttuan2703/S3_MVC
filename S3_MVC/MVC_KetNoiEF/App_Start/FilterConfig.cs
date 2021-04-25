using System.Web;
using System.Web.Mvc;

namespace MVC_Bai5_KetNoiEF
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}