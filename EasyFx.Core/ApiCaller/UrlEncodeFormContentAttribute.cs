using EasyFx.Core.Extensions;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace EasyFx.Core.ApiCaller
{
    public class UrlEncodeFormContentAttribute : HttpContentAttribute
    {
        /// <summary>设置参数到http请求内容</summary>
        /// <param name="context">上下文</param>
        /// <param name="parameter">特性关联的参数</param>
        /// <exception cref="T:WebApiClient.HttpApiConfigException"></exception>
        /// <returns></returns>
        protected override Task SetHttpContentAsync(ApiActionContext context, ApiParameterDescriptor parameter)
        {
            var data = parameter.Value;
            var map = data.ObjectToDictionary();
            string fromStuff = string.Join("&", map.Select(it => $"{it.Key}={HttpUtility.UrlEncode(it.Value)}"));

            context.RequestMessage.Content = new StringContent(fromStuff, Encoding.UTF8, "application/x-www-form-urlencoded");

            return Task.CompletedTask;
        }
    }
}