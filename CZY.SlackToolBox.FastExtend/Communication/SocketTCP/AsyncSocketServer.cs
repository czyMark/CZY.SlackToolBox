using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using CZY.SlackToolBox.FastExtend.Enums;

namespace CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// 1VN tcp server by socket
    /// </summary>
    public class AsyncSocketServer : IDisposable
    {
        #region fields
        private int _maxClient;

        private int _clientCount;
        /// <summary>
        /// 服务端监听socket对象
        /// </summary>
        private Socket _listenerSocket;
        /// <summary>
        /// 正在通信的socket对象
        /// </summary>
        private List<NetState> _clients;

        private bool disposed = false;

        protected ArraySegment<byte> bomBytes = new ArraySegment<byte>(new byte[] { 1, 2, 1, 255 });

        protected ArraySegment<byte> closeBytes = new ArraySegment<byte>(new byte[] { 3, 1, 5, 4 });

        #endregion

        #region Properties
        /// <summary>
        /// 获取一个值，指示server是否正在监听端口
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 获取或设置一个值，指示server连接的远程主机终结点IP地址
        /// </summary>
        public IPAddress Address { get; set; }

        /// <summary>
        /// 获取或设置一个值，指示server连接的远程主机终结点端口号
        /// </summary>
        public int Port { get; set; }

        public bool UseBom { get; set; } = true;

        public bool UseCloseBom { get; set; } = true;

        //public Encoding Encoding { get; set; }

        /// <summary>
        /// 获取一个值，指示同server建立连接的客户端对象
        /// </summary>
        public List<NetState> Clients { get => _clients; }

        #endregion

        #region Constructors
        /// <summary>
        /// 使用监听端口初始化实例
        /// </summary>
        /// <param name="listenPort">socket绑定的端口</param>
        public AsyncSocketServer(int listenPort) : this(IPAddress.Any, listenPort, 1024)
        {
        }
        /// <summary>
        /// 使用监听终结点初始化实例
        /// </summary>
        /// <param name="localEP">socket绑定的IP终结点</param>
        public AsyncSocketServer(IPEndPoint localEP) : this(localEP.Address, localEP.Port, 1024)
        {

        }
        /// <summary>
        /// 使用IP地址、监听端口、最大连接数初始化实例
        /// </summary>
        /// <param name="localIPAddress">监听IP地址</param>
        /// <param name="listPort">监听端口</param>
        /// <param name="maxClient">客户端最大连接数</param>
        public AsyncSocketServer(IPAddress localIPAddress, int listPort, int maxClient)
        {
            this.Address = localIPAddress;
            this.Port = listPort;
            //this.Encoding = Encoding.Default;

            _maxClient = maxClient;
            _clients = new List<NetState>();
            _listenerSocket = new Socket(localIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        #endregion

        #region Method

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void Start()
        {
            Start(1024);
        }

        /// <summary>
        /// 启动服务器, 开始监听端口
        /// </summary>
        /// <param name="backlog">
        /// 服务器所允许的挂起连接序列的最大长度
        /// </param>
        public void Start(int backlog)
        {
            try
            {
                if (!IsRunning)
                {
                    if (_listenerSocket == null)
                    {
                        _listenerSocket = new Socket(this.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    }
                    IsRunning = true;
                    _listenerSocket.Bind(new IPEndPoint(this.Address, this.Port));
                    _listenerSocket.Listen(backlog);
                    _listenerSocket.BeginAccept(new AsyncCallback(HandleAcceptConnected), _listenerSocket);
                }
            }
            catch (Exception e)
            {
                RaiseOtherException(e);
            }
        }

        public void StopListening()
        {
            if (IsRunning)
            {
                IsRunning = false;
                _listenerSocket.Close();
                _listenerSocket.Dispose();
                _listenerSocket = null;
            }
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                CloseAllClient();
                _listenerSocket.Close();
                _listenerSocket.Dispose();
                _listenerSocket = null;
            }
        }

        /// <summary>
        /// 处理客户端连接
        /// </summary>
        /// <param name="ar"></param>
        private void HandleAcceptConnected(IAsyncResult ar)
        {
            if (IsRunning && ar.AsyncState != null)
            {
                Socket server = (Socket)ar.AsyncState;

                try
                {
                    Socket client = server.EndAccept(ar);

                    //检查是否达到最大的允许的客户端数目
                    if (_clientCount >= _maxClient)
                    {
                        //C-TODO 触发事件
                        RaiseError("客户端连接超过最大值:" + _maxClient, null);
                    }
                    else
                    {
                        RaiseConnectedRequested(client);
                    }
                }
                catch (ObjectDisposedException)
                {
                    return;
                }
                catch (SocketException ex)
                {
                    //Stop();
                    RaiseError("Listen Socket", ex);
                }
                catch (Exception ex)
                {
                    RaiseError("Listen Socket", ex);
                }
                finally
                {
                    if (IsRunning && server != null && !disposed)
                        server.BeginAccept(new AsyncCallback(HandleAcceptConnected), server);
                }
            }
        } 

        /// <summary>
        /// 将数据发送至指定ip的客户端
        /// </summary>
        /// <param name="clientIp"></param>
        /// <param name="data"></param>
        /// <exception cref="SocketException"></exception>
        /// <exception cref="Exception"></exception>
        public void Send(string clientIp, byte[] data)
        {
            foreach (var client in _clients)
            {
                if (client.RemoteIP.ToString() == clientIp)
                {
                    Send(client, data);
                    break;
                }
            }
        }

        /// <summary>
        /// 通过IP终结点指定发送的客户端
        /// </summary>
        /// <param name="ep"></param>
        /// <param name="data"></param>
        public void Send(IPEndPoint ep, byte[] data)
        {
            foreach (var client in _clients)
            {
                if (client.RemoteEP.Equals(ep))
                {
                    Send(client, data);
                    break;
                }
            }
        }

        /// <summary>
        /// 异步发送数据至指定的客户端
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="data">报文</param>
        private void Send(NetState client, byte[] data)
        {
            try
            {
                if (!IsRunning)
                    throw new InvalidProgramException("This TCP Socket server has not been started.");

                if (client == null)
                    throw new ArgumentNullException("client");

                if (data == null)
                    throw new ArgumentNullException("data");
                client.Send(data);
            }
            //catch (SocketException e)
            //{
            //    RaiseError("send to client", e);
            //}
            catch (Exception e)
            {
                RaiseError("Send to client", e);
            }
        }

        #endregion

        #region events
        /// <summary>
        /// 客户端socket状态改变事件. sender - clientSocketState, e - statusChanged.
        /// </summary>
        public event EventHandler<AsyncSockStateChangedEventArgs> ClientStateChanged;

        /// <summary>
        /// 远程客户端连接请求事件，在该事件中调用Accept方法以接收连接请求，否则拒绝。
        /// </summary>
        public event EventHandler<AsyncSockConnectionRequestEventArgs> ConnectionRequest;

        /// <summary>
        /// 与客户端的连接已建立事件
        /// </summary>
        public event EventHandler<AsyncSockConnectedEventArgs> ClientConnected;
        /// <summary>
        /// 与客户端的连接已断开事件
        /// </summary>
        public event EventHandler<AsyncSockDisconnectedEventArgs> ClientDisconnected;
        /// <summary>
        /// 接收到数据事件
        /// </summary>
        public event EventHandler<AsyncSockDataArrivedEventArgs> DataArrived;
        /// <summary>
        /// 异常事件
        /// </summary>
        public event EventHandler<AsyncSockErrorReceivedEventArgs> ErrorReceived;

        /// <summary>
        /// 收到远程socket关闭指令
        /// </summary>
        public event EventHandler<AsyncSockRemoteClosingEventArgs> RemoteClosing;
        /// <summary>
        /// 发送结束后
        /// </summary>
        public event EventHandler<AsyncSockSendCompletedEventArgs> SendCompleted;
        /// <summary>
        /// 日志记录
        /// </summary>
        public event EventHandler<NetSockTracedInfoEventArgs> Traced;


        private void RaiseClientStateChanged(object sender, NetSockStateChangedEventArgs e)
        {
            ClientStateChanged?.Invoke(this, new AsyncSockStateChangedEventArgs(e.NewState, e.PrevState, sender as NetState));
        }
        /// <summary>
        /// 触发客户端连接事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseClientConnected(NetState state)
        {
            if (ClientConnected != null)
            {
                ClientConnected(this, new AsyncSockConnectedEventArgs(state.RemoteIP, state));
            }
        }
        /// <summary>
        /// 触发客户端连接断开事件
        /// </summary>
        /// <param name="client"></param>
        private void RaiseClientDisconnected(string reason, NetState state)
        {
            try
            {
                if (_clients.Remove(state))
                    _clientCount--;
                if (ClientDisconnected != null)
                {
                    ClientDisconnected(this, new AsyncSockDisconnectedEventArgs(reason, state));
                }
            }
            catch (Exception ex)
            {
                RaiseOtherException(ex);
            }
        }


        private void RaiseDataArrived(byte[] data, NetState state)
        {
            DataArrived?.Invoke(this, new AsyncSockDataArrivedEventArgs(data, state));
        }



        private void RaiseError(string msg, Exception ex, NetState state = null)
        {
            ErrorReceived?.Invoke(this, new AsyncSockErrorReceivedEventArgs(msg, state, ex));
        }

        private void RaiseOtherException(Exception ex)
        {
            RaiseError("", ex, null);
        }

        private void RaiseRemoteSocketClosing(NetState client, NetSockClosingStatus closingStatus)
        {
            RemoteClosing?.Invoke(this, new AsyncSockRemoteClosingEventArgs(client, closingStatus));
        }

        private void RaiseSendCompleted(NetState client, NetSockSendCompletedEventArgs e)
        {
            SendCompleted?.Invoke(this, new AsyncSockSendCompletedEventArgs(client, e.State, e.SendResult, e.Message));
        }
        private void RaiseConnectedRequested(Socket socket)
        {
            ConnectionRequest?.Invoke(this, new AsyncSockConnectionRequestEventArgs(socket));
        }
        #endregion

        #region Accept

        public void Accept(Socket client, bool syncSend = false)
        {
            try
            {
                if (!this.IsRunning)
                    throw new Exception("Cannot accept, Socket is not running");


                NetState clientState = new NetState(client, syncSend : syncSend);
                InitClientState(clientState);
                // 1. binding events 2. initialize socket state and begin receive 3. add state to the clients list.
                Clients.Add(clientState);
            }
            catch (Exception ex)
            {
                RaiseError("Exceptions during accepting", ex);
            }
        }

        public void Accept(NetState clientState)
        {
            try
            {
                if (!this.IsRunning)
                    throw new Exception("Cannot accept, Socket is not running");

                InitClientState(clientState);
                // 1. binding events 2. initialize socket state and begin receive 3. add state to the clients list.
                Clients.Add(clientState);
            }
            catch (Exception ex)
            {
                RaiseError("Exceptions during accepting", ex);
            }
        }

        private void InitClientState(NetState client)
        {
            client.StateChanged += (s, e) => RaiseClientStateChanged(s, e);
            client.Connected += (s, e) => RaiseClientConnected(s as NetState);
            client.RemoteClosing += (s, e) => RaiseRemoteSocketClosing(s as NetState, e);
            client.Disconnected += (s, e) => RaiseClientDisconnected(e.Reason, s as NetState);
            client.DataArrived += (s, e) => RaiseDataArrived(e.Data, s as NetState);
            client.ErrorReceived += (s, e) => RaiseError(e.Function, e.Exception, s as NetState);
            client.SendCompleted += (s, e) => RaiseSendCompleted(s as NetState, e);
            client.Traced += (s, e) => Traced?.Invoke(s as NetState, e);
            client.Init();
        }
        #endregion

        #region Close
        /// <summary>
        /// 关闭一个与客户端之间的会话
        /// </summary>
        /// <param name="state">需要关闭的客户端会话对象</param>
        public void Close(NetState state)
        {
            if (state != null)
            {
                _clients.Remove(state);
                _clientCount--;
                state.CloseOnly("Stop");
            }
        }

        /// <summary>
        /// 关闭一个与客户端之间的对话
        /// </summary>
        /// <param name="ip"></param>
        public void Close(string ip)
        {
            if (_clients == null || _clients.Count == 0)
                return;
            for (int i = _clients.Count - 1; i >= 0; i--)
            {
                var client = _clients[i];
                if (client.RemoteIP.ToString() == ip)
                {
                    Close(client);
                }
            }
        }
        /// <summary>
        /// 关闭所有的客户端会话,与所有的客户端连接会断开
        /// </summary>
        public void CloseAllClient()
        {
            if (_clients == null || _clients.Count == 0)
                return;
            for (int i = _clients.Count - 1; i >= 0; i--)
            {
                Close(_clients[i]);
            }
            _clientCount = 0;
            _clients.Clear();
        }
        #endregion

        #region dispose
        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release 
        /// both managed and unmanaged resources; <c>false</c> 
        /// to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();
                        if (_listenerSocket != null)
                        {
                            _listenerSocket = null;
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO
                        RaiseOtherException(e);
                    }
                }
                disposed = true;
            }
        }
        #endregion
    }
}
