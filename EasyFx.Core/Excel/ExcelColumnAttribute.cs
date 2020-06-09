using System;

namespace EasyFx.Core.Excel
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelColumnAttribute : Attribute
    {
        public ExcelColumnAttribute(string title,ExcelColumnType columnType=ExcelColumnType.Default,string format=null)
        {
            this.Title = title;
            this.ColumnType = columnType;
            this.Format = format;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public ExcelColumnType ColumnType { get;private set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; private set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string Title { get; private set; }

    }
}