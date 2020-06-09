using System;
using EasyFx.Core.Extensions;
using WebApiClient;

namespace EasyFx.Core.ApiCaller
{
    public class JsonFormatter:IJsonFormatter
    {
        /// <summary>将对象序列化为json文本</summary>
        /// <param name="obj">对象</param>
        /// <param name="options">选项</param>
        /// <returns></returns>
        public string Serialize(object obj, FormatOptions options)
        {
            return obj.ToJson();
        }

        /// <summary>将json文本反序列化对象</summary>
        /// <param name="json">json文本内容</param>
        /// <param name="objType">对象类型</param>
        /// <returns></returns>
        public object Deserialize(string json, Type objType)
        {
            return json.JsonToObject(objType);
        }
    }
}