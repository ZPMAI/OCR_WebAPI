using System;

namespace DBFramework.Mapping
{
	/// <summary>
	/// TableAttribute ��ժҪ˵����
	/// </summary>
    /// <example>[Table(Name = "Menu")]</example>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
	public sealed class TableAttribute : Attribute
	{
		private string name;
        private string dbName;

		/// <summary>
		/// ���ݱ�����
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

        /// <summary>
        /// SQL Server ���ݿ�����, Oracle Schema ����
        /// </summary>
        public string DbName
        {
            get { return dbName; }
            set { dbName = value; }
        }
	}
}
