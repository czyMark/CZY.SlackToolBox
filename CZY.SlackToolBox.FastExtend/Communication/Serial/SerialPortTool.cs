using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace CZY.SlackToolBox.FastExtend.Communication.Serial
{
    /// <summary>
    /// 条码抢扫描
    /// </summary>
    public class SerialPortTool
    {
        SerialPort sp = new SerialPort();
        string barcode = string.Empty;
        bool ScanState = false;
        /// <summary>
        ///  C#串口监听  需要链接的设备的串口名称可在设备管理器中查看 其余参数通过厂家设置手册处理 RS232及USB使用的军事115200波特率
        /// </summary>
        /// <param name="portName">需要链接的设备的串口名称</param>
        /// <param name="portRate">波特率 目前使用的设备</param>
        /// <param name="stopBits">停止位</param>
        /// <param name="parity">奇偶校验位</param>
        /// <param name="dataBits">数据位</param>
        public SerialPortTool(string portName, string portRate, StopBits stopBits = StopBits.One, Parity parity = Parity.None, int dataBits = 8, int receivedBytesThreshold = 10)
        {
            sp.PortName = portName;
            sp.BaudRate =
            Convert.ToInt32(portRate);
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
        /// 串口返回的信息
        /// </summary> 
        public void Sp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //部分设备无法一次接收完成，需要等待处理完成后在接收，所以等待一秒完全接收完成后在显示
            //System.Threading.Thread.Sleep(1000);

            //将接收返回的值 转换成string
            byte[] readBuffer = new byte[sp.ReadBufferSize];
            sp.Read(readBuffer, 0, readBuffer.Length);
            barcode = Encoding.UTF8.GetString(readBuffer);
            ScanState = true;
        }
        /// <summary>
        /// 对外封装返回的参数
        /// </summary>
        /// <returns>串口返回的结果</returns>
        public Dictionary<string, bool> LoadBarcode()
        {
            if (ScanState)
            {
                ScanState = false;
                return new Dictionary<string, bool> { { barcode, true } };
            }
            return new Dictionary<string, bool> { { barcode, false } };
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
