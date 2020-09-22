using Serilog;
using System;
using System.Web.Mvc;

namespace EBook.Filters
{
    public class ExceptionFilter : HandleErrorAttribute
    {

        private readonly ILogger _logger;


        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext filterContext)
        {

            base.OnException(filterContext);
            Exception ex = filterContext.Exception;

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            _logger.Information(
                $"发生时间: {DateTime.Now}, Controller: {controllerName}Controller, Aciton:{actionName},\n 错误信息为: {ex}  ");


        }
    }
}