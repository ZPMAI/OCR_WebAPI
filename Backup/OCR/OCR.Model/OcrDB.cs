using System;
using System.Collections.Generic;
using System.Text;

namespace OCR.Model
{
    /// <summary>
    /// ͼƬ��������
    /// </summary>
    public class OcrDBPmsServer
    {
        private int id;

        /// <summary>
        /// ���к�
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string indexCode;
        /// <summary>
        /// ���
        /// </summary>
        public string IndexCode
        {
            get { return indexCode; }
            set { indexCode = value; }
        }
        private string ip;
        /// <summary>
        /// ͼƬ������ip
        /// </summary>
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string port;
        /// <summary>
        /// �������˿�
        /// </summary>
        public string Port
        {
            get { return port; }
            set { port = value; }
        }
    }
}
