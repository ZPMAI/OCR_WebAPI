using System;
using System.Data;

namespace DBFramework.Mapping
{
	/// <summary>
    /// ColumnAttribute 的摘要说明。
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
        /// 字段名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIncrement
        {
            get { return isIncrement; }
            set { isIncrement = value; }
        }
	}
}
