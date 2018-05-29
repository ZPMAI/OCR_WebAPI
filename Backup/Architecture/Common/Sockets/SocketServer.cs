using System;
using System.Collections.Generic;
using System.Net; 
using System.Net.Sockets;

namespace CCT.Common.Sockets
{
	/// <summary>
	/// SocketServer ��ժҪ˵����
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

		/// ���캯��
		public SocketServer(string ipString, int port)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//            
            IPAddress ipAddress = IPAddress.Parse(ipString);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

			this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
			this.server.Bind(ipEndPoint);
            this.server.Listen(BACKLOG);
		}


		/// Ϊ�½����Ӵ����µ� Socket
        public SocketClient AcceptCllient()
		{
            Socket socket = this.server.Accept();

            string ipAddress = (socket.RemoteEndPoint as IPEndPoint).Address.ToString();

            // ����һ�� IP ֻ����һ��ͨѶͨ��
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

		/// �ر� Socket ���Ӳ��ͷ����й�������Դ
		public void Close()
		{	
			this.server.Close();
		}


		#region IDisposable ��Ա

		/// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
		public void Dispose()
		{
			Close();
			GC.SuppressFinalize( this );
		}

		#endregion
	}
}
