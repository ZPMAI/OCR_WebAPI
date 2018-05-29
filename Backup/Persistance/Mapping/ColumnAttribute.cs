using System;
using System.Data;

namespace DBFramework.Mapping
{
	/// <summary>
    /// ColumnAttribute ��ժҪ˵����
	/// </summary>
    /// <example>[Column(Name="ID", DbType=DbType.Int32, Length=4, IsPrimaryKey=true, IsIncrement=true)]</example>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=true, Inherited=true)]
    public class ColumnAttribute : Attribute
	{
        private string name;
        private DbType dbType;
        private int length;
        private bool isPrimaryKey = false;
        private bool isIncrement = false;
        
        /// <summary>
        /// �ֶ�����
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// �ֶ�����
        /// </summary>
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// �ֶ�����
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsIncrement
        {
            get { return isIncrement; }
            set { isIncrement = value; }
        }
	}
}
