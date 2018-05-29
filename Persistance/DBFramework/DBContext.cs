using System;
using System.Collections.Generic;
using System.Text;

using DBFramework.Runners;

namespace DBFramework
{
    /// <summary>
    /// 采用享元模式
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

        /// 构造函数
        public DBContext(string connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.connectionString = connectionString;
        }
    }
}
