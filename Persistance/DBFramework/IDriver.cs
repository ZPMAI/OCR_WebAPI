using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DBFramework
{
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
