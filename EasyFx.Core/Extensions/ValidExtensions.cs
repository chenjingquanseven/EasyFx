using System.Collections.Generic;
using System.Linq;

namespace EasyFx.Core.Extensions
{
    public static partial class ValidExtensions
    {
        public static bool IsEmpty(this string data)
        {
            return string.IsNullOrWhiteSpace(data);
        }

        public static bool IsAny<T>(this IEnumerable<T> enumable)
        {
            if (enumable == null)
            {
                return false;
            }

            return enumable.Any();
        }
    }
}