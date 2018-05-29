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
            // ȥ���洢���̲��������� '@' ':' 'p_' ���ַ�ǰ׺���Ա��� T.PropertyName һ��
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

        #region ��ʵ�������ȡ����ֵ�������ݿ�����������

        /// <summary>
        /// ��ʵ�������ȡ����ֵ�������ݿ�����������
        /// </summary>
        /// <param name="dataParameters">SqlParameter���󼯺�</param>
        /// <param name="entity">ʵ�����</param>
        /// <remarks>׼������ MSIL ����</remarks>
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

        #region �����ݿ�����������ȡ����ֵ���� Entity.Properties

        /// <summary>
        /// �����ݿ�����������ȡ����ֵ���� Entity.Properties
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <param name="dataParameters">IDataParameter���󼯺�</param>
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

        #region �� DataRow ������ȡ����ֵ�������ݿ�����������

        /// <summary>
        /// �� DataRow ������ȡ����ֵ�������ݿ�����������
        /// </summary>
        /// <param name="dataParameters">DataParameter���󼯺�</param>
        /// <param name="dataRow">����ֵ(DataRow)</param>
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

        #region �����ݿ�����������ȡ����ֵ���� DataRow ����

        /// <summary>
        /// �����ݿ�����������ȡ����ֵ���� DataRow ����
        /// </summary>
        /// <param name="dataRow">DataRow ����</param>
        /// <param name="dataParameters">IDataParameter ���󼯺�</param>
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
