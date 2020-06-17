using EasyFx.Core.Extensions;
using EasyFx.Web.Core.Abstracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyFx.Web.Core.Middlewares
{
    internal class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TraceOptions _options;

        public TraceMiddleware(RequestDelegate next, TraceOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            var userIpKey = _options.UserIpKey.IsEmpty() ? SysConsts.UserIp : _options.UserIpKey;
            var traceIdKey = _options.TraceIdKey.IsEmpty() ? SysConsts.TraceId : _options.TraceIdKey;
            var userIp = context.GetHeaderValue(userIpKey);
            var traceId = context.GetHeaderValue(traceIdKey);

            if (userIp.IsEmpty())
            {
                userIp = context.GetUserOrCurrentIp();
                context.Request.Headers.TryAdd(userIpKey, userIp);
            }

            if (traceId.IsEmpty())
            {
                traceId = Guid.NewGuid().ToString("N");
                context.Request.Headers.TryAdd(traceIdKey, traceId);
            }


            if (_next != null)
            {
                await _next.Invoke(context);
            }
        }
    }
}