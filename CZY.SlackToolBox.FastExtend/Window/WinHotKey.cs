using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CZY.SlackToolBox.FastExtend.Window
{
    public static class ViewTreeTool
    {

        /// <summary>
        /// 键盘Hook的数据结构体。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int ddwExtraInfo;
        }
        public enum SendMessageTimeoutFlag : uint
        {
            SMTO_NORMAL = 0x0000,
            SMTO_BLOCK = 0x0001,
            SMTO_ABORTIFHUNG = 0x0002,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
        }

        /// <summary>
        /// 钩子标识符
        /// </summary>
        private static int hHook = 0;
        const int WH_KEYBORAD_LL = 13;

        const int WM_SETTINGCHANGE = 0x001A;
        const int HWND_BROADCAST = 0xffff;

        /// <summary>
        /// 委托定义：键盘钩子的处理
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        protected delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        /// <summary>
        /// 委托声明：键盘钩子的处理。LowLevel键盘截获，如果是WH_KEYBOARD=2，并不能对系统键盘截取，会在你借去之前获得键盘。
        /// </summary>
        private static HookProc KeyBoardHookProcedure;

        #region win32API   
        //安装钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //卸载钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern bool UnhookWindowsHookEx(int idHook);
        //下一个钩挂的函数
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        //获取键盘状态
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern int GetKeyState(int vKey);
        //获取模块句柄
        [DllImport("kernel32.dll")]
        static extern IntPtr GetModuleHandle(string name);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr SendMessageTimeout(IntPtr handle, uint msg, IntPtr wParam, IntPtr lParam, SendMessageTimeoutFlag flags, uint timeout, out IntPtr result);
        #endregion

        private static int KeyBoardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                Keys key = (Keys)kbh.vkCode;
                switch (key)
                {//屏蔽单键,例如：win+D,win+Tab,win+R,alt+tab,
                    case Keys.LWin:
                    case Keys.RWin:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                        return 1;
                }

                if ((int)System.Windows.Forms.Control.ModifierKeys == (int)ModifierKeys.Windows &&
                    (key == Keys.L || key == Keys.R || key == Keys.D || key == Keys.Tab))
                {//win+D,win+Tab,win+R,win+L(此处无效，通过禁用“锁定计算机”选项后有效)
                    return 1;
                }
                if ((key == Keys.Tab || key == Keys.F4) && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
                { //alt+tab,alt+F4
                    return 1;
                }
                if (key == Keys.Escape && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Control)
                { //ctrl+ESC
                    return 1;
                }

                //if (key == Keys.Delete && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)
                //{//ctrl+alt+del,但无效，只有通过注册表移除选项的方式实现
                //    Debug.Print("截获ctrl+alt+del");
                //    //ProcessMgr.SuspendWinlogon();

                //    //ProcessMgr.ResumeWinlogon();
                //    return 1;
                //}
                Debug.Print($"code:{kbh.vkCode}");
            }

            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// 安装钩子事件。
        /// </summary>
        private static void SetHotKeyDisable()
        {
            if (hHook == 0)
            {
                KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);
                IntPtr hInstance = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
                hHook = SetWindowsHookEx(WH_KEYBORAD_LL, KeyBoardHookProcedure, hInstance, 0);
            }
        }

        /// <summary>
        /// 取消钩子事件。
        /// </summary>
        private static void SetHotKeyEnable()
        {
            if (hHook != 0)
            {
                UnhookWindowsHookEx(hHook);
                hHook = 0;
            }
        }
        /// <summary>
        /// 注销选项
        /// </summary>
        private const string RegKey_Explorer = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer";
        /// <summary>
        /// 修改密码、锁定计算机、任务管理器选项
        /// </summary>
        private const string RegKey_System = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
        private const string RegKey_Logoff = "NoLogoff";//注销
        private const string RegKey_DisableChangePassword = "DisableChangePassword";//更改密码
        private const string RegKey_DisableLockWorkstation = "DisableLockWorkstation";//锁定计算机
        private const string RegKey_DisableTaskMgr = "DisableTaskMgr";//任务管理器
        private const string RegKey_DisableSwitchUserOption = "DisableSwitchUserOption";//切换用户，系统存在多用户时才会有选项，单用户删除该key也不影响
        private const string RegKey_NoClose = "NoClose";//电源选项
        /// <summary>
        /// 启用ctrl+alt+del的所有选项
        /// </summary>
        private static void SetSystemFunctionEnable()
        {
            var expKey = Registry.CurrentUser.CreateSubKey(RegKey_Explorer);
            expKey.DeleteValue(RegKey_Logoff, false);
            //expKey.DeleteValue(RegKey_NoClose, false);
            expKey.Close();

            var sysKey = Registry.CurrentUser.CreateSubKey(RegKey_System);
            sysKey.DeleteValue(RegKey_DisableChangePassword, false);
            sysKey.DeleteValue(RegKey_DisableLockWorkstation, false);
            sysKey.DeleteValue(RegKey_DisableTaskMgr, false);
            sysKey.DeleteValue(RegKey_DisableSwitchUserOption, false);
            sysKey.Close();

        }
        /// <summary>
        /// 禁用ctrl+alt+del的所有选项
        /// </summary>
        private static void SetSystemFunctionDisable()
        {
            var expKey = Registry.CurrentUser.CreateSubKey(RegKey_Explorer);
            expKey.SetValue(RegKey_Logoff, 1, RegistryValueKind.DWord);
            //expKey.SetValue(RegKey_NoClose,1,RegistryValueKind.DWord);
            expKey.Close();

            var sysKey = Registry.CurrentUser.CreateSubKey(RegKey_System);
            sysKey.SetValue(RegKey_DisableChangePassword, 1, RegistryValueKind.DWord);
            sysKey.SetValue(RegKey_DisableLockWorkstation, 1, RegistryValueKind.DWord);
            sysKey.SetValue(RegKey_DisableTaskMgr, 1, RegistryValueKind.DWord);
            sysKey.SetValue(RegKey_DisableSwitchUserOption, 1, RegistryValueKind.DWord);
            sysKey.Close();
        }
        /// <summary>
        /// 设置热键是否可用
        /// </summary>
        /// <param name="isEnable"></param>
        public static void SetHotKey(bool isEnable)
        {
            if (isEnable)
            {
                SetHotKeyEnable();
                //ctrl+alt+del和win+L 只有通过注册表移除选项的方式实现
                SetSystemFunctionEnable();
            }
            else
            {
                SetHotKeyDisable();
                SetSystemFunctionDisable();
            }
            //注册表修改完成后，通知所有（更新组策略无效；重启explorer进程部分已启动的程序丢失）
            //SendMessage(HWND_BROADCAST,WM_SETTINGCHANGE,0,0);
            //IntPtr result;
            //SendMessageTimeout(new IntPtr(HWND_BROADCAST),WM_SETTINGCHANGE,IntPtr.Zero,IntPtr.Zero,SendMessageTimeoutFlag.SMTO_NORMAL,1000,out result);
        }
    }
}
