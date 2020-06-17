using EasyFx.Web.Core.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace EasyFx.Web.Core.Middlewares
{
    /// <summary>
    /// 请求日志中间件
    /// </summary>
    internal class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogMiddleware> _logger;
        private readonly RequestLogOptions _options;

        public RequestLogMiddleware(RequestDelegate next,ILogger<RequestLogMiddleware> logger,
            RequestLogOptions options)
        {
            _next = next;
            _logger = logger;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            var body = await FormatRequest(context.Request);
            _logger.LogInformation($"Path={context.Request.Path},Method={context.Request.Method},QueryString={context.Request.QueryString},Body={body}");

            Stopwatch sw=new Stopwatch();
            sw.Start();
            await _next.Invoke(context);
            sw.Stop();

            string response = string.Empty;
            if (_options.IsWriteResponse)
            {
                response = await FormatResponse(context.Response);
            }

            _logger.LogInformation($"Path={context.Request.Path},Method={context.Request.Method},Cost={sw.ElapsedMilliseconds}ms,Response={response}");
        }


        #region Private Method

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering(30720, default);
            if (request.ContentType == null || request.ContentType.Contains("json") ||
                request.ContentType.Contains("text"))
            {
                request.Body.Seek(0L, SeekOrigin.Begin);
                using (var reader=new StreamReader(request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    request.Body.Seek(0L, SeekOrigin.Begin);
                    return body?.Trim();
                }
            }
            request.Body.Seek(0L, SeekOrigin.Begin);
            return string.Empty;
        }


        private async Task<string> FormatResponse(HttpResponse response)
        { 
            if (response.HasStarted)
                return string.Empty;
            if (response.ContentType != null && response.ContentType.Contains("json"))
            {
                response.Body.Seek(0L, SeekOrigin.Begin);
                using (var reader=new StreamReader(response.Body))
                {
                    string endAsync = await reader.ReadToEndAsync();
                    response.Body.Seek(0L, SeekOrigin.Begin);
                    return endAsync?.Trim();
                }
            }
            response.Body.Seek(0L, SeekOrigin.Begin);
            return string.Empty;
        }

        #endregion
    }
}