using System.Security.Cryptography;
using System.Text;

namespace EasyFx.Core.Utils
{
    /// <summary>
    /// MD5
    /// </summary>
    public class MD5Helper
    {
        public static string Encrypt(string md5Str)
        {
            var md5Obj = MD5.Create();
            var sBuilder = new StringBuilder();
            byte[] data = md5Obj.ComputeHash(Encoding.UTF8.GetBytes(md5Str));
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}