using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend.Window
{
    /// <summary>
    /// 窗口操作工具
    /// </summary>
    public static class WindowTool
    {

        /// <summary>
        /// 恢复窗口大小
        /// </summary>
        /// <param name="hwnd"></param>
        public static void RecoverNormalSize(IntPtr hwnd)
        {
            //获取实例主窗口句柄
            //IntPtr hwnd = prevProcess.MainWindowHandle;
            if (hwnd != IntPtr.Zero)
            {
                //系统具有主窗体句柄
                //窗体已经最小化，恢复为正常大小
                if (IsIconic(hwnd))
                {
                    ShowWindow(hwnd, (short)ShowWindowStyles.SW_SHOWNORMAL);
                }
                SetForegroundWindow(hwnd);
            }
        }

        /// <summary>
        /// 将窗体置于最前
        /// </summary>
        public static void BringCurrentAppToForeground(IntPtr hWnd)
        {

            //激活主窗口
            SetForegroundWindow(hWnd);
            //激活主窗口对话框
            SetForegroundWindow(GetLastActivePopup(hWnd));
            //获取焦点
            SetFocus(hWnd);
        }

        //将窗体置于最前
        public static void SetWindowPos(IntPtr hwnd)
        {
            SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 500, 500, SWP_SHOWWINDOW);
        }

        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SWP_NOMOVE = 0x2;
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        // 将窗体置于最前
        [DllImport("User32.dll")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);


        [DllImport("User32.dll", EntryPoint = "IsIconic", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        /// <summary>
        /// 判断系统主窗体是否最小化
        /// </summary>
        /// <param name="ptr"></param>
        private static extern bool IsIconic(IntPtr ptr);

        [DllImport("User32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="prt"></param>
        /// <param name="cmdShow"></param>
        /// <returns></returns>
        private static extern int ShowWindow(IntPtr prt, short cmdShow);

        // 该函数将创建指定窗口的线程设置到前台，并且激活该窗口
        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow", CharSet = CharSet.Auto)]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        // 该函数确定指定窗口中的哪一个弹出式窗口是最近活动的窗口
        [DllImport("User32.dll", EntryPoint = "GetLastActivePopup", CharSet = CharSet.Auto)]
        private static extern IntPtr GetLastActivePopup(IntPtr hWnd);


        [DllImport("User32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);
        private enum ShowWindowStyles : short
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }
    }
}
