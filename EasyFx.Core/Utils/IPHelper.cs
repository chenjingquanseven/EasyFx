using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace EasyFx.Core.Utils
{
    public class IPHelper
    {
        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public static string GetDnsIp()
        {
            var ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(i => i.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            return ipAddress?.ToString();
        }
    }
}