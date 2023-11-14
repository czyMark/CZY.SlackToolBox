using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Threading;

namespace  CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// 模拟操作
    /// </summary>
    public class SimulatedOperationTool
	{
		/// <summary>
		/// 获得指定的窗口的句柄
		/// </summary>
		/// <param name="Point"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(POINT Point);

		/// <summary>
		/// 获取鼠标在窗体中的当前位置
		/// </summary>
		/// <param name="lpPoint"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetCursorPos(out POINT lpPoint);

		/// <summary>
		/// 该函数将指定的消息发送到一个或多个窗口
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="Msg"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImportAttribute("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// 该函数将指定的消息发送到一个或多个窗口
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="Msg"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImportAttribute("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
		/// <summary>
		/// 刷新图像
		/// </summary>
		/// <returns></returns>
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();



		#region 移动窗口
		const int WM_NCLBUTTONDOWN = 0xA1;
		const int HT_CAPTION = 0x2;
		const uint WM_SYSCOMMAND = 0x0112;
		const uint SC_MOVE = 0xF010;
		const uint HTCAPTION = 0x0002;

		/// <summary>
		/// 移动窗口
		/// </summary>
		/// <param name="Handle">控件的 Handle</param>
		public static void ControlMoveMouseDown(IntPtr Handle)
		{
			ReleaseCapture();
			SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;
		}
		/// <summary>
		/// 拖动Popup属性框
		/// </summary>
		public static void ControlMoveMouseDown()
		{
			POINT curPos;
			IntPtr hWndPopup;

			GetCursorPos(out curPos);
			hWndPopup = WindowFromPoint(curPos);

			ReleaseCapture();

			SendMessage(hWndPopup, WM_NCLBUTTONDOWN, new IntPtr(HT_CAPTION), IntPtr.Zero);
		} 
		#endregion



		/// <summary>
		/// 模拟winfrom界面Application.DoEvents()方法，避免程序在 线程中操作时间过长导致 操作界面假死
		/// 
		/// </summary>
		[SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void DoEvents()
		{
			//按钮只触发一次
			//btn.IsEnabled = false;
			//VerifyRepeatInputTool.DoEvents();
			//btn.IsEnabled = true;
			DispatcherFrame frame = new DispatcherFrame();
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);
			try
			{
				Dispatcher.PushFrame(frame);
			}
			catch (InvalidOperationException) { }
		}
		private static object ExitFrames(object frame)
		{
			((DispatcherFrame)frame).Continue = false;
			return null;
		}
		 
	}
}
