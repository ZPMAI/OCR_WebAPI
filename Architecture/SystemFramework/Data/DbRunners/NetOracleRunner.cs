using System;

using CCT.SystemFramework.Data.Drivers;

namespace CCT.SystemFramework.Data.DbRunners
{
    /// <summary>
    /// NetOracleRunner ��ժҪ˵����
    /// </summary>
    public class NetOracleRunner : AbstractDbRunner
    {
        /// ���캯��
        public NetOracleRunner()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            driver = new NetOracleDriver();
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
