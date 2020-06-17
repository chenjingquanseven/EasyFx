using System;
using EasyFx.Web.Core.Abstracts;
using EasyFx.Web.Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasyFx.Web.Core.Extensions
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// trace
        /// </summary>
        /// <param name="app"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseTrace(this IApplicationBuilder app, Action<TraceOptions> action)
        {
            var options = new TraceOptions();

            action(options);

            app.UseMiddleware<TraceMiddleware>(options);

            return app;
        }

        /// <summary>
        /// request log
        /// </summary>
        /// <param name="app"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder app, Action<RequestLogOptions> action)
        {
            var options = new RequestLogOptions();

            action(options);

            app.UseMiddleware<RequestLogMiddleware>(options);

            return app;
        }


        /// <summary>
        /// cxy mvc
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configureBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseEasyMvc(this IApplicationBuilder app, IWebHostEnvironment environment,
            ILoggerFactory loggerFactory, Action configureBuilder = null)
        {
            app.UseStaticFiles();

            app.UseTrace(opt => { });

            app.UseRequestLog(opt => { });

            configureBuilder?.Invoke();

            app.UseMvc();

            WebContext.Configure(app.ApplicationServices.GetService<IHttpContextAccessor>());
            

            return app;
        }
    }
}