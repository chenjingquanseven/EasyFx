using System;
using System.IO;
using System.Text;
using EasyFx.Core.Utils;

namespace EasyFx.Core.Extensions
{
    /// <summary>
    /// Xml
    /// </summary>
    public static partial  class XmlExtensions
    {
        /// <summary>
        /// 序列化为xml 字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToXml(this object data)
        {
            return XmlHelper.Serializer(data);
        }
        /// <summary>
        /// xml 转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T XmlToObject<T>(this string xml)
        {
            return XmlHelper.Deserialize<T>(xml);
        }
    }
}