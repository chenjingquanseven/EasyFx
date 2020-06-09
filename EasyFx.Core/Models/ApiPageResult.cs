using System.Collections;

namespace EasyFx.Core.Models
{
    public class ApiPageResult<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 列表
        /// </summary>
        public IEnumerable List { get; set; }
    }
}