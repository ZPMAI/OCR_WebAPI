using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace OCR.Model
{
    /// <summary>
    /// CTOS接口返回值
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

        //错误代码，0表示调用成功
        public string ERRORCODE;
        //错误说明，可以为空
        public string ERRORMSG;
        //返回结果集
        public DataSet DS;
        //返回值清单
        public Dictionary<string, string> DIC;

    }
}
