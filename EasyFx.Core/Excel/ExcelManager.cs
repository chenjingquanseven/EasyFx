using EasyFx.Core.Extensions;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;

namespace EasyFx.Core.Excel
{
    public class ExcelManager
    {
        public static byte[] Export<T>(ExcelConfig<T> config) where T : class
        {
            var stream = ExportStream<T>(config);
            return stream.ToArray();
        }

        public static MemoryStream ExportStream<T>(ExcelConfig<T> config) where T : class
        {
            if (config == null)
            {
                throw new ArgumentException(nameof(config));
            }

            
            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add(config.SheetName??"Sheet1");
                var list = config.ColumnConfigs.ToArray();
                //写表头
                for (int i = 1; i <= list.Length; i++)
                {
                    sheet.Cells[1, i].Value = list[i-1].Title;
                }
                //写内容
                for (int i = 1; i <= config.Data.Count; i++)
                {
                    var hashtable = ExpressionCache<T>.GetColumnValues(config.Data[i - 1]);
                    if (hashtable == null)
                    {
                        continue;
                    }

                    for (int j = 1; j <= list.Length; j++)
                    {
                        WriteCellValue(sheet, i + 1, j, list[j-1], hashtable[list[j-1].Name]);
                    }
                }
                
                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
        private static void WriteCellValue(ExcelWorksheet sheet,int row,int col,ExcelColumnConfig column,object value)
        {
            if (value == null)
            {
                return;
            }
            switch (column.ColumnType)
            {
                case ExcelColumnType.Date:
                    DateTime date;
                    if (value is long&&(long)value==0)
                    {
                        sheet.Cells[row, col].Value = "-";
                        return;              
                    }
                    if(value is long unixTimestamp)
                    {
                        date = unixTimestamp.TimestampToDateTime();
                    }
                    else
                    {
                        date = (DateTime)value;
                    }
                    if (!string.IsNullOrWhiteSpace(column.Format))
                    {
                        sheet.Cells[row, col].Style.Numberformat.Format = column.Format;
                        sheet.Cells[row, col].Value =date;
                    }
                    else
                    {
                        sheet.Cells[row, col].Value = date;
                    }
                    break;
                case ExcelColumnType.Enum:
                    sheet.Cells[row, col].Value = value.GetDescription();
                    break;
                case ExcelColumnType.Number:
                case ExcelColumnType.Text:
                default:
                     sheet.Cells[row, col].Value = value;
                     break;
            }
        }
    }
}