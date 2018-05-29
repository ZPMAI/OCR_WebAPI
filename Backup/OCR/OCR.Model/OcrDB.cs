using System;
using System.Collections.Generic;
using System.Text;

namespace OCR.Model
{
    /// <summary>
    /// 图片服务器表
    /// </summary>
    public class OcrDBPmsServer
    {
        private int id;

        /// <summary>
        /// 序列号
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string indexCode;
        /// <summary>
        /// 编号
        /// </summary>
        public string IndexCode
        {
            get { return indexCode; }
            set { indexCode = value; }
        }
        private string ip;
        /// <summary>
        /// 图片服务器ip
        /// </summary>
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string port;
        /// <summary>
        /// 服务器端口
        /// </summary>
        public string Port
        {
            get { return port; }
            set { port = value; }
        }
    }
}
