using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
	/// <summary>
	/// OracleRunner 的摘要说明。
	/// </summary>
	public class OracleRunner : AbstractDbRunner
	{
		/// 构造函数
		public OracleRunner()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			driver = new OracleDriver();
		}


        public OracleRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.driver = new OracleDriver(connectionString);
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
