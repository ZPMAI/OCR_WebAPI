using System;

namespace CCT.Common.Sockets
{
	/// <summary>
	/// ProtocolEventArgs ��ժҪ˵����
	/// </summary>
	public class ProtocolEventArgs : EventArgs
	{
        private IProtocol protocol;

        public IProtocol Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }


		/// ���캯��
		public ProtocolEventArgs()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
