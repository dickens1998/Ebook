using Serilog;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace EBook.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly ILogger _logger;
        private const string key = "__action_duration__";
        public ActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var stopWatch = new Stopwatch();

            filterContext.Controller.ViewData[key] = stopWatch;

            stopWatch.Start();
        }


        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
         
            if (!filterContext.Controller.ViewData.ContainsKey(key))
            {
                return;
            }


            var stopWatch = filterContext.Controller.ViewData[key] as Stopwatch;

            if (stopWatch != null)
            {
                stopWatch.Stop();

                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                _logger.Information(
                    $"Time: {DateTime.Now}, Controller: {controllerName}Controller, Aciton:{actionName}, Elapsed: {stopWatch.ElapsedMilliseconds} ms");
            }
        }

       
    }
}