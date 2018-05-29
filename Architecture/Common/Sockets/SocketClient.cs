using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Net; 
using System.Net.Sockets;

using CCT.Common.Reflection;

namespace CCT.Common.Sockets
{    
    /// <summary>
    /// SocketClient ��ժҪ˵����
    /// </summary>
    public class SocketClient : IDisposable
    {
        const int BUFFERSIZE = 8192;
        
        public event DisconnectDelegate DisconnectEvent;
        public event ReceiveCompleteDelegate ReceiveCompleteEvent;
        
        private byte[] buffer = new byte[BUFFERSIZE];

        // �� List<byte> ���� byte[] �᲻��ȽϺķ���Դ��        
        //private byte[] content = new byte[0];        
        private List<byte> content = new List<byte>();        
        private string clientIP;
        private Socket socket;

        #region ��������

        public string ClientIP
        {
            get { return this.clientIP; }
            set { this.clientIP = value; }
        }

        public bool Connected
        {
            get { return this.socket.Connected; }
        }

        #endregion

        #region ���캯��

        /// ���캯��
        public SocketClient(string serverIP, int port)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            string localName = Dns.GetHostName();
            this.clientIP = Dns.GetHostEntry(localName).AddressList[0].ToString();

            IPAddress ipAddress = IPAddress.Parse(serverIP);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Connect(ipEndPoint);
        }


        /// ���캯��
        /// <remarks>���Ƹù��캯��ֻ���� SocketServer ����</remarks>
        internal SocketClient(Socket socket)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.socket = socket;
        }

        #endregion

        /// �����������ݶ�ȡ���ڴ���
        private void Store(byte[] buffer, int count)
        {
            // ��ǰ������������ + �ӷ�������ȡ������
            for (int i = 0; i < count; i++)
            {
                this.content.Add(buffer[i]);
            }
        }


        /// ��ʼ�����ӵ� Socket �첽��������
        public void Receive()
        {
            if (!this.socket.Connected) { return; }
            
            this.socket.BeginReceive(
                this.buffer, 
                0, 
                BUFFERSIZE, 
                SocketFlags.None,
                new AsyncCallback(this.ReceiveCallBack), 
                this.socket
                );
        }


        /// ����������첽��ȡ 
        private void ReceiveCallBack(IAsyncResult asyncResult)
        {            
            try
            {
                int count;

                lock (this.socket)
                {
                    count = this.socket.EndReceive(asyncResult);
                }
                
                Store(buffer, count);

                // �˴���ͣ 0 �����Ա����ݻ���
                Thread.Sleep(0);

                // ��ȡ�Ѿ�����������ҿɹ���ȡ��������
                if (this.socket.Available > 0)
                {
                    lock (this.socket)
                    {
                        // �ݹ������������ֱ���������
                        Receive();
                    }

                    return;
                }

                // ȫ����������򴥷� ReceiveComplete �¼�
                if (this.content.Count > 0)
                {
                    OnReceiveComplete();

                    this.content.Clear();
                }                
            }
            catch (SocketException)
            {
                Close();
            }

            // Ϊʲô�� Receive 2 ��ͬ������Ϣ��, ��ȷ�Ϸ�����ֻ�� 1 ����Ϣ, �ѵ��ǿͻ��˻����������������ɵ�?
            // ��ʼ��һ�ν���
            Receive();
        }


        /// �������ݽ�������¼�
        private void OnReceiveComplete()
        {
            if (ReceiveCompleteEvent == null) { return; }

            IProtocol protocol = SerializeHelper.Deserialize<IProtocol>(this.content.ToArray());            
            ProtocolEventArgs e = new ProtocolEventArgs();
            e.Protocol = protocol;            

            ReceiveCompleteEvent(this, e);
        }


        /// �������첽���͵����ӵ� Socket
        public void Send<T>(T obj)
            where T : IProtocol, new()
        {
            byte[] content = SerializeHelper.Serialize<T>(obj);

            lock (this.socket)
            {
                this.socket.Send(content);

                //this.socket.BeginSend(
                //    content,
                //    0,
                //    content.Length,
                //    SocketFlags.None,
                //    new AsyncCallback(SendCallBack),
                //    this.socket
                //    );
            }
        }


        // ���ܲ����첽���, �����뷢�ͷ�����ͻ?
        //private void SendCallBack(IAsyncResult asyncResult)
        //{
        //    int count = this.socket.EndSend(asyncResult);

        //    if (SendCompleteEvent != null)
        //    {
        //        SendCompleteEvent(this, EventArgs.Empty);
        //    }

        //    ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        //    manualResetEvent.Set();
        //    manualResetEvent.WaitOne();
        //}

        #region IDisposable ��Ա

        /// �ر� Socket ���Ӳ��ͷ����й�������Դ
        public void Close()
        {
            if (!this.socket.Connected)
            {                
                this.socket.Close();

                if (this.DisconnectEvent != null)
                {
                    this.DisconnectEvent(this, EventArgs.Empty);
                }
            }
        }


        /// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
