using System;
using System.Security.Cryptography;
using System.Text;

namespace EasyFx.Core.Utils
{
    /// <summary>
    /// aes
    /// </summary>
    public class AESHelper
    {
        /// <summary>
        /// AES加密(UTF8)
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="keyBytes"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, byte[] keyBytes, CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyBytes;

            rDel.Mode = cipherMode;
            rDel.Padding = paddingMode;
            rDel.BlockSize = 128;
            byte[] iv = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
            rDel.IV = iv;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="keyBytes"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>

        public static string Decrypt(string toDecrypt, byte[] keyBytes, CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyBytes;
            rDel.Mode = cipherMode;
            rDel.Padding = paddingMode;
            byte[] iv = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
            rDel.IV = iv;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);

        }
    }
}