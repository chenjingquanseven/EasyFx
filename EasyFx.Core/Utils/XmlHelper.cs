using System;
using System.IO;
using System.Text;

namespace EasyFx.Core.Utils
{
    public class XmlHelper
    {
        /// <summary>
        /// xml 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlstr)
        {
            if (string.IsNullOrEmpty(xmlstr))
                throw new ArgumentNullException("xmlstr");

            using (var strReader = new StringReader(xmlstr))
            {
                System.Xml.Serialization.XmlSerializer xmlser = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)xmlser.Deserialize(strReader);
            }
        }

        /// <summary>
        /// 序列化为xml 字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serializer(object data)
        {
            StringBuilder sb = new StringBuilder();
            System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
            ns.Add("", "");
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            System.Xml.XmlWriter myXmlTextWriter = System.Xml.XmlWriter.Create(sb, settings);
            System.Xml.Serialization.XmlSerializer slz = new System.Xml.Serialization.XmlSerializer(data.GetType());
            slz.Serialize(myXmlTextWriter, data, ns);
            return sb.ToString();
        }
    }
}