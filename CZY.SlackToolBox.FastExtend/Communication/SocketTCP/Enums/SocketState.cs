using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// 定义用于表示Socket状态的常数
    /// </summary>
    public enum SocketState
    {
        /// <summary>
        /// socket已经关闭
        /// </summary>
        Closed,
        /// <summary>
        /// socket正在关闭
        /// </summary>
        Closing,
        /// <summary>
        /// socket处于连接成功状态
        /// </summary>
        Connected,
        /// <summary>
        /// socket处于尝试连接的状态
        /// </summary>
        Connecting,
        /// <summary>
        /// socket处于监听的状态
        /// </summary>
        Listening,
    }
}
