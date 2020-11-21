using System.Web;
using System.Web.Mvc;

namespace SavePasswordAPI_No_entityFramework_WIth_Database_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
