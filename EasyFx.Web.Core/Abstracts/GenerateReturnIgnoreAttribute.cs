using System;

namespace EasyFx.Web.Core.Abstracts
{
    /// <summary>
    /// 忽略api接口返回格式化 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class GenerateReturnIgnoreAttribute : Attribute
    {

    }
}