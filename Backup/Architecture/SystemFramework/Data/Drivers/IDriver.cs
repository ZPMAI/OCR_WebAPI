using System;
using System.Data;

namespace CCT.SystemFramework.Data.Drivers
{
	/// <summary>
	/// IDriver 的摘要说明。
	/// </summary>
	public interface IDriver
	{
        string GetDbConnectionString();
        IDbConnection GetDbConnection(string connectionString);
        IDbConnection GetDbConnection();

		IDbCommand GetDbCommand();

		IDbDataAdapter GetDbDataAdapter();

		void DeriveParameters(IDbCommand command);		
	}
}
