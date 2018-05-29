using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace OCR.Model
{
    /// <summary>
    /// CTOS�ӿڷ���ֵ
    /// </summary>
    [Serializable]
    public class CtosResult
    {
        public CtosResult()
        {
            ERRORCODE = string.Empty;
            ERRORMSG = string.Empty;
            DS = new DataSet();
            DIC = new Dictionary<string, string>();
        }

        //������룬0��ʾ���óɹ�
        public string ERRORCODE;
        //����˵��������Ϊ��
        public string ERRORMSG;
        //���ؽ����
        public DataSet DS;
        //����ֵ�嵥
        public Dictionary<string, string> DIC;

    }
}
