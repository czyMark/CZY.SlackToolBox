using System;
using System.Runtime.InteropServices;

namespace  CZY.SlackToolBox.FastExtend
{

    /// <summary>
    /// 设置系统时间 调用系统功能使用到的结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
	public struct SystemTime
	{
		[MarshalAs(UnmanagedType.U2)]
		public ushort year;
		[MarshalAs(UnmanagedType.U2)]
		public ushort month;
		[MarshalAs(UnmanagedType.U2)]
		public ushort dayofweek;
		[MarshalAs(UnmanagedType.U2)]
		public ushort day;
		[MarshalAs(UnmanagedType.U2)]
		public ushort hour;
		[MarshalAs(UnmanagedType.U2)]
		public ushort minute;
		[MarshalAs(UnmanagedType.U2)]
		public ushort second;
		[MarshalAs(UnmanagedType.U2)]
		public ushort milliseconds;
	}
	public class SetSystemTool
	{
		[DllImport("Kernel32.dll")]
		public static extern void GetLocalTime(ref SystemTime lpSystemTime);

		[DllImport("Kernel32.dll")]
		public static extern int SetLocalTime(ref SystemTime lpSystemTime);

		[DllImport("Kernel32.dll")]
		public static extern void GetSystemTime(ref SystemTime lpSystemTime);

		[DllImport("Kernel32.dll")]
		public static extern void SetSystemTime(ref SystemTime lpSystemTime);
		 
		/// <summary>
		/// 设置系统时间
		/// 此函数需要程序以管理员权限运行才能有效
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static bool SetLocalTimePC(DateTime dateTime)
		{
			var sysTime = dateTime.ToSystemTime();
			return SetLocalTime(ref sysTime) == 0;
		}
	}
}
