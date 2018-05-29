using System;
using System.Data;

namespace CCT.SystemFramework.Data.Drivers
{
	/// <summary>
	/// IDriver ��ժҪ˵����
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
