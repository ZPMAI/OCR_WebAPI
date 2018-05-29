using System;

namespace CCT.Common.Sockets
{
	/// <summary>
	/// ProtocolEventArgs 的摘要说明。
	/// </summary>
	public class ProtocolEventArgs : EventArgs
	{
        private IProtocol protocol;

        public IProtocol Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }


		/// 构造函数
		public ProtocolEventArgs()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
