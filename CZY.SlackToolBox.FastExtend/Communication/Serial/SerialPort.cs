﻿using System;
using System.IO.Ports;
using System.Text;

namespace CZY.SlackToolBox.FastExtend.Communication
{
    /// <summary>
    /// 串通信 返回字符串。
    /// 多个串口 创建多个
    /// </summary>
    public class SerialPort
    {
        public enum ReceiveType
        {
            Byte,
            String,
        }

        System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();

        /// <summary>
        ///  C#串口监听  需要链接的设备的串口名称可在设备管理器中查看 其余参数通过厂家设置手册处理 RS232及USB使用的军事115200波特率
        /// </summary>
        /// <param name="portName">需要链接的设备的串口名称</param>
        /// <param name="portRate">波特率 目前使用的设备</param>
        /// <param name="stopBits">停止位</param>
        /// <param name="parity">奇偶校验位</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="receivedBytesThreshold">事件触发器内部缓冲区字节数 。当接收的字节大一次收不完的时候修改</param>
        public SerialPort(string portName, string portRate, StopBits stopBits = StopBits.One, Parity parity = Parity.None, int dataBits = 8, int receivedBytesThreshold = 10)
        {
            sp.PortName = portName;
            sp.BaudRate = Convert.ToInt32(portRate);
            sp.Parity = parity;
            sp.StopBits = stopBits;
            sp.DataBits = dataBits;
            sp.ReceivedBytesThreshold = receivedBytesThreshold;
            sp.DataReceived += new SerialDataReceivedEventHandler(Sp_DataReceived);
            try
            {
                sp.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设备返回值等待时间
        /// </summary>
        public int Timeout = 0;
        /// <summary>
        /// 接收到的的信息返回值类型
        /// </summary>
        public ReceiveType Receive = ReceiveType.String;
        /// <summary>
        /// 接收到的数据
        /// </summary>
        object ReceivedData = string.Empty;
        /// <summary>
        /// 扫描状态
        /// </summary>
        bool ScanState = false;

        #region 串口发送命令

        public void Send(byte[] command, int offset, int count) => sp.Write(command, offset, count);
        public void Send(char[] command, int offset, int count) => sp.Write(command, offset, count);
        public void Send(string command) => sp.Write(command); 
        #endregion

        /// <summary>
        /// 串口返回的信息
        /// </summary> 
        public void Sp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //部分设备无法一次接收完成，需要等待处理完成后在接收，所以等待一秒完全接收完成后在显示
            System.Threading.Thread.Sleep(Timeout);

            switch (Receive)
            {
                case ReceiveType.Byte:
                    {
                        byte[] readBuffer = new byte[sp.ReadBufferSize];
                        sp.Read(readBuffer, 0, readBuffer.Length);
                        ReceivedData = readBuffer;
                        ScanState = true;
                    }
                    break;
                case ReceiveType.String:
                    {

                        //将接收返回的值 转换成string
                        byte[] readBuffer = new byte[sp.ReadBufferSize];
                        sp.Read(readBuffer, 0, readBuffer.Length);
                        ReceivedData = Encoding.UTF8.GetString(readBuffer);
                        ScanState = true;
                    }
                    break;
                default:
                    {
                        byte[] readBuffer = new byte[sp.ReadBufferSize];
                        sp.Read(readBuffer, 0, readBuffer.Length);
                        ReceivedData = readBuffer;
                        ScanState = true;
                    }
                    break;
            }  
        }
       
        /// <summary>
        /// 对外封装返回的参数
        /// </summary>
        /// <returns>
        /// 串口返回的结果
        /// ReceivedData 接收到的数据 类型为：设置的数据类型
        /// NewData 是否是新扫描的数据
        /// </returns>
        public dynamic GetReceivedData()
        {
            if (ScanState)
            {
                ScanState = false;
                return new { ReceivedData, NewData = true };
            }
            return new { ReceivedData, NewData = false };
        }

        /// <summary>
        /// 关闭串口监听
        /// </summary>
        public void ClosePort()
        {
            sp.Close();
        }

    }
}
