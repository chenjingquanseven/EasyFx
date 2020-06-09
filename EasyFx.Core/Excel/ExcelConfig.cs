using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace EasyFx.Core.Excel
{
    public class ExcelConfig<T> where T : class
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }
        /// <summary>
        /// 工作簿
        /// </summary>
        public string SheetName { get; set; }


        public string FileName { get; set; }

        public HashSet<ExcelColumnConfig> ColumnConfigs { get; set; }
    }
}