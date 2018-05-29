using System;
using System.Collections.Generic;
using System.Text;

namespace CCT.Common.Sockets
{
    public delegate void ConnectDelegate(SocketClient sender, EventArgs e);

    public delegate void ReceiveCompleteDelegate(SocketClient sender, ProtocolEventArgs e);

    public delegate void DisconnectDelegate(SocketClient sender, EventArgs e);

    //public delegate void SendCompleteDelegate(object sender, EventArgs e);
}
