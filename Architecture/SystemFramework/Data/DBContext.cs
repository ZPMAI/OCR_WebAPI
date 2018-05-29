using System;
using System.Collections.Generic;
using System.Text;

using CCT.SystemFramework.Data.DbRunners;

namespace CCT.SystemFramework.Data
{
    public sealed class DBContext
    {
        private string connectionString;
        private AbstractDbRunner   msSqlRunner = null;
        private AbstractDbRunner oleDbRunner = null;
        private AbstractDbRunner oracleRunner = null;

        public AbstractDbRunner MsSql
        {
            get
            {
                if (msSqlRunner == null)
                {
                    msSqlRunner = new SqlServerRunner (this.connectionString);
                }

                return msSqlRunner;
            }
        }

        public AbstractDbRunner Oledb
        {
            get
            {
                if (oleDbRunner == null)
                {
                    oleDbRunner = new  OledbRunner(this.connectionString);
                }

                return oleDbRunner;
            }
        }

        public AbstractDbRunner Oracle
        {
            get
            {
                if (oracleRunner == null)
                {
                    oracleRunner = new OracleRunner(this.connectionString);
                }

                return oracleRunner;
            }
        }

        /// ���캯��
        public DBContext(string connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.connectionString = connectionString;
        }
    }
}
