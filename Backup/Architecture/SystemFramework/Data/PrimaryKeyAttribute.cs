using System;

namespace CCT.SystemFramework.Data
{
	/// <summary>
	/// PrimaryKeyAttribute ��ժҪ˵����
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=false, Inherited=true)]
	public class PrimaryKeyAttribute : Attribute
	{
	}
}
