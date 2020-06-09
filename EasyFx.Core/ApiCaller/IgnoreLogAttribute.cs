using System;

namespace EasyFx.Core.ApiCaller
{
    /// <summary>
    /// 忽略日志输出
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public class IgnoreLogAttribute : Attribute
    {

    }
}