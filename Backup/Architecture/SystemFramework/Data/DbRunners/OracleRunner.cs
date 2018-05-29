using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
	/// <summary>
	/// OracleRunner ��ժҪ˵����
	/// </summary>
	public class OracleRunner : AbstractDbRunner
	{
		/// ���캯��
		public OracleRunner()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			driver = new OracleDriver();
		}


        public OracleRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
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
