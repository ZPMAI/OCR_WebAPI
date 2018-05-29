using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
	/// <summary>
	/// OledbRunner ��ժҪ˵����
	/// </summary>
	public class OledbRunner : AbstractDbRunner
	{
		/// ���캯��
		public OledbRunner()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			this.driver = new OledbDriver();
		}

        public OledbRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.driver = new OledbDriver(connectionString);
        }
	}
}
