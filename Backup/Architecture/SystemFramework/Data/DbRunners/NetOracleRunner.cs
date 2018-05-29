using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
    /// <summary>
    /// NetOracleRunner 的摘要说明。
    /// </summary>
    public class NetOracleRunner : AbstractDbRunner
    {
        /// 构造函数
        public NetOracleRunner()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            driver = new NetOracleDriver();
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
