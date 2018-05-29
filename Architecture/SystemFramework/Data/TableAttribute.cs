using System;

namespace CCT.SystemFramework.Data
{
	/// <summary>
	/// TableAttribute 的摘要说明。
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public sealed class TableAttribute : Attribute
	{
		private string _tableName;

		/// <summary>
		/// 数据表名称
		/// </summary>
		public string TableName
		{
			get { return _tableName; }
			set { _tableName = value; }
		}
	}
}
