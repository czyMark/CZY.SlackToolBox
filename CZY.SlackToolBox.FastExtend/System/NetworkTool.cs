using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace  CZY.SlackToolBox.FastExtend
{
	/// <summary>
	/// 计算机网络工具类
	/// </summary>
	public static class NetworkTool
	{
		#region 检测网络状态
		//wininet.dll是Windows应用程序网络相关模块。
		[DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]
		private extern static bool InternetGetConnectedState(out int conState, int reder);

		/// <summary>
		/// 检测是否有网卡驱动 和 wininet.dll组合的方式验证是否连接到网路（正确的接入网卡适配器）
		/// </summary>
		/// <returns>true 网路连接正常  false 网路连接错误</returns>
		public static bool VerifyConnection()
		{
			if (new Network().IsAvailable)//有网卡驱动
			{
				int desCode = 0;
				//检查网路是否是通的，只要连接上交换机就是true
				if (InternetGetConnectedState(out desCode, 0))
				{
					//todo:扩展 根据返回的desCode 可以检测出是拨号上网还是其他的情况
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				//返回无法连接数据服务。需要检查本地网卡驱动
				return false;
			}
		}

		/// <summary>
		/// ping的方式检测网络是否连接
		/// </summary>
		/// <param name="ipAddr">ip地址</param>
		/// <param name="timeout">超时时间</param>
		/// <returns>true 能连接成功 false 无法连接</returns>
		public static bool PingIp(this string ipAddr, int timeout = 120)
		{
			try
			{
				//初始化pin
				Ping objPingSender = new Ping();
				PingOptions objPinOptions = new PingOptions();
				objPinOptions.DontFragment = true;
				string data = "";
				byte[] buffer = Encoding.UTF8.GetBytes(data);
				//发送pin命令
				PingReply objPinReply = objPingSender.Send(ipAddr, timeout, buffer, objPinOptions);
				string strInfo = objPinReply.Status.ToString();
				if (strInfo == "Success")
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		#endregion

		#region 监测网速及流量
		//被监测网络端口 
		private static List<NetworkInterface> networkInterfaces { get; set; }

		//监测的网络数据
		public static Dictionary<string, string> NetworkUpSpeed = new Dictionary<string, string>();
		public static Dictionary<string, string> NetworkDownSpeed = new Dictionary<string, string>();
		public static Dictionary<string, string> NetworkAllTraffic = new Dictionary<string, string>();
		private static Dictionary<string, long> NetworkBaseTraffic = new Dictionary<string, long>();
		private static Dictionary<string, long> NetworkOldUp = new Dictionary<string, long>();
		private static Dictionary<string, long> NetworkOldDown = new Dictionary<string, long>();

		//定时监控
		private static Timer timer = new Timer() { Interval = 1000 };


		//流量单位
		private static string[] units = new string[] { "KB/s", "MB/s", "GB/s" };
		private static string[] unitAlls = new string[] { "KB", "MB", "GB", "TB" };

		public static List<string> GetMachineNetwork()
		{
			List<string> list = new List<string>();	
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
			foreach (var item in nics)
			{
				list.Add(item.Description);
			}
			return list;
		}

		/// <summary>
		/// 获取网卡接口
		/// </summary>
		/// <returns></returns>
		public static List<NetworkInterface> GetNetworkInterfaces()
		{ 
			return networkInterfaces;
		}

		/// <summary>
		/// 启动网卡监控流量-指定网卡
		/// </summary>
		/// <param name="netCardDescription">网卡备注</param>
		/// <returns></returns>
		public static bool StartMonitorNetwork(List<string> netCardDescription)
		{
			//如果已经启动了，就不再启动
			if(timer.Enabled==true)
				return false;
			//创建定时器 记录每秒的数据流量
			timer.Elapsed += Timer_Elapsed;
			timer.Interval = 1000;

			//创建要监控的网络接口 
			networkInterfaces = new List<NetworkInterface>();
			 

			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
			foreach (var var in nics)
			{
				foreach (var item in netCardDescription)
				{
					if (var.Description.Contains(item))
					{
						networkInterfaces.Add(var);
					}
				}
			}
			if (networkInterfaces == null || networkInterfaces.Count == 0)
			{
				return false;
			}
			else
			{
				networkInterfaces.ForEach(networkInterface =>
				{
					long bytesSent = networkInterface.GetIPStatistics().BytesSent;
					long bytesReceived = networkInterface.GetIPStatistics().BytesReceived;
					NetworkBaseTraffic.Add(networkInterface.Description, bytesSent + bytesReceived);
					NetworkOldUp.Add(networkInterface.Description, bytesSent);
					NetworkOldDown.Add(networkInterface.Description, bytesReceived);
				});
				timer.Start();
				return true;
			}

		}

		/// <summary>
		/// 启动对所有网卡 监控流量
		/// </summary>
		/// <returns></returns>
		public static bool StartMonitorNetwork()
		{
			//如果已经启动了，就不再启动
			if (timer.Enabled == true)
				return false;
			//创建定时器 记录每秒的数据流量
			timer.Elapsed += Timer_Elapsed;
			timer.Interval = 1000;	

			//创建要监控的网卡
			networkInterfaces = new List<NetworkInterface>();


			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
			foreach (var var in nics)
			{
				networkInterfaces.Add(var);
			}

			if (networkInterfaces == null || networkInterfaces.Count == 0)
			{
				return false;
			}
			else
			{
				networkInterfaces.ForEach(networkInterface =>
				{
					long bytesSent = networkInterface.GetIPStatistics().BytesSent;
					long bytesReceived = networkInterface.GetIPStatistics().BytesReceived;
					NetworkBaseTraffic.Add(networkInterface.Description, bytesSent + bytesReceived);
					NetworkOldUp.Add(networkInterface.Description, bytesSent);
					NetworkOldDown.Add(networkInterface.Description, bytesReceived);
				});
				timer.Start();
				return true;
			}

		}

		public static void EndMonitorNetwork()
		{
			//停止网络监控
			timer.Stop();
			timer = new Timer() { Interval = 1000 };
			//清理网卡接口 
			networkInterfaces = null;


			//清除历史的监控流量
			NetworkBaseTraffic.Clear();
			NetworkOldUp.Clear();
			NetworkOldDown.Clear();
		}
		/// <summary>
		/// 监控定时器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			networkInterfaces.ForEach(networkInterface =>
			{
				CalcUpSpeed(networkInterface);
				CalcDownSpeed(networkInterface);
				CalcAllTraffic(networkInterface);
			});
		}

		#region 计算
		/// <summary>
		/// 计算上传速度
		/// </summary>
		private static void CalcUpSpeed(NetworkInterface networkInterface)
        {
            long nowValue = networkInterface.GetIPStatistics().BytesSent;
            int num = 0;
            double value = (nowValue - NetworkOldUp[networkInterface.Description]) / 1024.0;
            while (value > 1023)
            {
                value = (value / 1024.0);
                num++;
            }
            if (!NetworkUpSpeed.ContainsKey(networkInterface.Description))
                NetworkUpSpeed.Add(networkInterface.Description, value.ToString("0.0") + units[num]);
            else
                NetworkUpSpeed[networkInterface.Description] = value.ToString("0.0") + units[num];
            NetworkOldUp[networkInterface.Description] = nowValue;
        }
        /// <summary>
        /// 计算下载速度
        /// </summary>
        private static void CalcDownSpeed(NetworkInterface networkInterface)
        {
            long nowValue = networkInterface.GetIPStatistics().BytesReceived;
            int num = 0;
            double value = (nowValue - NetworkOldDown[networkInterface.Description]) / 1024.0;
            while (value > 1023)
            {
                value = (value / 1024.0);
                num++;
            }
            if (!NetworkDownSpeed.ContainsKey(networkInterface.Description))
                NetworkDownSpeed.Add(networkInterface.Description, value.ToString("0.0") + units[num]);
            else
                NetworkDownSpeed[networkInterface.Description] = value.ToString("0.0") + units[num];
            NetworkOldDown[networkInterface.Description] = nowValue;
        }

        /// <summary>
        /// 计算总流量
        /// </summary>
        private static void CalcAllTraffic(NetworkInterface networkInterface)
        {
            long nowValue = NetworkOldDown[networkInterface.Description] + NetworkOldUp[networkInterface.Description];
            int num = 0;
            double value = (nowValue - NetworkBaseTraffic[networkInterface.Description]) / 1024.0;
            while (value > 1023)
            {
                value = (value / 1024.0);
                num++;
            }

            if (!NetworkAllTraffic.ContainsKey(networkInterface.Description))
                NetworkAllTraffic.Add(networkInterface.Description, value.ToString("0.0") + unitAlls[num]);
            else
                NetworkAllTraffic[networkInterface.Description] = value.ToString("0.0") + unitAlls[num];
        }

        #endregion

        #endregion
    }
}
