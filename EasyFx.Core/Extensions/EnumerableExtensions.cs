
using System.Collections;
using System.Collections.Generic;

namespace EasyFx.Core.Extensions
{
    public static partial class EnumerableExtensions
    {
        public static T GetValueOrDefault<T>(this IList<T> list, int index)
        {
            if (!list.IsAny())
            {
                return default(T);
            }

            if (list.Count >= index + 1)
            {
                return list[index];
            }

            return default(T);
        }
    }
}