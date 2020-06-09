using EasyFx.Core.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace EasyFx.Core.Domain
{
    /// <summary>
    /// httpContext user provider
    /// </summary>
    public interface IHttpContextUserProvider:ISingleton
    {
        T GetUser<T>(HttpContext context);
    }
}