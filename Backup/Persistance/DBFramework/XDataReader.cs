using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using DBFramework.MSIL;

namespace DBFramework
{
    /// <remarks>
    /// ��ʹ�� List<object[]>
    /// </remarks>
    public class XDataReader : IListSource
    {
        private List<string> columnNames = new List<string>();
        private IList rows = new ArrayList();

        #region ��������
        
        public bool ContainsListCollection
        {
            get { return true; }
        }

        public List<string> ColumnNames
        {
            get { return this.columnNames; }
        }

        //public ArrayList Rows
        //{
        //    get { return this.rows; }
        //}

        public int Count
        {
            get { return this.rows.Count; }
        }

        public object this[int rowIndex, int columnIndex]
        {
            get
            {
                string columnName = columnNames[columnIndex];
                return this[rowIndex, columnName];
            }
        }

        public object this[int rowIndex, string columnName]
        {
            get
            {
                object entity = rows[rowIndex];
                return SqlUtil.GetValue(entity, columnName);
            }
        }

        #endregion

        /// ���캯��
        public XDataReader(IDataReader dataReader)
        {
            // dataReader.GetSchemaTable().TableName �ܷ��� SchemaTable
            // ��û��������������Ψһ�����ƣ������û��汣�涯̬���ɵ���
            string typeName = dataReader.GetSchemaTable().TableName;

            #warning �õط�д���˳��򼯵����� CCT.DynamicModel

            DynamicClass dynamicClass = new DynamicClass("CCT.DynamicModel", typeName);

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string name = dataReader.GetName(i);
                Type fieldType = dataReader.GetFieldType(i);

                dynamicClass.AddProperty(name, fieldType);
                this.columnNames.Add(name);
            }

            dynamicClass.CreateAssembly();

            // DynamicBuilder<object> dynamicBuilder = new DynamicBuilder<object>();
            // dynamicBuilder.CreateEntity(dataReader);

            DynamicBuilder dynamicBuilder = new DynamicBuilder(dynamicClass.ClassType, dataReader);
            
            while (dataReader.Read())
            {
                object entity = dynamicBuilder.CreateEntity(dataReader);

                this.rows.Add(entity);
            }

            dataReader.Close();
        }


        public IList GetList()
        {
            return this.rows;
        }
    }
}