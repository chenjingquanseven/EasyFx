namespace EasyFx.Core.Excel
{
    public class ExcelColumnConfig
    {
        public ExcelColumnConfig(string name,string title,ExcelColumnType columnType=ExcelColumnType.Text,string format=null)
        {
            this.Name = name;
            this.Title = title;
            this.ColumnType = columnType;
            this.Format = format;
        }
        public string Title { get;private set; }

        public string Name { get; private set; }

        public string Format { get; private set; }

        public ExcelColumnType ColumnType { get;private set; }


        public override int GetHashCode()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return 0;
            }
            return Name.GetHashCode();
        }

        public static bool operator ==(ExcelColumnConfig a, ExcelColumnConfig b)
        {
            return a?.GetHashCode() == b?.GetHashCode();
        }

        public static bool operator !=(ExcelColumnConfig a, ExcelColumnConfig b)
        {
            return a?.GetHashCode() != b?.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null|| !(obj is ExcelColumnConfig))
            {
                return false;
            }
            ExcelColumnConfig b=obj as ExcelColumnConfig;

            return this.GetHashCode().Equals(b.GetHashCode());
        }
    }
}