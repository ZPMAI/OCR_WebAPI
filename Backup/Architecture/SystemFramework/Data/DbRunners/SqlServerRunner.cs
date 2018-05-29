using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
	/// <summary>
	/// SqlRunner ��ժҪ˵����
	/// </summary>
	public class SqlServerRunner : AbstractDbRunner
	{
		/// ���캯��
		public SqlServerRunner()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			driver = new SqlServerDriver();
		}

        /// ���캯��
        public SqlServerRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.driver = new SqlServerDriver(connectionString);
        }

	}
}
