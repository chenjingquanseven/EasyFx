using System.Security.Cryptography;

namespace EasyFx.Core.Utils
{
    public class Sha256Helper
    {
        /// <summary>
        /// SHA256
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Encrypt(string str)
        {
            //如果str有中文，不同Encoding的sha是不同的！！
            byte[] SHA256Data = System.Text.Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);
            return by;
        }
    }
}