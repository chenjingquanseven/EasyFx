using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasyFx.Web.Core.Filters
{
    public class CustomExceptionFilter:IExceptionFilter
    {
        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<CustomExceptionFilter>>();
            var enviroment = context.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
        }
    }
}