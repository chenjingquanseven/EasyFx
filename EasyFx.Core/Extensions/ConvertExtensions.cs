using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace EasyFx.Core.Extensions
{
    public static partial class ConvertExtensions
    {
        public static List<string> ToSplitList(this string value,string separator=",")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new List<string>();
            }
            return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        /// <summary>
        ///     转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(this object data)
        {
            if (data == null)
                return false;
            var value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            return bool.TryParse(data.ToString(), out result) && result;
        }
        /// <summary>
        ///     获取布尔值
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        private static bool? GetBool(this object data)
        {
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return null;
            }
        }

        public static T ToEnum<T>(this Enum @enum) where T : struct
        {
            T res;
            Enum.TryParse<T>(@enum.ToString(), true, out res);
            return res;
        }

        public static DateTime? ToDateTimeOrNull(this string data,string format=null)
        {
            DateTime result;
            bool flag = false;
            if (format.IsEmpty())
            {
                flag = DateTime.TryParse(data, out result);
            }
            else
            {
                flag = DateTime.TryParseExact(data, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out result);
            }
             
            return flag ? result : (DateTime?) null;
        }

        /// <summary>
        /// yyyyMMdd 模式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNullByShort(this string data)
        {
            return data.ToDateTimeOrNull("yyyyMMdd");
        }

        /// <summary>
        /// object to dictionary
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ObjectToDictionary(this object data)
        {
            return data.GetType().GetProperties()
                .ToDictionary(src =>
                {
                    var attribute = src.GetCustomAttribute(typeof(JsonPropertyAttribute));
                    if (attribute == null || !(attribute is JsonPropertyAttribute jsonProperty))
                    {
                        return src.Name;
                    }

                    return jsonProperty?.PropertyName;
                }, src => src.GetValue(data)?.ToString());
        }


        public static int ToInt(this object data)
        {
            try
            {
                return Convert.ToInt32(data);
            }
            catch (Exception)
            {
                var flag = int.TryParse(data.ToString(), out var result);
                return flag ? result : 0;
            }
        }
    }
}