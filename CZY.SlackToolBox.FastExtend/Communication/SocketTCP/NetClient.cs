using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CZY.SlackToolBox.FastExtend
{
    public class NetClient : NetBase
    {
        #region Constructor
        /// <summary>
        /// 使用默认参数初始化Socket客户端
        /// </summary>
        public NetClient() : base()
        {
            if (byteBuffer == null)
            {
                byteBuffer = new byte[receiveBufferSize];
            }
        }

        /// <summary>
        /// 初始化Socket客户端
        /// </summary>
        /// <param name="useBom">是否在发送和接收时解析报文头</param>
        /// <param name="useClosingBom">是否使用主动断开报文头</param>
        /// <param name="syncSend">是否使用同步方式发送</param>
        public NetClient(bool useBom = true, bool useClosingBom = true, bool syncSend = true) : base()
        {
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
        public NetClient(int sendBufferSize, int receiveBufferSize) : base()
        {
            if (sendBufferSize <= 0 ||  receiveBufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sendBufferSize) + nameof(receiveBufferSize));
            }
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
        public NetClient(int sendBufferSize, int receiveBufferSize, bool useBom, bool useClosingBom, bool syncSend) : base()
        {
            if (sendBufferSize <= 0 || receiveBufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sendBufferSize) + nameof(receiveBufferSize));
            }
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

        #region Connect
        /// <summary>Connect to the computer specified by Host and Port</summary>
        public void Connect(IPEndPoint endPoint)
        {
            if (this.state == SocketState.Connected)
                return; // already connecting to something

            try
            {
                if (this.state != SocketState.Closed)
                    throw new Exception("Cannot connect Socket is " + this.state.ToString());

                OnChangeState(SocketState.Connecting);

                if (this.socket == null)
                {
                    this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        ReceiveBufferSize = receiveBufferSize,
                        SendBufferSize = sendBufferSize
                    };
                }

                this.socket.BeginConnect(endPoint, new AsyncCallback(this.ConnectCallback), this.socket);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Connect", ex);
                CloseOnly("Connect Exception");
            }
        }

        /// <summary>Callback for BeginConnect</summary>
        /// <param name="ar"></param>
        private void ConnectCallback(IAsyncResult ar)
        {
            if (ar.AsyncState != null)
            {
                try
                {
                    Socket sock = (Socket)ar.AsyncState;
                    sock.EndConnect(ar);

                    if (this.socket != sock)
                    {
                        CloseOnly("Async Connect Socket mismatched");
                        return;
                    }

                    if (this.state != SocketState.Connecting)
                        throw new Exception("Cannot connect Socket is " + this.state.ToString());

                    this.socket.ReceiveBufferSize = this.byteBuffer.Length;
                    this.socket.SendBufferSize = this.byteBuffer.Length;

                    SetKeepAlive();

                    OnChangeState(SocketState.Connected);
                    OnConnected(this.socket);

                    Receive();
                }
                catch (Exception ex)
                {
                    CloseOnly("Socket Connect Exception");
                    OnErrorReceived("Socket Connect", ex);
                }
            }
        }

        /// <summary>
        /// Disconnect the socket, send closing boms if exists.
        /// </summary>
        /// <param name="reason"></param>
        //public override void Close(string reason)
        //{
        //    base.Close(reason);
        //}
        #endregion

    }
}
