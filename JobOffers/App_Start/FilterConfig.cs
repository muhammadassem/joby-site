using System.Web;
using System.Web.Mvc;

namespace JobOffers
{
    public class FilterConfig
    {
        public static void 
            
            GlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
