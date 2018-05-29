using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
	/// <summary>
	/// SqlRunner 的摘要说明。
	/// </summary>
	public class SqlServerRunner : AbstractDbRunner
	{
		/// 构造函数
		public SqlServerRunner()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			driver = new SqlServerDriver();
		}

        /// 构造函数
        public SqlServerRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.driver = new SqlServerDriver(connectionString);
        }

	}
}
