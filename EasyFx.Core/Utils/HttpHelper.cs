using EasyFx.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace EasyFx.Core.Utils
{
    public class HttpHelper
    {
        public static string GetQueryString<T>(T data,params string[] ignoreProps)
        {
            var param = data.ObjectToDictionary();
            var sortedParam = new SortedDictionary<string, string>(param)
                .Where(it => !it.Value.IsEmpty() && !ignoreProps.Contains(it.Key));
 
            var paramStr = string.Empty;
            return string.Join("&", sortedParam.Select(it => $"{it.Key}={it.Value}"));
        }
    }
}