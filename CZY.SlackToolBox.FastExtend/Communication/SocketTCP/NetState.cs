using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// socket用户类
    /// </summary>
    public class NetState : NetBase
    {
        #region fields
        #endregion

        #region Properties

        private bool IsSocketConnected()
        {
            bool blockingState = socket.Blocking;
            try
            {
                byte[] tmp = new byte[1];
                socket.Blocking = false;
                socket.Send(tmp, 0, 0);
                return true;
            }
            catch (SocketException e)
            {
                if (e.NativeErrorCode.Equals(10035))
                {
                    return false;
                }
                return true;
            }
            finally
            {
                socket.Blocking = blockingState;
            }
        }

        #endregion

        #region Events

        #endregion
        #region Constructor
        /// <summary>
        /// 使用客户端连接后的套接字初始化新的AsyncSocketState实例
        /// </summary>
        /// <param name="cliSock"></param>
        /// <param name="useBom"></param>
        /// <param name="useClosingBom"></param>
        public NetState(Socket cliSock, bool useBom = true, bool useClosingBom = true, bool syncSend = true) : base()
        {
            this.socket = cliSock;
            this.useBom = useBom;
            this.useClosingBom = useClosingBom;
            this.IsSyncSendMode = syncSend;
            if (byteBuffer == null)
            {
                byteBuffer = new byte[receiveBufferSize];
            }
        }
        /// <summary>
        /// 初始化Socket客户端
        /// </summary>
        /// <param name="sendBufferSize">发送缓冲区大小</param>
        /// <param name="receiveBufferSize">接收缓冲区大小</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public NetState(Socket cliSock, int sendBufferSize, int receiveBufferSize) : base()
        {
            if (sendBufferSize <= 0 || receiveBufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sendBufferSize) + nameof(receiveBufferSize));
            }
            this.socket = cliSock;
            this.sendBufferSize = sendBufferSize;
            this.receiveBufferSize = receiveBufferSize;
            if (byteBuffer == null)
            {
                byteBuffer = new byte[receiveBufferSize];
            }
        }
        /// <summary>
        /// 初始化Socket客户端
        /// </summary>
        /// <param name="sendBufferSize"></param>
        /// <param name="receiveBufferSize"></param>
        /// <param name="useBom">是否在发送和接收时解析报文头</param>
        /// <param name="useClosingBom">是否使用主动断开报文头</param>
        /// <param name="syncSend">是否使用同步方式发送</param>
        public NetState(Socket cliSock, int sendBufferSize, int receiveBufferSize, bool useBom, bool useClosingBom, bool syncSend) : base()
        {
            if (sendBufferSize <= 0 || receiveBufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sendBufferSize) + nameof(receiveBufferSize));
            }
            this.socket = cliSock;
            this.sendBufferSize = sendBufferSize;
            this.receiveBufferSize = receiveBufferSize;
            this.useBom = useBom;
            this.useClosingBom = useClosingBom;
            this.IsSyncSendMode = syncSend;
            if (byteBuffer == null)
            {
                byteBuffer = new byte[receiveBufferSize];
            }

        }

        #endregion

        public void Init()
        {
            socket.ReceiveBufferSize = receiveBufferSize;
            socket.SendBufferSize = sendBufferSize;
            SetKeepAlive();
            OnChangeState(SocketState.Connected);
            OnConnected(this.socket);
            Receive();
        }
    }
}
