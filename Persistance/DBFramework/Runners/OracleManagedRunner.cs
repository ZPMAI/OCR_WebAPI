using System;
using System.Collections.Generic;
using System.Text;

using DBFramework.Drivers;

namespace DBFramework.Runners
{
    public class OracleManagedRunner : AbstractDbRunner
    {
        /// ���캯��
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
