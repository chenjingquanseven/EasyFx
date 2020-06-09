using System;
using System.Collections.Generic;
using System.Reflection;

namespace EasyFx.Core.Excel
{
    public class ExcelColumnReader<T> where T:class
    {
        private HashSet<ExcelColumnConfig> _configs;
         
        protected virtual HashSet<ExcelColumnConfig> Initialize()
        {
            HashSet<ExcelColumnConfig> configs=new HashSet<ExcelColumnConfig>();
            var type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(ExcelColumnAttribute), false))
                {
                    var attribute = prop.GetCustomAttribute<ExcelColumnAttribute>();
                    if (attribute == null)
                    {
                        continue;
                    }
                    configs.Add(new ExcelColumnConfig(prop.Name, attribute.Title,
                        ChangeColumnType(attribute, prop.PropertyType),
                        attribute.Format));
                }
            }

            return configs;
        }

        public HashSet<ExcelColumnConfig> ColumnConfigs
        {
            get 
            {
                if (_configs == null)
                {
                    _configs=Initialize();
                }
                return _configs;
            }
        }

        private ExcelColumnType ChangeColumnType(ExcelColumnAttribute attribute,Type type)
        {
            if (attribute.ColumnType != ExcelColumnType.Default)
            {
                return attribute.ColumnType;
            }
            if (type.IsEnum)
            {
                return ExcelColumnType.Enum;
            }
            string typeStr = type.Name.ToLower();
            
            switch (typeStr)
            {
                case "int16":
                case "int32":
                case "int64":
                case "long":
                case "short":
                case "double":
                case "decimal":
                case "byte":
                case "single":
                case "sbyte":
                case "uint16":
                case "uint32":
                case "uint64":
                    return ExcelColumnType.Number;
                case "datetime":
                    return ExcelColumnType.Date;
                case "nullable`1":
                case "string":
                    return ExcelColumnType.Text;
                default: 
                    throw new TypeAccessException("无法访问的类型");
            }
        }
    }
}