namespace EasyFx.Core
{
    /// <summary>
    /// 用户友好异常
    /// </summary>
    public class UserFriendlyException : System.Exception
    {
        public UserFriendlyException(string message) : base(message)
        {

        }

        public UserFriendlyException(int code, string message) : base(message)
        {
            this.Code = code;
        }

        public int Code { get; set; }
    }
}