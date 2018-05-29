using System;
using System.Collections.Generic;
using System.Net; 
using System.Net.Sockets;

namespace CCT.Common.Sockets
{
	/// <summary>
	/// SocketServer 的摘要说明。
	/// </summary>
	public class SocketServer : IDisposable
	{
		private const int BACKLOG = 50;

        public event ConnectDelegate ConnectEvent;

		private Socket server;
        private Dictionary<string, SocketClient> clients = new Dictionary<string, SocketClient>();

        public Dictionary<string, SocketClient> Clients
        {
            get { return this.clients; }
        }

		/// 构造函数
		public SocketServer(string ipString, int port)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//            
            IPAddress ipAddress = IPAddress.Parse(ipString);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

			this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
			this.server.Bind(ipEndPoint);
            this.server.Listen(BACKLOG);
		}


		/// 为新建连接创建新的 Socket
        public SocketClient AcceptCllient()
		{
            Socket socket = this.server.Accept();

            string ipAddress = (socket.RemoteEndPoint as IPEndPoint).Address.ToString();

            // 限制一个 IP 只能有一个通讯通道
            if (clients.ContainsKey(ipAddress))
            {
                socket.Shutdown(SocketShutdown.Both);
                return null;
            }

            SocketClient socketClient = new SocketClient(socket);
            socketClient.ClientIP = ipAddress;

            lock (this.clients)
            {
                clients.Add(ipAddress, socketClient);
            }

            if (this.ConnectEvent != null)
            {
                this.ConnectEvent(socketClient, EventArgs.Empty);
            }

            return socketClient;
		}


        public void RemoveClient(string ipAddress)
        {
            if (this.clients.ContainsKey(ipAddress))
            {
                lock (this.clients)
                {
                    this.clients.Remove(ipAddress);
                }
            }
        }

		/// 关闭 Socket 连接并释放所有关联的资源
		public void Close()
		{	
			this.server.Close();
		}


		#region IDisposable 成员

		/// 执行与释放或重置非托管资源相关的应用程序定义的任务
		public void Dispose()
		{
			Close();
			GC.SuppressFinalize( this );
		}

		#endregion
	}
}
