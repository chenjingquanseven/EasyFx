using EasyFx.Core.Extensions;
using EasyFx.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Contexts;

namespace EasyFx.Core.ApiCaller
{
    /// <summary>
    /// 添加traceId和userIp
    /// </summary>
    public class TraceFilter : IApiActionFilter
    {
        private readonly string _traceIdKey;
        private readonly string _userIpKey;
        private readonly Stopwatch _stopwatch;
        public TraceFilter(string traceIdKey,string userIpKey)
        {
            _traceIdKey = traceIdKey;
            _userIpKey = userIpKey;
            _stopwatch = new Stopwatch();
        }
        /// <summary>准备请求之前</summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public Task OnBeginRequestAsync(ApiActionContext context)
        {
            _stopwatch.Start();
            var httpContextAccessor = context.GetService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor?.HttpContext;
            var userIp = httpContext?.GetHeaderValue(_userIpKey) ?? IPHelper.GetDnsIp();
            var traceId = httpContext?.GetHeaderValue(_traceIdKey) ?? Guid.NewGuid().ToString("N");
            context.RequestMessage.Headers.TryAddWithoutValidation(_userIpKey, userIp);
            context.RequestMessage.Headers.TryAddWithoutValidation(_traceIdKey, traceId);
            return Task.CompletedTask;
        }

        /// <summary>请求完成之后</summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public async Task OnEndRequestAsync(ApiActionContext context)
        {
            var ignore = context.ApiActionDescriptor.Member.GetCustomAttribute<IgnoreLogAttribute>();
            if (ignore == null)
            {
                ignore = context.ApiActionDescriptor.Member.DeclaringType?.GetCustomAttribute<IgnoreLogAttribute>();
            }

            if (ignore != null)
            {
                return;
            }
            var logger = context.GetService<ILogger<TraceFilter>>();
            string content = string.Empty;
            if (context.RequestMessage.Content != null)
            {
                content = await context.RequestMessage.Content?.ReadAsStringAsync();
            }

            string result = string.Empty;
            if (context.ResponseMessage != null)
            {
                result = await context.ResponseMessage?.Content.ReadAsStringAsync();
            }

            _stopwatch.Stop();
            var cost = _stopwatch.ElapsedMilliseconds;
            logger.LogInformation($" Api :{context.HttpApi.GetType().FullName },Cost:{ cost }, Url:{context.RequestMessage.RequestUri},RequestBody:{content},Response:{result}");

            _stopwatch.Reset();
        }
    }
}