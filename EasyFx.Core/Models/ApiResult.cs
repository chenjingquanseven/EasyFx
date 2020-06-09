namespace EasyFx.Core.Models
{
    public class ApiResult
    {
        /// <summary>
        /// Api响应码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Api响应数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Api响应消息
        /// </summary>
        public string Message { get; set; }
    }
}