using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DBFramework.Mapping
{
    public class XDataReader : IListSource
    {
        private List<string> columnNames = new List<string>();
        private ArrayList rows = new ArrayList();

        public List<string> ColumnNames
        {
            get { return this.columnNames; }
        }

        public ArrayList Rows
        {
            get { return this.rows; }
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
                return DynamicClass.GetValue(entity, columnName);
            }
        }

        /// ¹¹Ôìº¯Êý
        public XDataReader(IDataReader dataReader)
        {
            string typeName = dataReader.GetSchemaTable().TableName;
            DynamicClass dynamicClass = new DynamicClass(typeName);

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string fieldType = dataReader.GetFieldType(i).Name;
                string name = dataReader.GetName(i);

                dynamicClass.AddProperty(fieldType, name);
                this.columnNames.Add(name);
            }

            dynamicClass.CreateAssembly();

            while (dataReader.Read())
            {
                //object[] itemArray = new object[this.columnNames.Count];
                object entity = dynamicClass.CreateInstance();

                for (int i = 0; i < this.columnNames.Count; i++)
                {
                    object value = dataReader.GetValue(i);
                    DynamicClass.SetValue(entity, columnNames[i], value);
                }

                this.rows.Add(entity);
            }
        }


        public bool ContainsListCollection 
        { 
            get { return true; }
        }

        public IList GetList()
        {
            return this.rows;
        }
    }
}