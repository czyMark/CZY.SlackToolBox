using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace CZY.SlackToolBox.FastExtend.BarCode
{
	/// <summary>
	/// 条码抢扫描
	/// </summary>
    public class BarcodeScannerTool
	{
		private SerialPort sp = new SerialPort();
		private string barcode = string.Empty;
		bool ScanState = false;
		/// <summary>
		/// 扫码枪 C#串口监听 默认
		/// </summary>
		/// <param name="portName">设置硬件扫码枪链接的串口 可在设备管理器中查看</param>
		/// <param name="portRate">波特率 目前使用的设备 根据设置手册 RS232及USB使用的军事115200波特率</param>
		/// <param name="stopBits">停止位</param>
		/// <param name="parity">奇偶校验位</param>
		/// <param name="dataBits">数据位</param>
		public BarcodeScannerTool(string portName, string portRate, StopBits stopBits = StopBits.One, Parity parity = Parity.None, int dataBits = 8)
		{
			sp.PortName = portName;
			sp.BaudRate =
			Convert.ToInt32(portRate);
			sp.Parity = parity;
			sp.StopBits = stopBits;
			sp.DataBits = dataBits;
			sp.ReceivedBytesThreshold = 1;
			sp.DataReceived +=
			new SerialDataReceivedEventHandler(Sp_DataReceived);
			try
			{
				sp.Open();
			}
			catch(Exception ex)
			{
				throw ex; 
			}
		}

		/// <summary>
		/// 接收扫码枪返回的信息
		/// </summary> 
		public void Sp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			//部分扫码枪有条码太长会有延迟，所以等待一秒完全接收完成后在显示
			System.Threading.Thread.Sleep(1000);

			//将接收返回的值 转换成string
			byte[] readBuffer = new byte[sp.ReadBufferSize];
			sp.Read(readBuffer, 0, readBuffer.Length);
			barcode = Encoding.UTF8.GetString(readBuffer);
			ScanState = true;
		}
		/// <summary>
		/// 对外封装返回的barcode
		/// </summary>
		/// <returns>条码</returns>
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
		/// 关闭扫码枪窗口监听
		/// </summary>
		public void ClosePort()
		{
			sp.Close();
		}

	} 
}
