using System;
using System.Collections.Generic;
using System.Text;

using DBFramework.Drivers;

namespace DBFramework.Runners
{
    public class OracleRunner : AbstractRunner
    {
        /// ���캯��
        public OracleRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.driver = new OracleDriver(connectionString);
        }
    }
}
