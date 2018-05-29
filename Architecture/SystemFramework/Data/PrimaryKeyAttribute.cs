using System;

namespace CCT.SystemFramework.Data
{
	/// <summary>
	/// PrimaryKeyAttribute 的摘要说明。
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=false, Inherited=true)]
	public class PrimaryKeyAttribute : Attribute
	{
	}
}
