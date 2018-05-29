using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DBFramework.Mapping
{
    public class TableInfo
    {
        private string tableName;
        private Type type;
        private string incrementName;
        private Dictionary<string, ColumnAttribute> primaryKeyColumns = new Dictionary<string, ColumnAttribute>();
        private Dictionary<string, ColumnAttribute> columns = new Dictionary<string, ColumnAttribute>();

        public string TableName
        {
            get { return this.tableName; }
        }

        public string IncrementName
        {
            get { return this.incrementName; }
        }

        public Dictionary<string, ColumnAttribute> PrimaryKeyColumns
        {
            get { return primaryKeyColumns; }
        }

        public Dictionary<string, ColumnAttribute> Columns
        {
            get { return this.columns; }
        }


        /// 构造函数
        public TableInfo(Type type)
        {
            this.type = type;

            object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);

            if (tableAttributes.Length > 0)
            {
                TableAttribute tableAttribute = tableAttributes[0] as TableAttribute;
                this.tableName = tableAttribute.Name;
            }
            else
            {
                this.tableName = type.Name;
            }

            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true);

                if (columnAttributes.Length > 0)
                {
                    ColumnAttribute columnAttribute = columnAttributes[0] as ColumnAttribute;

                    this.columns.Add(propertyInfo.Name, columnAttribute);

                    // 主键列
                    if (columnAttribute.IsPrimaryKey)
                    {
                        this.primaryKeyColumns.Add(propertyInfo.Name, columnAttribute);
                    }

                    // 自增列
                    if (columnAttribute.IsIncrement)
                    {
                        this.incrementName = propertyInfo.Name;
                    }
                }
                else
                {
                    this.columns.Add(propertyInfo.Name, null);
                }
            }
        }


        public void SetValue(object obj, string propertyName, object value)
        {
            PropertyInfo propertyInfo = this.type.GetProperty(propertyName);

            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(obj, value, null);
            }
        }


        public object GetValue(object obj, string propertyName)
        {
            object returnValue = null;
            PropertyInfo propertyInfo = this.type.GetProperty(propertyName);            

            if (propertyInfo != null
                && propertyInfo.CanRead)
            {
                returnValue = propertyInfo.GetValue(obj, null);
            }

            if (returnValue == null)
            {
                return DBNull.Value;                
            }

            return returnValue;
        }
    }


    public class TableInfo<T> : TableInfo
    {
        /// 构造函数
        public TableInfo()
            : base(typeof(T))
        {
        }
    }
}
