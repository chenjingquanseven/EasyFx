using Microsoft.AspNetCore.Http;

namespace EasyFx.Web.Core.Abstracts
{
    public class WebContext
    {
        private static  IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;
    }
}