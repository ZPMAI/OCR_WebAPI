using System;
using System.Collections.Generic;
using System.Text;

using DBFramework.Drivers;

namespace DBFramework.Runners
{
    public  class OleDbRunner : AbstractRunner
    {               
        /// ���캯��
        public OleDbRunner(string connectionString)
            : base(connectionString)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.driver = new OleDbDriver(connectionString);
        }
    }
}
