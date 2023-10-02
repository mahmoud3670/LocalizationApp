
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace APIAPP.Filters
{
   
    internal class LocalizationFilter : IActionFilter
    {
       

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            string? lang = filterContext?.HttpContext?.Request?.Headers["language"];
            CultureInfo.CurrentUICulture = new CultureInfo(lang??"en-Us");
        }
    }
}
