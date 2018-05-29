using System;

namespace DBFramework.Mapping
{
	/// <summary>
	/// TableAttribute 的摘要说明。
	/// </summary>
    /// <example>[Table(Name = "Menu")]</example>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
	public sealed class TableAttribute : Attribute
	{
		private string name;
        private string dbName;

		/// <summary>
		/// 数据表名称
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

        /// <summary>
        /// SQL Server 数据库名称, Oracle Schema 名称
        /// </summary>
        public string DbName
        {
            get { return dbName; }
            set { dbName = value; }
        }
	}
}
