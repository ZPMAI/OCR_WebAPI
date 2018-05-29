using System;
using System.Collections.Generic;
using System.Text;

using DBFramework.Runners;

namespace DBFramework
{
    /// <summary>
    /// ������Ԫģʽ
    /// </summary>
    public sealed class DBContext
    {
        private string connectionString;
        //private AbstractRunner msSqlRunner = null;
        //private AbstractRunner oleDbRunner = null;
        private AbstractDbRunner oracleRunner = null;

        //public AbstractRunner MsSql
        //{
        //    get
        //    {
        //        if (msSqlRunner == null)
        //        {
        //            msSqlRunner = new MsSqlRunner(this.connectionString);
        //        }

        //        return msSqlRunner;
        //    }
        //}

        //public AbstractRunner Oledb
        //{
        //    get
        //    {
        //        if (oleDbRunner == null)
        //        {
        //            oleDbRunner = new OleDbRunner(this.connectionString);
        //        }

        //        return oleDbRunner;
        //    }
        //}

        //public AbstractRunner Oracle
        //{
        //    get
        //    {
        //        if (oracleRunner == null)
        //        {
        //            oracleRunner = new OracleRunner(this.connectionString);
        //        }

        //        return oracleRunner;
        //    }
        //}

        public AbstractDbRunner OracleManaged
        {
            get
            {
                if (oracleRunner == null)
                {
                    oracleRunner = new OracleManagedRunner(this.connectionString);
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
