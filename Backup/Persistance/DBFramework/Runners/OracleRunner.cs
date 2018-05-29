using System;
using System.Collections.Generic;
using System.Text;

using DBFramework.Drivers;

namespace DBFramework.Runners
{
    public class OracleRunner : AbstractRunner
    {
        /// 构造函数
        public OracleRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.driver = new OracleDriver(connectionString);
        }
    }
}
