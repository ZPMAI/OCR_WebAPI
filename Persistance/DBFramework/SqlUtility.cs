using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using DBFramework.Mapping;

namespace DBFramework
{
    static class SqlUtility
    {
        public static string RemovePrefix(string parameterName)
        {
            // 去除存储过程参数名称中 '@' ':' 'p_' 等字符前缀，以便与 T.PropertyName 一致
            string c1 = parameterName.Substring(0, 1);
            if (c1.Equals("@")|| c1.Equals(":")) return parameterName.Substring(1);

            string c2 = parameterName.Substring(0, 2);
            if (string.Compare(c2, "p_", true) == 0) return parameterName.Substring(2);

            return parameterName;
        }

        #region 从 T 中取参数值赋给数据库操作命令参数

        /// <summary>
        /// 从 T 中取参数值赋给数据库操作命令参数
        /// </summary>
        /// <param name="dataParameters">SqlParameter对象集合</param>
        /// <param name="entity">实体对象</param>
        internal static void AssignParameterValues(IDataParameterCollection dataParameters, object entity)
        {
            Type type = entity.GetType();
            TableInfo tableInfo = new TableInfo(type);

            foreach (IDataParameter dataParameter in dataParameters)
            {
                string propertyName = RemovePrefix(dataParameter.ParameterName);
                object value = tableInfo.GetValue(entity, propertyName);

                dataParameter.Value =  (value == null) ? DBNull.Value : value;
            }
        }


        #endregion

        #region 从数据库操作命令参数取返回值赋给 T.Propertys

        /// <summary>
        /// 从数据库操作命令参数取返回值赋给 T.Propertys
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="dataParameters">IDataParameter对象集合</param>
        internal static void AssignEntityProperties(object entity, IDataParameterCollection dataParameters)
        {
            Type type = entity.GetType();
            TableInfo tableInfo = new TableInfo(type);

            foreach (IDataParameter dataParameter in dataParameters)
            {
                if (dataParameter.Direction == ParameterDirection.InputOutput
                    || dataParameter.Direction == ParameterDirection.Output)
                {                    
                    string propertyName = RemovePrefix(dataParameter.ParameterName);
                    tableInfo.SetValue(entity, propertyName, dataParameter.Value);
                }
            }
        }


        #endregion

        #region 把 IDataReader 对象转换为 T 集合

        /// <summary>
        /// 把 IDataReader 对象转换为 T 集合
        /// </summary>
        /// <param name="dataReader">IDataReader 对象</param>
        /// <returns>T 集合</returns>
        internal static List<T> DataReaderToEntity<T>(IDataReader dataReader)
        {            
            TableInfo<T> tableInfo = new TableInfo<T>();
            List<T> list = new List<T>();

            // 为了节省时间, 可以调用一次 GetOrdinal, 然后将结果分配给整数变量以便在循环中使用。
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (string propertyName in tableInfo.Columns.Keys)
            {
                int index = -1;

                try
                {
                    ColumnAttribute columnAttribute = tableInfo.Columns[propertyName];

                    if (columnAttribute != null)
                    {
                        string columnName = columnAttribute.Name;
                        index = dataReader.GetOrdinal(columnName);
                    }
                    else
                    {
                        index = dataReader.GetOrdinal(propertyName);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // 若指定的名称不是有效的列名称，即类属性名称与数据库字段名称不匹配
                }

                dictionary.Add(propertyName, index);
            }

            while (dataReader.Read())
            {
                T entity = Activator.CreateInstance<T>();

                foreach (string propertyName in tableInfo.Columns.Keys)
                {
                    int index = dictionary[propertyName];

                    if (index != -1
                        && dataReader.GetValue(index) != DBNull.Value)
                    {
                        object value = dataReader.GetValue(index);
                        tableInfo.SetValue(entity, propertyName, value);
                    }
                }

                list.Add(entity);
            }

            dataReader.Close();

            return list;
        }

        #endregion
    }
}
