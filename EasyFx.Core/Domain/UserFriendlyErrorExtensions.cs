using EasyFx.Core.Extensions;
using System;

namespace EasyFx.Core.Domain
{
    public static partial  class UserFriendlyErrorExtensions
    {
        public static UserFriendlyException ToException(this Enum error)
        {
            var description = error.GetDescription();
            var code = error.GetHashCode();

            return new UserFriendlyException(code, description);
        }
    }
}