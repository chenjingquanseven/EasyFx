using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EasyFx.Core.Extensions
{
    public static partial class ReflectionExtensions
    {
        public static string GetDescription(this object value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description;
        }

    }
}