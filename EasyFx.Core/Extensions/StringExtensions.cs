namespace EasyFx.Core.Extensions
{
    public static partial class StringExtensions
    {
        public static string TrimAll(this string data)
        {
            string res = string.Empty;
            foreach (var item in data)
            {
                if (!char.IsWhiteSpace(item))
                {
                    res += item;
                }
            }

            return res;
        }

        public static string ToCamcel(this string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return data;
            }
            var first = data[0];
            return first.ToString().ToLower() + data.Substring(1, data.Length - 1);
        }

        public static long GetFileSize(this string base64)
        {
            //去掉base64后的等号
            base64 = base64.Replace("=", "");
            var len = base64.Length;
            return len - (len / 8) * 2;
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }


        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
    }
}