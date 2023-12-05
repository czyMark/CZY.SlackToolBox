using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CZY.SlackToolBox.FastExtend.Enums;

namespace CZY.SlackToolBox.FastExtend
{
    public class SocketUserState
    {
        public Socket Socket { get; private set; }

        public object State { get; private set; }
        public SocketUserState(Socket socket, object state, long length = -1, Action<NetSockSendCompletedResult> sendCompleted = null)
        {
            this.Socket = socket;
            this.State = state;
            this.Length = length;
            this.SendCompleted = sendCompleted;
        }

        public long Length { get; private set; }

        public Action<NetSockSendCompletedResult> SendCompleted { get; private set; }
    }
}
