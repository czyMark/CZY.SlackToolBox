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
    /// 1vN tcp socket server event arguments 
    /// </summary>
    public class AsyncSocketEventArgs : EventArgs
    {
        /// <summary>
        /// 连接的客户端状态
        /// </summary>
        public NetState State { get; set; }


        public AsyncSocketEventArgs(NetState state = null)
        {
            this.State = state;
        }

    }


    public class AsyncSockStateChangedEventArgs : AsyncSocketEventArgs
    {
        public SocketState NewState { get; set; }

        public SocketState OldState { get; set; }

        public AsyncSockStateChangedEventArgs(SocketState newState, SocketState oldState, NetState state) : base(state)
        {
            NewState = newState;
            OldState = oldState;
        }
    }

    public class AsyncSockConnectionRequestEventArgs : AsyncSocketEventArgs
    {
        public Socket Client { get; set; }

        public AsyncSockConnectionRequestEventArgs(Socket client, NetState state = null) : base(state)
        {
            Client = client;
        }
    }

    public class AsyncSockConnectedEventArgs : AsyncSocketEventArgs
    {
        public IPAddress clientIP { get; set; }

        public AsyncSockConnectedEventArgs(IPAddress cliIP, NetState state = null) : base(state)
        {
            clientIP = cliIP;
        }
    }

    public class AsyncSockDisconnectedEventArgs : AsyncSocketEventArgs
    {
        public string Reason { get; set; }

        public AsyncSockDisconnectedEventArgs(string reason, NetState state) : base(state)
        {
            Reason = reason;
        }
    }

    public class AsyncSockDataArrivedEventArgs : AsyncSocketEventArgs
    {
        public byte[] Data { get; set; }

        public AsyncSockDataArrivedEventArgs(byte[] data, NetState state) : base(state)
        {
            Data = data;
        }
    }

    public class AsyncSockErrorReceivedEventArgs : AsyncSocketEventArgs
    {
        public Exception OccuredException { get; set; }

        public string ErrMsg { get; set; }

        public AsyncSockErrorReceivedEventArgs(string msg, NetState state, Exception ex = null) : base(state)
        {
            ErrMsg = msg;
            OccuredException = ex;
        }
    }

    public class AsyncSockRemoteClosingEventArgs : AsyncSocketEventArgs
    {
        /// <summary>
        /// 主动关闭连接前的状态<br/>
        /// 0 - 一般状况
        /// 1 - 操作成功，主动断开
        /// 2 - 操作失败，主动断开
        /// </summary>
        public NetSockClosingStatus ClosingStatus { get; set; }

        public AsyncSockRemoteClosingEventArgs(NetState state, NetSockClosingStatus closingStatus) : base(state)
        {
            ClosingStatus = closingStatus;
        }
    }

    public class AsyncSockSendCompletedEventArgs : AsyncSocketEventArgs
    {
        /// <summary>
        /// 用户在Send中传入的引用
        /// </summary>
        public object UserState;
        /// <summary>
        /// 发送结果<br/>
        /// 0 - 成功<br/>
        /// 非0 - 失败<br/>
        /// </summary>
        public NetSockSendCompletedResult SendResult;

        public string Message;

        public AsyncSockSendCompletedEventArgs(NetState state, object userState, NetSockSendCompletedResult sendResult, string message) : base(state)
        {
            UserState = userState;
            SendResult = sendResult;
            Message = message;
        }
    }
}
