using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using DBFramework.Mapping;
using DBFramework.MSIL;

namespace DBFramework
{
    static class SqlUtil
    {
        public static string RemovePrefix(string parameterName)
        {
            // 去除存储过程参数名称中 '@' ':' 'p_' 等字符前缀，以便与 T.PropertyName 一致
            string c1 = parameterName.Substring(0, 1);

            if (c1.Equals("@") || c1.Equals(":"))
            {
                return parameterName.Substring(1);
            }

            string c2 = parameterName.Substring(0, 2);

            if (string.Compare(c2, "p_", true) == 0)
            {
                return parameterName.Substring(2);
            }

            return parameterName;
        }

        internal static void SetValue(object target, string propertyName, object value)
        {
            Type type = target.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            propertyInfo.SetValue(target, Convert.ChangeType(value, propertyInfo.PropertyType), null);

            //object[] args = new object[] {
            //    Convert.ChangeType(value, value.GetType()) 
            //};

            //target.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, target, args);
        }

        internal static object GetValue(object target, string propertyName)
        {
            Type type = target.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            return propertyInfo.GetValue(target, null);

            //object[] args = new object[] { };
            //object returnValue = target.GetType().InvokeMember(propertyName, BindingFlags.GetProperty, null, target, args);

            //if (returnValue == null)
            //{
            //    return DBNull.Value;
            //}

            //return returnValue;
        }

        #region 从实体对象中取参数值赋给数据库操作命令参数

        /// <summary>
        /// 从实体对象中取参数值赋给数据库操作命令参数
        /// </summary>
        /// <param name="dataParameters">SqlParameter对象集合</param>
        /// <param name="entity">实体对象</param>
        /// <remarks>准备采用 MSIL 技术</remarks>
        internal static void AssignParameters(IDataParameterCollection dataParameters, object entity)
        {
            foreach (IDataParameter dataParameter in dataParameters)
            {
                if (dataParameter.Direction == ParameterDirection.Input
                    || dataParameter.Direction == ParameterDirection.InputOutput)
                {
                    string propertyName = RemovePrefix(dataParameter.ParameterName);
                    object value = SqlUtil.GetValue(entity, propertyName);

                    dataParameter.Value = (value == null) ? DBNull.Value : value;
                }
            }
        }


        #endregion

        #region 从数据库操作命令参数取返回值赋给 Entity.Properties

        /// <summary>
        /// 从数据库操作命令参数取返回值赋给 Entity.Properties
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="dataParameters">IDataParameter对象集合</param>
        internal static void AssignProperties(object entity, IDataParameterCollection dataParameters)
        {
            foreach (IDataParameter dataParameter in dataParameters)
            {
                if (dataParameter.Direction == ParameterDirection.InputOutput
                    || dataParameter.Direction == ParameterDirection.Output)
                {
                    string propertyName = RemovePrefix(dataParameter.ParameterName);
                    SqlUtil.SetValue(entity, propertyName, dataParameter.Value);
                }
            }
        }


        #endregion

        #region 从 DataRow 对象中取参数值赋给数据库操作命令参数

        /// <summary>
        /// 从 DataRow 对象中取参数值赋给数据库操作命令参数
        /// </summary>
        /// <param name="dataParameters">DataParameter对象集合</param>
        /// <param name="dataRow">参数值(DataRow)</param>
        internal static void AssignParameterValues(IDataParameterCollection dataParameters, DataRow dataRow)
        {
            foreach (IDataParameter dataParameter in dataParameters)
            {
                string parameterName = SqlUtil.RemovePrefix(dataParameter.ParameterName);

                if (dataRow.Table.Columns.IndexOf(parameterName) != -1)
                {
                    dataParameter.Value = dataRow[parameterName];
                }
            }
        }


        #endregion

        #region 从数据库操作命令参数取返回值赋给 DataRow 对象

        /// <summary>
        /// 从数据库操作命令参数取返回值赋给 DataRow 对象
        /// </summary>
        /// <param name="dataRow">DataRow 对象</param>
        /// <param name="dataParameters">IDataParameter 对象集合</param>
        internal static void AssignDataRowValues(DataRow dataRow, IDataParameterCollection dataParameters)
        {
            foreach (IDataParameter dataParameter in dataParameters)
            {
                if ((dataParameter.Direction == ParameterDirection.InputOutput)
                    || (dataParameter.Direction == ParameterDirection.Output))
                {
                    string parameterName = SqlUtil.RemovePrefix(dataParameter.ParameterName);

                    if (dataRow.Table.Columns.IndexOf(parameterName) != -1)
                    {
                        dataRow[parameterName] = dataParameter.Value;
                    }
                }
            }
        }

        #endregion
    }
}
