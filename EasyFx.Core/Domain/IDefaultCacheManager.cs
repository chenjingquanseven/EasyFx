using EasyFx.Core.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EasyFx.Core.Domain
{
    public interface IDefaultCacheManager:IScope
    {
        Task<T> GetShellAsync<T>(string key,Func<Task<T>> valueFunc, TimeSpan expire);

        Task<T> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T value,TimeSpan expire);


        Task RemoveAsync(string key);
    }
}