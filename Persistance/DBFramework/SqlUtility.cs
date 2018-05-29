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
            // ȥ���洢���̲��������� '@' ':' 'p_' ���ַ�ǰ׺���Ա��� T.PropertyName һ��
            string c1 = parameterName.Substring(0, 1);
            if (c1.Equals("@")|| c1.Equals(":")) return parameterName.Substring(1);

            string c2 = parameterName.Substring(0, 2);
            if (string.Compare(c2, "p_", true) == 0) return parameterName.Substring(2);

            return parameterName;
        }

        #region �� T ��ȡ����ֵ�������ݿ�����������

        /// <summary>
        /// �� T ��ȡ����ֵ�������ݿ�����������
        /// </summary>
        /// <param name="dataParameters">SqlParameter���󼯺�</param>
        /// <param name="entity">ʵ�����</param>
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

        #region �����ݿ�����������ȡ����ֵ���� T.Propertys

        /// <summary>
        /// �����ݿ�����������ȡ����ֵ���� T.Propertys
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <param name="dataParameters">IDataParameter���󼯺�</param>
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

        #region �� IDataReader ����ת��Ϊ T ����

        /// <summary>
        /// �� IDataReader ����ת��Ϊ T ����
        /// </summary>
        /// <param name="dataReader">IDataReader ����</param>
        /// <returns>T ����</returns>
        internal static List<T> DataReaderToEntity<T>(IDataReader dataReader)
        {            
            TableInfo<T> tableInfo = new TableInfo<T>();
            List<T> list = new List<T>();

            // Ϊ�˽�ʡʱ��, ���Ե���һ�� GetOrdinal, Ȼ�󽫽����������������Ա���ѭ����ʹ�á�
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
                    // ��ָ�������Ʋ�����Ч�������ƣ������������������ݿ��ֶ����Ʋ�ƥ��
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
