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
    /// SocketClient 的摘要说明。
    /// </summary>
    public class SocketClient : IDisposable
    {
        const int BUFFERSIZE = 8192;
        
        public event DisconnectDelegate DisconnectEvent;
        public event ReceiveCompleteDelegate ReceiveCompleteEvent;
        
        private byte[] buffer = new byte[BUFFERSIZE];

        // 用 List<byte> 代替 byte[] 会不会比较耗费资源？        
        //private byte[] content = new byte[0];        
        private List<byte> content = new List<byte>();        
        private string clientIP;
        private Socket socket;

        #region 公共属性

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

        #region 构造函数

        /// 构造函数
        public SocketClient(string serverIP, int port)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            string localName = Dns.GetHostName();
            this.clientIP = Dns.GetHostEntry(localName).AddressList[0].ToString();

            IPAddress ipAddress = IPAddress.Parse(serverIP);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Connect(ipEndPoint);
        }


        /// 构造函数
        /// <remarks>限制该构造函数只能由 SocketServer 调用</remarks>
        internal SocketClient(Socket socket)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.socket = socket;
        }

        #endregion

        /// 将缓冲区数据读取到内存区
        private void Store(byte[] buffer, int count)
        {
            // 当前缓冲区的内容 + 从服务器读取的内容
            for (int i = 0; i < count; i++)
            {
                this.content.Add(buffer[i]);
            }
        }


        /// 开始从连接的 Socket 异步接收数据
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


        /// 结束挂起的异步读取 
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

                // 此处暂停 0 毫秒以便数据缓冲
                Thread.Sleep(0);

                // 获取已经从网络接收且可供读取的数据量
                if (this.socket.Available > 0)
                {
                    lock (this.socket)
                    {
                        // 递归继续接收数据直至接收完毕
                        Receive();
                    }

                    return;
                }

                // 全部接收完毕则触发 ReceiveComplete 事件
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

            // 为什么会 Receive 2 次同样的消息呢, 已确认服务器只发 1 次信息, 难道是客户端缓冲区来不及清空造成的?
            // 开始下一次接收
            Receive();
        }


        /// 触发数据接收完毕事件
        private void OnReceiveComplete()
        {
            if (ReceiveCompleteEvent == null) { return; }

            IProtocol protocol = SerializeHelper.Deserialize<IProtocol>(this.content.ToArray());            
            ProtocolEventArgs e = new ProtocolEventArgs();
            e.Protocol = protocol;            

            ReceiveCompleteEvent(this, e);
        }


        /// 将数据异步发送到连接的 Socket
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


        // 不能采用异步编程, 接收与发送发生冲突?
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

        #region IDisposable 成员

        /// 关闭 Socket 连接并释放所有关联的资源
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


        /// 执行与释放或重置非托管资源相关的应用程序定义的任务
        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
