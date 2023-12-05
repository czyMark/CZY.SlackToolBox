using CZY.SlackToolBox.FastExtend.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend
{
    #region Event Args
    public class NetSocketConnectedEventArgs : EventArgs
    {
        public IPAddress SourceIP;
        public NetSocketConnectedEventArgs(IPAddress ip)
        {
            SourceIP = ip;
        }
    }

    public class NetSocketDisconnectedEventArgs : EventArgs
    {
        public string Reason;
        public NetSocketDisconnectedEventArgs(string reason)
        {
            Reason = reason;
        }
    }

    public class NetSockStateChangedEventArgs : EventArgs
    {
        public SocketState NewState;
        public SocketState PrevState;
        public NetSockStateChangedEventArgs(SocketState newState, SocketState prevState)
        {
            NewState = newState;
            PrevState = prevState;
        }
    }

    /// <summary>
    /// 接收数据事件所需参数
    /// </summary>
    public class NetSockDataArrivalEventArgs : EventArgs
    {
        /// <summary>
        /// 接收到的数据
        /// </summary>
        public byte[] Data;
        public NetSockDataArrivalEventArgs(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// 使用BinaryFormatter反序列化数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>序列化成功，返回指定对象的实例，失败，返回default</returns>
        public T Deserialize<T>()
        {
            if (Data == null || Data.Length < 1)
            {
                return default(T);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(Data);
            try
            {
                var res = formatter.Deserialize(ms);
                return (T)res;
            }
            catch (Exception)
            {
                return default(T);
            }
            finally
            {
                ms.Close();
            }
        }
    }
    public class NetSockSendCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// 用户在Send中传入的引用
        /// </summary>
        public object State;
        /// <summary>
        /// 发送结果<br/>
        /// 0 - 成功<br/>
        /// -1 - 失败<br/>
        /// </summary>
        public NetSockSendCompletedResult SendResult;

        public string Message;

        public int DateLength;

        public NetSockSendCompletedEventArgs(object state, NetSockSendCompletedResult sendResult, string message, int dateLength = 0)
        {
            State = state;
            SendResult = sendResult;
            Message = message;
            DateLength = dateLength;
        }
    }

    /// <summary>
    /// 记录内部跟踪信息
    /// </summary>
    public class NetSockTracedInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Trace名
        /// </summary>
        public string TraceName;
        /// <summary>
        /// Trace记录
        /// </summary>
        public string Message;

        public NetSockTracedInfoEventArgs(string traceName, string message)
        {
            TraceName = traceName;
            Message = message;
        }

        /// <summary>
        /// 返回[name, msg]
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(TraceName) && string.IsNullOrEmpty(Message))
                return string.Empty;
            return $"[TraceName:{TraceName}, Message:{Message}]";
        }
    }

    public class NetSockErrorReceivedEventArgs : EventArgs
    {
        public string Function;
        public Exception Exception;
        public NetSockErrorReceivedEventArgs(string function, Exception ex)
        {
            Function = function;
            Exception = ex;
        }
    }

    public class NetSockConnectionRequestEventArgs : EventArgs
    {
        public Socket Client;
        public NetSockConnectionRequestEventArgs(Socket client)
        {
            Client = client;
        }
    }
    #endregion

    #region Socket Classes
    public abstract class NetBase
    {
        #region Fields
        /// <summary>Current socket state</summary>
        protected SocketState state = SocketState.Closed;
        /// <summary>The socket object, obviously</summary>
        protected Socket socket;

        //protected Socket socket;

        ///// 定义一个集合，存储客户端信息
        //static Dictionary<string, Socket> clientConnectionItems = new Dictionary<string, Socket> { };

        /// <summary>Keep track of when data is being sent</summary>
        protected bool isSending = false;

        /// <summary>The boolean which indicates whether use bombytes in sending</summary>
        protected bool useBom = true;

        /// <summary>The boolean which indicates whether use bombytes to tell the remote socket to close connection</summary>
        protected bool useClosingBom = true;

        /// <summary>Queue of objects to be sent out</summary>
        protected Queue<SendState> sendBuffer = new Queue<SendState>();
        protected object sendLocker = new object();

        /// <summary>Store incoming bytes to be processed</summary>
        protected byte[] byteBuffer;
        // 发送缓冲区大小
        protected int sendBufferSize = 8192;
        // 接收缓冲区大小
        protected int receiveBufferSize = 8192;

        /// <summary>Position of the bom header in the rxBuffer</summary>
        protected int rxHeaderIndex = -1;
        /// <summary>Expected length of the message from the bom header</summary>
        protected int rxBodyLen = -1;
        protected object rxLocker = new object();
        /// <summary>Buffer of received data</summary>
        protected MemoryStream rxBuffer = new MemoryStream();

        /// <summary>Beginning of message indicator</summary>
        protected ArraySegment<byte> bomBytes = new ArraySegment<byte>(new byte[] { 1, 2, 1, 255 });

        /// <summary> </summary>
        protected ArraySegment<byte> closeBytes = new ArraySegment<byte>(new byte[] { 3, 1, 5, 4 });

        /// <summary>TCP inactivity before sending keep-alive packet (ms)</summary>
        protected uint KeepAliveInactivity = 500;
        /// <summary>Interval to send keep-alive packet if acknowledgement was not received (ms)</summary>
        protected uint KeepAliveInterval = 100;

        /// <summary>Threaded timer checks if socket is busted</summary>
        protected Timer connectionTimer;
        /// <summary>Interval for socket checks (ms)</summary>
        protected int ConnectionCheckInterval = 1000;
        #endregion

        #region Public Properties
        /// <summary>Current state of the socket</summary>
        public SocketState State { get { return state; } }

        /// <summary>Port the socket control is listening on.</summary>
        public int LocalPort
        {
            get
            {
                try
                {
                    if (socket == null) return -1;
                    return ((IPEndPoint)socket.LocalEndPoint).Port;
                }
                catch
                {
                    return -1;
                }
            }
        }



        public IPEndPoint RemoteEP
        {
            get
            {
                try
                {
                    if (socket != null)
                        remoteEP = (IPEndPoint)socket.RemoteEndPoint;
                    return remoteEP;
                }
                catch
                {
                    return remoteEP;
                }
            }
        }


        private IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        public IPAddress RemoteIP
        {
            get => RemoteEP.Address;
        }

        public int RemotePort
        {
            get => RemoteEP.Port;
        }

        /// <summary>
        /// 是否使用同步发送方法
        /// </summary>
        public bool IsSyncSendMode { get; protected set; } = true;
        /// <summary>
        /// 发送失败时重试的次数
        /// </summary>
        public int SendRetryTimes { get; protected set; } = 3;

        /// <summary>IP address enumeration for local computer</summary>
        //public static string[] LocalIP
        //{
        //    get
        //    {
        //        try
        //        {
        //            IPHostEntry h = Dns.GetHostEntry(Dns.GetHostName());
        //            List<string> s = new List<string>(h.AddressList.Length);
        //            foreach (IPAddress i in h.AddressList)
        //                s.Add(i.ToString());
        //            return s.ToArray();
        //        }
        //        catch
        //        {
        //            return null;
        //        }

        //    }
        //}
        #endregion

        #region Events
        public event EventHandler<NetSockTracedInfoEventArgs> Traced;
        /// <summary>Socket is connected</summary>
        public event EventHandler<NetSocketConnectedEventArgs> Connected;
        /// <summary>Socket connection closed</summary>
        public event EventHandler<NetSocketDisconnectedEventArgs> Disconnected;
        /// <summary>Socket state has changed</summary>
        /// <remarks>This has the ability to fire very rapidly during connection / disconnection.</remarks>
        public event EventHandler<NetSockStateChangedEventArgs> StateChanged;
        /// <summary>Recived a new object</summary>
        public event EventHandler<NetSockDataArrivalEventArgs> DataArrived;
        /// <summary>An error has occurred</summary>
        public event EventHandler<NetSockErrorReceivedEventArgs> ErrorReceived;
        /// <summary>Data send completed<br/>
        /// 当数据发送完成后</summary>
        public event EventHandler<NetSockSendCompletedEventArgs> SendCompleted;
        /// <summary>Remote socket is closing</summary>
        public event EventHandler<NetSockClosingStatus> RemoteClosing;
        #endregion

        #region Constructor
        /// <summary>Base constructor sets up buffer and timer</summary>
        public NetBase()
        {
            ////定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connectionTimer = new Timer(
                new TimerCallback(ConnectedTimerCallback),
                null, Timeout.Infinite, Timeout.Infinite);
        }
        #endregion

        #region Send
        /// <summary>Send data</summary>
        /// <param name="data">Bytes to send</param>
        /// <param name="state">用户自定义数据，在发送结束后由SendCompleted事件返回</param>
        /// <param name="blocked">当前方法是否阻塞（仅限同步模式）</param>
        public void Send(byte[] data, object state = null, bool blocked = false)
        {
            try
            {
                SendState sendState = new SendState(data, state);
                SendInternal(data, sendState);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Send", ex);
            }
        }

        private void SendInternal(byte[] data, SendState sendState)
        {
            if (data == null)
                throw new NullReferenceException("data cannot be null");
            else if (data.Length == 0)
                throw new NullReferenceException("data cannot be empty");
            else
            {
                if (sendState == null)
                    sendState = new SendState(data, null);
                lock (sendLocker)
                {
                    sendBuffer.Enqueue(sendState);
                }

                if (!isSending)
                {
                    isSending = true;
                    if (IsSyncSendMode)
                    {
                        Task.Run(SendSynchronous);
                    }
                    else
                        SendNextQueued();
                }
            }
        }

        private void SendSynchronous()
        {
            while (isSending)
            {
                SendNextQueued();
            }
        }

        /// <summary>Send data for real</summary>
        private void SendNextQueued()
        {
            try
            {
                List<ArraySegment<byte>> send;
                byte[] data;
                int length = 0;
                object state = null;
                SocketUserState userState;
                lock (sendLocker)
                {
                    if (sendBuffer.Count == 0)
                    {
                        isSending = false;
                        return; // nothing more to send
                    }
                    var sendState = sendBuffer.Dequeue();
                    data = sendState.data;
                    state = sendState.state;
                    if (useBom)
                    {
                        send = new List<ArraySegment<byte>>(3);
                        send.Add(bomBytes);
                        send.Add(new ArraySegment<byte>(BitConverter.GetBytes(data.Length)));
                        send.Add(new ArraySegment<byte>(data));

                        length = bomBytes.Count + sizeof(int) + data.Length;
                    }
                    else
                    {
                        send = new List<ArraySegment<byte>>(1);
                        send.Add(new ArraySegment<byte>(data));
                        length = data.Length;
                    }
                    userState = new SocketUserState(socket, state, length, sendState.sendCompleted);
                }

                if (socket != null)
                {
                    if (IsSyncSendMode)
                    {
                        try
                        {
                            OnTracedInfo("Send", $"before Send, userState:{userState.State}");
                            var didSend = socket.Send(send, SocketFlags.None, out SocketError error);
                            OnTracedInfo("Send", $"after Send, sendedLength:{didSend}, dataLength:{userState.Length} userState:{userState.State}");
                            if (didSend < length)
                            {
                                OnErrorReceived("SendCallback", new Exception($"实际发送数据不匹配, data:[{length}], sended:[{didSend}]"));
                            }
                            if (error != SocketError.Success)
                                OnErrorReceived($"Send unsuccess: ErrorCode is {error}, userState is {userState.State}", null);
                            if (didSend < 1)
                            {
                                for (int i = 0; i < SendRetryTimes; i++)
                                {
                                    OnTracedInfo("Send", $"before Send, 第{i}次重试发送, userState:{userState.State}");
                                    didSend = socket.Send(send, SocketFlags.None, out error);
                                    OnTracedInfo("Send", $"after Send, 第{i}次重试发送 ,sendedLength:{didSend}, dataLength:{userState.Length} userState:{userState.State}");
                                    if (didSend > 0)
                                    {
                                        OnSendCompleted(userState, NetSockSendCompletedResult.Succeeded, $"Send succeeded with {i + 1} retries");
                                        return;
                                    }
                                }
                                OnSendCompleted(userState, NetSockSendCompletedResult.NoneBytes, "No Bytes Sended");
                                CloseOnly("No Bytes Sended");
                                return;
                            }

                            OnSendCompleted(userState, NetSockSendCompletedResult.Succeeded, "Send succeeded");
                        }
                        catch (SocketException ex)
                        {
                            if (ex.SocketErrorCode == SocketError.ConnectionReset)
                            {
                                OnSendCompleted(userState, NetSockSendCompletedResult.SocketError, "Remote Socket Closed");
                                CloseOnly("Remote Socket Closed");
                            }
                            else if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                            {
                                OnSendCompleted(userState, NetSockSendCompletedResult.SocketError, "Remote Socket Refused");
                                CloseOnly("Remote Socket Refused");
                            }
                            else
                            {
                                OnSendCompleted(userState, NetSockSendCompletedResult.SocketError, ex.ToString());
                                CloseOnly(ex.SocketErrorCode.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            OnSendCompleted(userState, NetSockSendCompletedResult.OtherError, "Socket Send Exception");
                            CloseOnly("Socket Send Exception");
                            OnErrorReceived("Socket Send", ex);
                        }
                    }
                    else
                    {
                        OnTracedInfo("Send", $"before Beginsend, userState:{userState.State}");
                        socket.BeginSend(send, SocketFlags.None, out SocketError error, new AsyncCallback(SendCallback), userState);
                        if (error != SocketError.Success)
                        {
                            OnErrorReceived($"BeginSend unsuccess: ErrorCode is {error}, userState is {userState.State}", null);
                        }
                        OnTracedInfo("Send", $"after Beginsend, userState:{userState.State}");
                    }
                }
            }
            catch (SocketException ex)
            {
                OnErrorReceived("Sending", ex);
                CloseOnly("SocketException in BeginSend");
            }
            catch (Exception ex)
            {
                OnErrorReceived("Sending", ex);
            }
        }

        /// <summary>Callback for BeginSend</summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            OnTracedInfo("Send", $"Entry SendCallback, asyncState is null:{ar == null}");

            if (ar.AsyncState != null)
            {
                SocketUserState sockState = (SocketUserState)ar.AsyncState;
                try
                {
                    var sock = sockState.Socket;
                    OnTracedInfo("Send", $"before EndSend, userState:{sockState.State}");
                    int didSend = sock.EndSend(ar, out SocketError error);
                    OnTracedInfo("Send", $"after EndSend, sendedLength:{didSend}, dataLength:{sockState.Length} userState:{sockState.State}");
                    if (didSend < sockState.Length)
                    {
                        OnErrorReceived("SendCallback", new Exception($"实际发送数据不匹配, data:[{sockState.Length}], sended:[{didSend}]"));
                    }
                    if (error != SocketError.Success)
                        OnErrorReceived($"EndSend unsuccess: ErrorCode is {error}, userState is {sockState.State}", null);
                    if (socket != sock)
                    {
                        OnSendCompleted(sockState, NetSockSendCompletedResult.SocketMismatched, "Async Connect Socket mismatched");
                        CloseOnly("Async Connect Socket mismatched");
                        return;
                    }
                    if (didSend < 1)
                    {
                        OnSendCompleted(sockState, NetSockSendCompletedResult.NoneBytes, "No Bytes Sended");
                        CloseOnly("No Bytes Sended");
                        return;
                    }


                    OnSendCompleted(sockState, NetSockSendCompletedResult.Succeeded, "Send succeeded");
                    SendNextQueued();
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        OnSendCompleted(sockState, NetSockSendCompletedResult.SocketError, "Remote Socket Closed");
                        CloseOnly("Remote Socket Closed");
                    }
                    else if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        OnSendCompleted(sockState, NetSockSendCompletedResult.SocketError, "Remote Socket Refused");
                        CloseOnly("Remote Socket Refused");
                    }
                    else
                    {
                        OnSendCompleted(sockState, NetSockSendCompletedResult.SocketError, ex.ToString());
                        CloseOnly(ex.SocketErrorCode.ToString());
                    }
                }
                catch (Exception ex)
                {
                    OnSendCompleted(sockState, NetSockSendCompletedResult.OtherError, "Socket Send Exception");
                    CloseOnly("Socket Send Exception");
                    OnErrorReceived("Socket Send", ex);
                }
            }
        }
        #endregion

        #region Close
        /// <summary>
        /// Send data to tell the remote socket that local socket is closing.
        /// </summary>
        public void PossitiveOnClosing()
        {
            // disabled while useBom is false
            if (!useBom || !useClosingBom)
            {
                return;
            }

        }

        /// <summary>
        /// Disconnect the socket, donot send anything.
        /// </summary>
        public virtual void CloseOnly(string reason)
        {

            try
            {
                if (state == SocketState.Closing || state == SocketState.Closed)
                    return; // already closing/closed


                OnChangeState(SocketState.Closing);

                if (socket != null)
                {
                    if (socket.Connected)
                        socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    socket.Dispose();
                    socket = null;
                }
            }
            catch (Exception ex)
            {
                OnErrorReceived("CloseOnly", ex);
            }

            try
            {
                lock (rxLocker)
                {
                    if (rxBuffer.Length > 0)
                    {
                        if (rxHeaderIndex > -1 && rxBodyLen > -1)
                        {
                            // start of message - length of header
                            int msgbytes = (int)rxBuffer.Length - rxHeaderIndex - bomBytes.Count - sizeof(int);
                            OnErrorReceived("Close Buffer", new Exception("Incomplete Message (" + msgbytes.ToString() + " of " + rxBodyLen.ToString() + " bytes received)"));
                        }
                        else
                        {
                            OnErrorReceived("Close Buffer", new Exception("Unprocessed data " + rxBuffer.Length.ToString() + " bytes"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorReceived("Close Buffer", ex);
            }

            try
            {
                if (rxBuffer.Length > 0)
                    rxBuffer.SetLength(0);
                lock (sendLocker)
                {
                    sendBuffer.Clear();
                    isSending = false;
                }
                OnChangeState(SocketState.Closed);
                if (Disconnected != null)
                    Disconnected(this, new NetSocketDisconnectedEventArgs(reason));
            }
            catch (Exception ex)
            {
                OnErrorReceived("Close Cleanup", ex);
            }

        }

        /// <summary>
        /// 关闭socket连接，socket对象可重用
        /// <br/>close the socket, can reuse.
        /// </summary>
        /// <param name="reason">关闭原因<br/>the reason of closing</param>
        /// <param name="isActiveClosing">是否为主动关闭(通知对端)<br/>wheather is actived closing, which send closing data to remote socket.</param>
        /// <param name="closingStatus">关闭状态: 0-一般关闭; 1-操作成功关闭; 2-操作失败关闭;<br/>closing status: 0-common closed; 1-operation succeeded; 2-operation failed.</param>
        public virtual void Close(string reason, bool isActiveClosing = false, NetSockClosingStatus closingStatus = 0)
        {
            try
            {
                if (state == SocketState.Closing || state == SocketState.Closed)
                    return; // already closing/closed

                if (isActiveClosing)
                {
                    if (useBom && useClosingBom)
                    {
                        List<byte> send = new List<byte>();
                        send.AddRange(closeBytes.Array);
                        send.AddRange(BitConverter.GetBytes((int)closingStatus));

                        SendState userState = new SendState(send.ToArray(), null, sendCompleted: b =>
                        {
                            // 不知道应该要不要等待或者等待多久，先等300ms
                            System.Threading.Thread.Sleep(300);
                            CloseOnly(reason);
                        });
                        SendInternal(send.ToArray(), userState);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorReceived("Close", ex);
            }

            CloseOnly(reason);

        }
        #endregion

        #region Receive
        /// <summary>Receive data asynchronously</summary>
        internal protected void Receive()
        {
            if (socket != null)
            {
                try
                {
                    socket.BeginReceive(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, out SocketError error, new AsyncCallback(ReceiveCallback), socket);
                    if (error != SocketError.Success)
                    {
                        OnErrorReceived($"BeginReceived unsuccess: ErrorCode is {error}", null);
                    }
                }
                catch (Exception ex)
                {
                    OnErrorReceived("Receive", ex);
                }
            }
        }
        /// <summary>Callback for BeginReceive</summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socket sock = (Socket)ar.AsyncState;
                int size = sock.EndReceive(ar, out SocketError error);
                if (error != SocketError.Success)
                {
                    OnErrorReceived($"EndReceived unsuccess: ErrorCode is {error}", null);
                }
                if (socket != sock)
                {
                    CloseOnly("Async Receive Socket mismatched");
                    return;
                }

                if (size < 1)
                {
                    CloseOnly("No Bytes Received");
                    return;
                }
                OnTracedInfo("Receive", $"[{size}] bytes received.");
                if (useBom)
                {
                    lock (rxLocker)
                    {
                        // put at the end for safe writing
                        rxBuffer.Position = rxBuffer.Length;
                        rxBuffer.Write(byteBuffer, 0, size);

                        bool more = false;
                        do
                        {
                            // search for header if not found yet
                            more = false;
                            if (rxHeaderIndex < 0)
                            {
                                rxBuffer.Position = 0; // rewind to search
                                rxHeaderIndex = IndexOfBytesInStream(rxBuffer, bomBytes.Array, out bool cut);
                                if (cut && rxHeaderIndex > -1)
                                {
                                    OnTracedInfo("receiveLoop", $"当前header被切割, 接收流长度:[{rxBuffer.Length}], header位置:[{rxHeaderIndex}], 接收下一个缓冲数据");
                                    rxBuffer.Position = rxHeaderIndex;
                                    CopyBack();
                                    rxHeaderIndex = -1;
                                }
                                else if (rxHeaderIndex < 0)
                                {
                                    rxBuffer.SetLength(0);
                                    rxBuffer.Capacity = byteBuffer.Length;
                                    OnErrorReceived("receiveLoop", new Exception($"在搜索header时失败，清空缓冲内存流"));
                                }
                            }

                            // have the header
                            if (rxHeaderIndex > -1)
                            {
                                // read the body length from header
                                if (rxBodyLen < 0 && rxBuffer.Length - rxHeaderIndex - bomBytes.Count >= 4)
                                {
                                    rxBuffer.Position = rxHeaderIndex + bomBytes.Count; // start reading after bomBytes
                                    rxBuffer.Read(byteBuffer, 0, 4); // read message length
                                    rxBodyLen = BitConverter.ToInt32(byteBuffer, 0);

                                    OnTracedInfo("Receive", $"收到新数据包头, 长度:{rxBodyLen}");
                                }

                                // 收完数据包
                                if (rxBodyLen > -1 && (rxBuffer.Length - rxHeaderIndex - bomBytes.Count - 4) >= rxBodyLen)
                                {
                                    // 收完当前数据包，从流中读取，给用户传递数据包事件
                                    try
                                    {
                                        if (State == SocketState.Closing || State == SocketState.Closed)
                                            return;
                                        rxBuffer.Position = rxHeaderIndex + bomBytes.Count + sizeof(int);
                                        byte[] data = new byte[rxBodyLen];
                                        rxBuffer.Read(data, 0, data.Length);
                                        try
                                        {
                                            OnDataArrived(this, new NetSockDataArrivalEventArgs(data));
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        OnErrorReceived("OnDataArrived", ex);
                                    }

                                    if (rxBuffer.Position == rxBuffer.Length)
                                    {
                                        // no bytes left
                                        // just resize buffer
                                        rxBuffer.SetLength(0);
                                        rxBuffer.Capacity = byteBuffer.Length;
                                        more = false;
                                    }
                                    else
                                    {
                                        // leftover bytes after current message
                                        // copy these bytes to the beginning of the rxBuffer
                                        CopyBack();
                                        more = true;
                                    }

                                    // reset header info
                                    rxHeaderIndex = -1;
                                    rxBodyLen = -1;
                                }
                                else if (rxHeaderIndex > 0)
                                {
                                    // remove bytes from before the header
                                    rxBuffer.Position = rxHeaderIndex;
                                    CopyBack();
                                    rxHeaderIndex = 0;
                                    more = false;
                                }
                                else
                                    more = false;
                            }
                        } while (more);
                    }
                }
                else
                {
                    lock (rxLocker)
                    {
                        rxBuffer.Position = rxBuffer.Length;
                        rxBuffer.Write(byteBuffer, 0, byteBuffer.Length);

                        try
                        {
                            if (State == SocketState.Closing || State == SocketState.Closed)
                                return;
                            var data = rxBuffer.ToArray();
                            OnDataArrived(this, new NetSockDataArrivalEventArgs(data));
                        }
                        catch (Exception ex)
                        {
                            OnErrorReceived("Receiving", ex);
                        }
                        rxBuffer.SetLength(0);
                        rxBuffer.Capacity = byteBuffer.Length;
                    }
                }
                if (socket != null)
                {
                    socket.BeginReceive(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                }
            }
            catch (ObjectDisposedException)
            {
                return; // socket disposed, let it die quietly
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    CloseOnly("Remote Socket Closed");
                OnErrorReceived("Socket Receive", ex);
            }
            catch (Exception ex)
            {
                CloseOnly("Socket Receive Exception");
                OnErrorReceived("Socket Receive", ex);
            }
        }

        /// <summary>
        /// Copies the stuff after the current position, back to the start of the stream,
        /// resizes the stream to only include the new content, and
        /// limits the capacity to length + another buffer.
        /// </summary>
        private void CopyBack()
        {
            try
            {
                int count;
                long readPos = rxBuffer.Position;
                long writePos = 0;
                var temp = new byte[byteBuffer.Length];
                do
                {
                    count = rxBuffer.Read(temp, 0, temp.Length);
                    readPos = rxBuffer.Position;
                    rxBuffer.Position = writePos;
                    rxBuffer.Write(temp, 0, count);
                    writePos = rxBuffer.Position;
                    rxBuffer.Position = readPos;
                }
                while (count > 0);
                rxBuffer.SetLength(writePos);
                rxBuffer.Capacity = (int)rxBuffer.Length + byteBuffer.Length;
            }
            catch (Exception ex)
            {
                OnErrorReceived("CopyBack", ex);
            }

        }

        /// <summary>Find first position the specified byte within the stream, or -1 if not found</summary>
        /// <param name="ms"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        private int IndexOfByteInStream(MemoryStream ms, byte find)
        {
            int b;
            try
            {
                do
                {
                    b = ms.ReadByte();
                } while (b > -1 && b != find);

                if (b == -1)
                    return -1;
                else
                    return (int)ms.Position - 1; // position is +1 byte after the byte we want
            }
            catch (Exception ex)
            {
                OnErrorReceived("IndexOfByteInStream", ex);
                return -1;
            }

        }

        /// <summary>在流中寻找与给定字节数组匹配的第一个位置。若没有匹配的，返回-1，若部分匹配，但是流已经到末尾，返回index, </summary>
        /// <param name="ms">内存流</param>
        /// <param name="find">需匹配的字节数组</param>
        /// <param name="isCutByStream">待查询数组是否被流切断</param>
        /// <returns>-1 没有找到对应的字节数组<br/></returns>
        private int IndexOfBytesInStream(MemoryStream ms, byte[] find, out bool isCutByStream)
        {
            int index;
            isCutByStream = false;
            try
            {
                do
                {
                    index = IndexOfByteInStream(ms, find[0]);

                    if (index > -1)
                    {
                        bool found = true;
                        for (int i = 1; i < find.Length; i++)
                        {
                            var temp = ms.ReadByte();
                            // 已经读到流的末尾，但是之前的解析正常，需要继续receive数据。
                            if (temp < 0)
                            {
                                isCutByStream = true;
                                return index;
                            }
                            // 不相等，认为该index不符合，向下搜索。
                            else if (find[i] != temp)
                            {
                                found = false;
                                ms.Position = index + 1;
                                break;
                            }
                        }
                        if (found)
                        {
                            return index;
                        }
                    }
                } while (index > -1);
                return -1;
            }
            catch (Exception ex)
            {
                OnErrorReceived("IndexOfBytesInStream", ex);
                return -1;
            }
        }

        private int IndexOfBytesInArray(byte[] data, byte[] find)
        {
            int index = -1;
            int position = 0;
            if (data.Length < find.Length)
                return -1;
            try
            {
                do
                {
                    for (int i = position; i < data.Length; i++)
                    {
                        if (data[i] == find[0])
                        {
                            position = i + 1;
                            index = i;
                            break;
                        }
                        index = -1;
                    }
                    if (index > -1)
                    {
                        if (data.Length < index + find.Length)
                        {
                            return -1;
                        }
                        bool found = true;
                        for (int i = 1; i < find.Length; i++)
                        {
                            if (find[i] != data[index + i])
                            {
                                found = false;
                                position += i;
                                break;
                            }
                        }
                        if (found)
                            return index;
                    }
                } while (index > -1);
                return -1;
            }
            catch (Exception ex)
            {
                OnErrorReceived("IndexOfBytesInArray", ex);
                return -1;
            }
        }

        #endregion

        #region OnEvents
        protected void OnSendCompleted(SocketUserState state, NetSockSendCompletedResult result, string message, int length = 0)
        {
            if (state.SendCompleted != null)
                state.SendCompleted(result);
            SendCompleted?.Invoke(this, new NetSockSendCompletedEventArgs(state.State, result, message));
        }

        protected void OnTracedInfo(string tracer, string message)
        {
            if (Traced != null)
            {
                var e = new NetSockTracedInfoEventArgs(tracer, message);
                Traced.Invoke(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="ex"></param>
        protected void OnErrorReceived(string function, Exception ex)
        {
            if (ErrorReceived != null)
                ErrorReceived(this, new NetSockErrorReceivedEventArgs(function, ex));
        }

        protected void OnConnected(Socket sock)
        {
            if (Connected != null)
                Connected(this, new NetSocketConnectedEventArgs(((IPEndPoint)sock.RemoteEndPoint).Address));
        }

        protected void OnChangeState(SocketState newState)
        {
            try
            {
                SocketState prev = state;
                state = newState;
                if (StateChanged != null)
                    StateChanged(this, new NetSockStateChangedEventArgs(state, prev));

                if (state == SocketState.Connected)
                    connectionTimer.Change(0, ConnectionCheckInterval);
                else if (state == SocketState.Closed)
                    connectionTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Change State", ex);
            }
        }

        protected void OnDataArrived(object sender, NetSockDataArrivalEventArgs e)
        {
            if (useBom && useClosingBom)
            {
                var idx = IndexOfBytesInArray(e.Data, closeBytes.Array);
                if (idx > -1)
                {
                    try
                    {
                        var closingData = e.Data.Skip(idx + closeBytes.Array.Length).Take(sizeof(int)).ToArray();
                        NetSockClosingStatus closingStatus = (NetSockClosingStatus)BitConverter.ToInt32(closingData, 0);
                        OnClosingRequested(closingStatus);
                    }
                    catch (Exception ex)
                    {
                        OnClosingRequested(NetSockClosingStatus.Unknown);
                        OnErrorReceived("ClosingDataReceived", ex);
                    }
                }
                else
                {
                    try
                    {
                        DataArrived?.Invoke(this, e);
                    }
                    catch (Exception ex)
                    {
                        OnErrorReceived("OnDataArrived", ex);
                    }
                }
            }
        }

        protected void OnClosingRequested(NetSockClosingStatus closingStatus)
        {
            RemoteClosing?.Invoke(this, closingStatus);
            //在上级对象上处理客户端Closing事件
        }
        #endregion

        #region Keep-alives
        /*
		 * Note about usage of keep-alives
		 * The TCP protocol does not successfully detect "abnormal" socket disconnects at both
		 * the client and server end. These are disconnects due to a computer crash, cable 
		 * disconnect, or other failure. The keep-alive mechanism built into the TCP socket can
		 * detect these disconnects by essentially sending null data packets (header only) and
		 * waiting for acks.
		 */

        /// <summary>Structure for settings keep-alive bytes</summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct Tcp_keepalive
        {
            /// <summary>1 = on, 0 = off</summary>
            public uint onoff;
            /// <summary>TCP inactivity before sending keep-alive packet (ms)</summary>
            public uint keepalivetime;
            /// <summary>Interval to send keep-alive packet if acknowledgement was not received (ms)</summary>
            public uint keepaliveinterval;
        }

        /// <summary>Set up the socket to use TCP keep alive messages</summary>
        internal protected void SetKeepAlive()
        {
            try
            {
                Tcp_keepalive sioKeepAliveVals = new Tcp_keepalive
                {
                    onoff = (uint)1, // 1 to enable 0 to disable
                    keepalivetime = KeepAliveInactivity,
                    keepaliveinterval = KeepAliveInterval
                };

                IntPtr p = Marshal.AllocHGlobal(Marshal.SizeOf(sioKeepAliveVals));
                Marshal.StructureToPtr(sioKeepAliveVals, p, true);
                byte[] inBytes = new byte[Marshal.SizeOf(sioKeepAliveVals)];
                Marshal.Copy(p, inBytes, 0, inBytes.Length);
                Marshal.FreeHGlobal(p);
                byte[] outBytes = BitConverter.GetBytes(0);
                socket.IOControl(IOControlCode.KeepAliveValues, inBytes, outBytes);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Keep Alive", ex);
            }
        }
        #endregion

        #region Connection Sanity Check
        private void ConnectedTimerCallback(object sender)
        {
            try
            {
                if (state == SocketState.Connected &&
                    (socket == null || !socket.Connected))
                    CloseOnly("Connect Timer");
            }
            catch (Exception ex)
            {
                OnErrorReceived("ConnectTimer", ex);
                CloseOnly("Connect Timer Exception");
            }
        }
        #endregion

    }


    #endregion
    /// <summary>
    /// 1V1 tcp server by socket
    /// </summary>
    public class NetServer : NetBase
    {
        #region Events
        /// <summary>A socket has requested a connection</summary>
        public event EventHandler<NetSockConnectionRequestEventArgs> ConnectionRequested;
        #endregion

        #region Listen
        /// <summary>Listen for incoming connections</summary>
        /// <param name="port">Port to listen on</param>
        public void Listen(int port)
        {
            try
            {
                if (socket != null)
                {
                    try
                    {
                        socket.Close();
                    }
                    catch { }; // ignore problems with old socket
                }
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);
                socket.Bind(ipLocal);
                socket.Listen(1);
                socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);
                OnChangeState(SocketState.Listening);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Listen", ex);
            }
        }

        /// <summary>Callback for BeginAccept</summary>
        /// <param name="ar"></param>
        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket listener = (Socket)ar.AsyncState;
                Socket sock = listener.EndAccept(ar);

                if (state == SocketState.Listening)
                {
                    if (socket != listener)
                    {
                        CloseOnly("Async Listen Socket mismatched");
                        return;
                    }

                    if (ConnectionRequested != null)
                        ConnectionRequested(this, new NetSockConnectionRequestEventArgs(sock));
                }

                if (state == SocketState.Listening)
                    socket.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                else
                {
                    try
                    {
                        listener.Close();
                    }
                    catch (Exception ex)
                    {
                        OnErrorReceived("Close Listen Socket", ex);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (SocketException ex)
            {
                CloseOnly("Listen Socket Exception");
                OnErrorReceived("Listen Socket", ex);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Listen Socket", ex);
            }
        }
        #endregion

        #region Accept
        /// <summary>Accept the connection request</summary>
        /// <param name="client">Client socket to accept</param>
        public void Accept(Socket client)
        {
            try
            {
                if (state != SocketState.Listening)
                    throw new Exception("Cannot accept Socket is " + state.ToString());

                if (socket != null)
                {
                    try
                    {
                        socket.Close(); // close listening socket
                    }
                    catch { } // don't care if this fails
                }

                socket = client;

                socket.ReceiveBufferSize = byteBuffer.Length;
                socket.SendBufferSize = byteBuffer.Length;

                SetKeepAlive();

                OnChangeState(SocketState.Connected);
                OnConnected(socket);

                Receive();
            }
            catch (Exception ex)
            {
                OnErrorReceived("Accept", ex);
            }
        }
        #endregion

    }


}
