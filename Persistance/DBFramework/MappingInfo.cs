using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

using DBFramework.Mapping;

namespace DBFramework
{
    public class MappingInfo
    {
        private string dbName;
        private string tableName;        
        private string typeName;
        private string incrementName;
        private Dictionary<string, ColumnAttribute> primaryKeyColumns = new Dictionary<string, ColumnAttribute>();
        private Dictionary<string, ColumnAttribute> columns = new Dictionary<string, ColumnAttribute>();
        private Dictionary<string, string> columnPropertys = new Dictionary<string, string>();

        #region 公共属性

        public string DbName
        {
            get { return this.dbName; }
        }

        public string TableName
        {
            get { return this.tableName; }
        }

        public string TypeName
        {
            get { return this.typeName; }
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

        #endregion

        /// 构造函数
        public MappingInfo(Type type)
        {
            this.typeName = type.Name;
            object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            PropertyInfo[] propertyInfos = type.GetProperties();

            if (tableAttributes.Length > 0)
            { 
                TableAttribute tableAttribute = tableAttributes[0] as TableAttribute;
                this.dbName = tableAttribute.DbName;
                this.tableName =  tableAttribute.Name ;
            }
            else
            {
                this.dbName = string.Empty;
                this.tableName = type.Name;
            }

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {               
                object[] columnAttributes = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true);

                if (columnAttributes.Length == 0)
                {
                    this.columns.Add(propertyInfo.Name, null);
                    this.columnPropertys.Add(propertyInfo.Name.ToUpper(), propertyInfo.Name);
                }
                else
                {
                    ColumnAttribute columnAttribute = columnAttributes[0] as ColumnAttribute;
                    this.columns.Add(propertyInfo.Name, columnAttribute);
                    this.columnPropertys.Add(columnAttribute.Name.ToUpper(), propertyInfo.Name);

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
            }
        }

        public bool ContainsColumn(string columnName)
        {
            return this.columnPropertys.ContainsKey(columnName.ToUpper());
        }

        public string GetPropertyName(string columnName)
        {
            return this.columnPropertys[columnName.ToUpper()];
        }
    }


    public class MappingInfo<T> : MappingInfo
    {
        /// 构造函数
        public MappingInfo()
            : base(typeof(T))
        {
        }
    }
}
