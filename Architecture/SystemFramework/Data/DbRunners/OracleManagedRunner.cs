using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
	/// <summary>
	/// OracleRunner ��ժҪ˵����
	/// </summary>
	public class OracleManagedRunner : AbstractDbRunner
	{
		/// ���캯��
		public OracleManagedRunner()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			driver = new OracleManagedDriver();
		}


        public OracleManagedRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
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
