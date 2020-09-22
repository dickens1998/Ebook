using EBook.Filters;
using System.Web.Mvc;

namespace EBook.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
  
        }
    }
}