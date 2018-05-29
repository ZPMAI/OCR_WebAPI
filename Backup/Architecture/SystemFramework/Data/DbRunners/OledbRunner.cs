using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
	/// <summary>
	/// OledbRunner 的摘要说明。
	/// </summary>
	public class OledbRunner : AbstractDbRunner
	{
		/// 构造函数
		public OledbRunner()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.driver = new OledbDriver();
		}

        public OledbRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.driver = new OledbDriver(connectionString);
        }
	}
}
