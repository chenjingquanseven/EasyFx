using EasyFx.Core.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Net;

namespace EasyFx.Core.Extensions
{
    public static partial class HttpContextExtensions
    {
        public static void ExcelDownload<T>(this HttpContext httpContext, ExcelConfig<T> config) where T : class
        {
            var bytes = ExcelManager.Export(config);
            ContentDispositionHeaderValue dispositionHeaderValue = new ContentDispositionHeaderValue((StringSegment)"attachment");
            dispositionHeaderValue.SetMimeFileName((StringSegment)config.FileName);
            httpContext.Response.Headers["Content-Disposition"] = (StringValues)dispositionHeaderValue.ToString();
            httpContext.Response.ContentType = "application/ms-excel";
            httpContext.Response.Body.Write(bytes, 0, bytes.Length);
        }

        public static string FullHost(this HttpContext context)
        {
            return $"{context.Request.Scheme}://{context.Request.Host}";
        }

        /// <summary>
        ///     获取用户IP或当前机器的IP
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public static string GetUserOrCurrentIp(this HttpContext context)
        {
            try
            {
                //先获取远和调用方的
                var ip = context.GetUserIp();

                if (string.IsNullOrWhiteSpace(ip))
                {
                    //程序初始化时，获取当前服务器IP
                    foreach (var IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                        if (IPA.AddressFamily.ToString() == "InterNetwork")
                        {
                            ip = IPA.ToString();
                            break;
                        }

                    if (string.IsNullOrWhiteSpace(ip)) ip = "127.0.0.1";
                }

                if (ip == "::1") ip = "127.0.0.1";

                return ip;
            }
            catch (Exception)
            {
                return "127.0.0.1";
            }
        }


        /// <summary>
        ///     获取用户IP
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public static string GetUserIp(this HttpContext context)
        {
            try
            {
                var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                if (string.IsNullOrEmpty(ip)) ip = context.Connection.RemoteIpAddress?.ToString();

                if (string.IsNullOrWhiteSpace(ip)) ip = context.Request.Headers["REMOTE_ADDR"].FirstOrDefault();

                return ip;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        ///     获取头部值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="headerKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetHeaderValue(this HttpContext context, string headerKey, string defaultValue = null)
        {
            context.Request.Headers.TryGetValue(headerKey, out var value);
            if (value.Count == 0) return string.Empty;

            return value[0];
        }
    }
}