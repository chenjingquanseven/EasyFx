using EasyFx.Core.Abstracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;

namespace EasyFx.Core.Extensions
{
    /// <summary>
    /// Json 拓展
    /// </summary>
    public static partial class JsonExtensions
    {
        /// <summary>
        ///     转json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="format">日期格式</param>
        /// <returns></returns>
        public static string ToJson(this object obj,string format=DateTimeFormat.Normal)
        {
            var settings=new JsonSerializerSettings(){
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                
                DateFormatString = format,
            };
            settings.Converters.Add(new StringEnumConverter());
;
            return JsonConvert.SerializeObject(obj, settings);
        }
        /// <summary>
        /// 转object
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object JsonToObject(this string json,Type type)
        {
            return JsonConvert.DeserializeObject(json,type);
        }
        /// <summary>
        ///     json转对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="format">日期格式</param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string json,string format=DateTimeFormat.Normal)
        {
            return  string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json,new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = format,
            });
        }

        /// <summary>
        ///     json转List
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string json)
        {
            return json.JsonToObject<List<T>>();
        }

        /// <summary>
        ///     json转DataTable
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static DataTable ToTable(this string json)
        {
            return json.JsonToObject<DataTable>();
        }

        /// <summary>
        ///     json转JObject 对象
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static JObject JsonToJObject(this string json)
        {
            return string.IsNullOrWhiteSpace(json) ? JObject.Parse("{}") : JObject.Parse(json.Replace("&nbsp;", ""));
        }
    }
}