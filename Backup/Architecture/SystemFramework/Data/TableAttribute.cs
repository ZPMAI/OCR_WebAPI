using System;

namespace CCT.SystemFramework.Data
{
	/// <summary>
	/// TableAttribute ��ժҪ˵����
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public sealed class TableAttribute : Attribute
	{
		private string _tableName;

		/// <summary>
		/// ���ݱ�����
		/// </summary>
		public string TableName
		{
			get { return _tableName; }
			set { _tableName = value; }
		}
	}
}
