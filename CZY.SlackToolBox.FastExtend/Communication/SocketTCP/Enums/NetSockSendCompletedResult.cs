using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend.Enums
{
    public enum NetSockSendCompletedResult
    {
        Succeeded = 0,
        SocketMismatched = 1,
        NoneBytes = 2,
        SocketError = 3,
        OtherError = 4,
    }
}
