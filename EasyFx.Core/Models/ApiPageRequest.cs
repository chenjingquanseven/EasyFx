namespace EasyFx.Core.Models
{
    /// <summary>
    /// 分页
    /// </summary>
    public class ApiPageRequest
    {
        public ApiPageRequest()
        {
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 是否正序排序
        /// </summary>
        public bool OrderAsc { get; set; } = false;
    }
}