using System;
using System.Collections.Generic;
using System.Text;

using DBFramework.Drivers;

namespace DBFramework.Runners
{
    public class OracleManagedRunner : AbstractDbRunner
    {
        /// 构造函数
        public OracleManagedRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.driver = new OracleManagedDriver(connectionString);
        }

        protected override string DriverType
        {
            get
            {
                return "ORACLE";
            }
        }
    }
}
