using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace  CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// WIndows进程相关操作
    /// </summary>
    public static class ProcessTool
    {
        #region 进程相关WIndwos API
        [DllImport("ntdll.dll")]
        public static extern int NtQueryObject(IntPtr ObjectHandle, int ObjectInformationClass, IntPtr ObjectInformation, int ObjectInformationLength, ref int returnLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);

        [DllImport("ntdll.dll")]
        public static extern uint NtQuerySystemInformation(int SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength, ref int returnLength);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, ushort hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("NTDLL.DLL")]
        public static extern int NtOpenProcess(ref IntPtr handle, int Mask, ref OBJECT_ATTRIBUTES OBJ, ref CLIENT_ID ID);

        public const int PROCESS_DUP_HANDLE = 0x40;

        [DllImport("NTDLL.DLL")]
        public static extern int NtDuplicateObject(/*1*/IntPtr SourceProcessHandle,/*2*/ushort SourceHandle, /*3*/IntPtr TargetProcessHandle, /*4*/ref IntPtr TargetHandle, /*5*/int DesiredAccess,/*6*/int HandleAttributes,/*7*/int Options);
        public const int DUPLICATE_CLOSE_SOURCE = 0x1;

        [DllImport("NTDLL.DLL")]
        public static extern int NtClose(IntPtr H);

        public enum ObjectInformationClass : int
        {
            ObjectBasicInformation = 0,
            ObjectNameInformation = 1,
            ObjectTypeInformation = 2,
            ObjectAllTypesInformation = 3,
            ObjectHandleInformation = 4
        }

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_BASIC_INFORMATION
        {
            // Information Class 0
            public int Attributes;
            public int GrantedAccess;
            public int HandleCount;
            public int PointerCount;
            public int PagedPoolUsage;
            public int NonPagedPoolUsage;
            public int Reserved1;
            public int Reserved2;
            public int Reserved3;
            public int NameInformationLength;
            public int TypeInformationLength;
            public int SecurityDescriptorLength;
            public System.Runtime.InteropServices.ComTypes.FILETIME CreateTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_TYPE_INFORMATION
        {
            // Information Class 2
            public UNICODE_STRING Name;
            public int ObjectCount;
            public int HandleCount;
            public int Reserved1;
            public int Reserved2;
            public int Reserved3;
            public int Reserved4;
            public int PeakObjectCount;
            public int PeakHandleCount;
            public int Reserved5;
            public int Reserved6;
            public int Reserved7;
            public int Reserved8;
            public int InvalidAttributes;
            public GENERIC_MAPPING GenericMapping;
            public int ValidAccess;
            public byte Unknown;
            public byte MaintainHandleDatabase;
            public int PoolType;
            public int PagedPoolUsage;
            public int NonPagedPoolUsage;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_NAME_INFORMATION
        {
            // Information Class 1
            public UNICODE_STRING Name;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;
            public IntPtr Buffer;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct OBJECT_ATTRIBUTES
        {
            public int Length;
            public IntPtr RootDirectory;
            public IntPtr ObjectName;
            public uint Attributes;
            public IntPtr SecurityDescriptor;
            public IntPtr SecurityQualityOfService;
        }

        //[StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CLIENT_ID
        {
            public int UniqueProcess;
            public IntPtr UniqueThread;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct GENERIC_MAPPING
        {
            public int GenericRead;
            public int GenericWrite;
            public int GenericExecute;
            public int GenericAll;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_HANDLE_INFORMATION
        {
            // Information Class 16
            public int ProcessID;
            public byte ObjectTypeNumber;
            public byte Flags; // 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
            public ushort Handle;
            public int Object_Pointer;
            public UInt32 GrantedAccess;
        }

        public const int MAX_PATH = 260;
        public const uint STATUS_INFO_LENGTH_MISMATCH = 0xC0000004;
        public const int DUPLICATE_SAME_ACCESS = 0x2;

        public const uint STATUS_SUCCESS = 0x00;

        const int CNST_SYSTEM_HANDLE_INFORMATION = 16;




        [DllImport("ntdll.dll")]
        private static extern uint NtDuplicateObject(
            IntPtr SourceProcessHandle,
            IntPtr SourceHandle,
            IntPtr TargetProcessHandle,
            ref IntPtr TargetHandle,
            int DesiredAccess,
            int HandleAttributes,
            int Options);

        #endregion


        /// <summary>
        /// 查看电脑是否是64位系统
        /// </summary>
        /// <returns>返回是否是64位系统bool值</returns>
        public static bool Is64Bits()
        {
            return Marshal.SizeOf(typeof(IntPtr)) == 8 ? true : false;
        }
        /// <summary>
        /// 获取目标进程的所有句柄信息
        /// </summary>
        /// <param name="process">目标进程</param>
        /// <returns>句柄信息列表</returns>
        public static List<SYSTEM_HANDLE_INFORMATION> GetHandles(this Process process)
        {
            uint nStatus;
            int nHandleInfoSize = 0x10000;
            IntPtr ipHandlePointer = Marshal.AllocHGlobal(nHandleInfoSize);
            int nLength = 0;
            IntPtr ipHandle = IntPtr.Zero;
            while ((nStatus = NtQuerySystemInformation(CNST_SYSTEM_HANDLE_INFORMATION, ipHandlePointer, nHandleInfoSize, ref nLength)) == STATUS_INFO_LENGTH_MISMATCH)
            {
                nHandleInfoSize = nLength;
                Marshal.FreeHGlobal(ipHandlePointer);
                ipHandlePointer = Marshal.AllocHGlobal(nLength);
            }
            /*****************/
            /* 原代码，怀疑此处内存泄漏
            byte[] baTemp = new byte[nLength];
            Marshal.Copy(ipHandlePointer, baTemp, 0, nLength);
            long lHandleCount = 0;
            if (Is64Bits())
            {
                lHandleCount = Marshal.ReadInt64(ipHandlePointer);
                ipHandle = new IntPtr(ipHandlePointer.ToInt64() + 8);
            }
            else
            {
                lHandleCount = Marshal.ReadInt32(ipHandlePointer);
                ipHandle = new IntPtr(ipHandlePointer.ToInt32() + 4);
            }*/
            long lHandleCount = 0;
            if (Is64Bits())
            {
                lHandleCount = Marshal.ReadInt64(ipHandlePointer);
                ipHandle = new IntPtr(ipHandlePointer.ToInt64() + 8);
            }
            else
            {
                lHandleCount = Marshal.ReadInt32(ipHandlePointer);
                ipHandle = new IntPtr(ipHandlePointer.ToInt32() + 4);
            }
            /****************/
            SYSTEM_HANDLE_INFORMATION shHandle;
            List<SYSTEM_HANDLE_INFORMATION> lstHandles = new List<SYSTEM_HANDLE_INFORMATION>();
            for (long lIndex = 0; lIndex < lHandleCount; lIndex++)
            {
                shHandle = new SYSTEM_HANDLE_INFORMATION();
                int BB = ipHandle.ToString().Length;
                if (Is64Bits())
                {
                    shHandle = (SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle) + 8);
                }
                else
                {
                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle));
                    int dd = ipHandle.ToString().Length;
                    shHandle = (SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
                }
                if (shHandle.ProcessID != process.Id) continue;
                lstHandles.Add(shHandle);
            }
            /***************加上释放**************/
            Marshal.FreeHGlobal(ipHandlePointer);
            /*************************************/
            ////Program.ShowMem(System.Reflection.MethodInfo.GetCurrentMethod().Name);
            return lstHandles;
        }
        /// <summary>
        /// 按原名称获取规则文件路径
        /// </summary>
        /// <param name="strRawName"></param>
        /// <returns>文件路径</returns>
        private static string GetRegularFileNameFromDevice(string strRawName)
        {
            string strFileName = strRawName;
            foreach (string strDrivePath in Environment.GetLogicalDrives())
            {
                StringBuilder sbTargetPath = new StringBuilder(MAX_PATH);
                if (QueryDosDevice(strDrivePath.Substring(0, 2), sbTargetPath, MAX_PATH) == 0)
                {
                    ////Program.ShowMem(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                    return strRawName;
                }
                string strTargetPath = sbTargetPath.ToString();
                if (strFileName.StartsWith(strTargetPath))
                {
                    strFileName = strFileName.Replace(strTargetPath, strDrivePath.Substring(0, 2));
                    break;
                }
            }
            ////Program.ShowMem(System.Reflection.MethodInfo.GetCurrentMethod().Name);
            return strFileName;
        }
        /// <summary>
        /// 获取进程文件路径名
        /// </summary>
        /// <param name="process">指定进程</param>
        /// <param name="sYSTEM_HANDLE_INFORMATION">句柄信息列表</param>
        /// <returns>文件路径名</returns>
        public static string GetFilePath(this Process process,SYSTEM_HANDLE_INFORMATION sYSTEM_HANDLE_INFORMATION)
        {
            IntPtr m_ipProcessHwnd = OpenProcess(ProcessAccessFlags.All, false, process.Id);
            IntPtr ipHandle = IntPtr.Zero;
            var objBasic = new OBJECT_BASIC_INFORMATION();
            IntPtr ipBasic = IntPtr.Zero;
            var objObjectType = new OBJECT_TYPE_INFORMATION();
            IntPtr ipObjectType = IntPtr.Zero;
            var objObjectName = new OBJECT_NAME_INFORMATION();
            IntPtr ipObjectName = IntPtr.Zero;
            string strObjectTypeName = "";
            string strObjectName = "";
            int nLength = 0;
            int nReturn = 0;
            IntPtr ipTemp = IntPtr.Zero;

            if (!DuplicateHandle(m_ipProcessHwnd, sYSTEM_HANDLE_INFORMATION.Handle, GetCurrentProcess(), out ipHandle, 0, false, DUPLICATE_SAME_ACCESS))
            {
                return null;
            }
            ipBasic = Marshal.AllocHGlobal(Marshal.SizeOf(objBasic));
            NtQueryObject(ipHandle, (int)ObjectInformationClass.ObjectBasicInformation, ipBasic, Marshal.SizeOf(objBasic), ref nLength);
            objBasic = (OBJECT_BASIC_INFORMATION)Marshal.PtrToStructure(ipBasic, objBasic.GetType());
            Marshal.FreeHGlobal(ipBasic);
            ipObjectType = Marshal.AllocHGlobal(objBasic.TypeInformationLength);
            nLength = objBasic.TypeInformationLength;
            while ((uint)(nReturn = NtQueryObject(ipHandle, (int)ObjectInformationClass.ObjectTypeInformation, ipObjectType, nLength, ref nLength)) == STATUS_INFO_LENGTH_MISMATCH)
            {
                Marshal.FreeHGlobal(ipObjectType);
                ipObjectType = Marshal.AllocHGlobal(nLength);
            }
            objObjectType = (OBJECT_TYPE_INFORMATION)Marshal.PtrToStructure(ipObjectType, objObjectType.GetType());
            ipTemp = Is64Bits() ? new IntPtr(Convert.ToInt64(objObjectType.Name.Buffer.ToString(), 10) >> 32) : objObjectType.Name.Buffer;
            strObjectTypeName = Marshal.PtrToStringUni(ipTemp, objObjectType.Name.Length >> 1);
            Marshal.FreeHGlobal(ipObjectType);
            nLength = objBasic.NameInformationLength;
            ipObjectName = Marshal.AllocHGlobal(nLength);
            while ((uint)(nReturn = NtQueryObject(ipHandle, (int)ObjectInformationClass.ObjectNameInformation, ipObjectName, nLength, ref nLength)) == STATUS_INFO_LENGTH_MISMATCH)
            {
                Marshal.FreeHGlobal(ipObjectName);
                ipObjectName = Marshal.AllocHGlobal(nLength);
            }
            objObjectName = (OBJECT_NAME_INFORMATION)Marshal.PtrToStructure(ipObjectName, objObjectName.GetType());

            if (nLength < 0)
                return null;

            ipTemp = Is64Bits() ? new IntPtr(Convert.ToInt64(objObjectName.Name.Buffer.ToString(), 10) >> 32) : objObjectName.Name.Buffer;
            if (ipTemp != IntPtr.Zero)
            {
                byte[] baTemp = new byte[nLength];
                try
                {
                    Marshal.Copy(ipTemp, baTemp, 0, nLength);

                    strObjectName = Marshal.PtrToStringUni(Is64Bits() ? new IntPtr(ipTemp.ToInt64()) : new IntPtr(ipTemp.ToInt32()));
                }
                catch (AccessViolationException)
                {
                    return null;
                }
                finally
                {
                    Marshal.FreeHGlobal(ipObjectName);
                    CloseHandle(ipHandle);
                }
            }
            try
            {
                return GetRegularFileNameFromDevice(strObjectName);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 关闭指定
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static bool CloseProcessHandle(this UInt16 handle, int pid)
        {
            CLIENT_ID cid = new CLIENT_ID();
            OBJECT_ATTRIBUTES oa = new OBJECT_ATTRIBUTES();
            cid.UniqueProcess = pid;
            oa.Length = Marshal.SizeOf(typeof(OBJECT_ATTRIBUTES));

            IntPtr src_proc_handle = IntPtr.Zero;
            if (STATUS_SUCCESS != NtOpenProcess(ref src_proc_handle, PROCESS_DUP_HANDLE, ref oa, ref cid))
            {
                return false;
            }

            IntPtr target_handle = IntPtr.Zero;
            if (STATUS_SUCCESS != NtDuplicateObject(
                    src_proc_handle, handle,
                    GetCurrentProcess(), ref target_handle,
                    0, 0, DUPLICATE_CLOSE_SOURCE))
            {
                return false;
            }

            return (STATUS_SUCCESS == NtClose(target_handle));
        }


        /// <summary>
        /// 启动进程 执行cmd命令
        /// </summary>
        /// <param name="Command">cmd命令</param>
        public static string ExecCommand(this string command)
        {
            Process p = new Process();

            p.StartInfo.FileName = "cmd.exe";

            p.StartInfo.CreateNoWindow = true;

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardError = true;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardOutput = true;

            //p.StartInfo.Arguments = "/c " + command;
            p.Start();

            //执行完成后立即退出
            p.StandardInput.WriteLine(command + "&exit");
            //p.StandardInput.WriteLine("exit");
            p.StandardInput.AutoFlush = true;

            //获取命令窗口的返回结果
            string temp = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
            //等待执行完成后退出
            p.WaitForExit();
            p.Close();

            return temp;
        }


        /// <summary>
        /// 以进程的方式启动 指定路径的程序
        /// </summary>
        /// <param name="ExeName">程序</param>
        /// <param name="ExeArguments">参数</param>
        /// <param name="ProcessesName">启动后的进程名字  任务管理器中的显示的名字</param>
        /// <param name="WaitTime">等待程序启动时间</param>
        /// <returns></returns>
        public static bool StartProcess(this string ExeName, string ExeArguments, string ProcessesName="",int WaitTime=500)
        {
            Process p = new Process();

            //设置要启动的应用程序
            p.StartInfo.FileName = ExeName;
            //p.StartInfo.Arguments = "-k";
            p.StartInfo.Arguments = ExeArguments;
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            //// 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = false;
            ////输出信息
            p.StartInfo.RedirectStandardOutput = true;
            //// 输出错误
            //p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();
            if (string.IsNullOrEmpty(ProcessesName))
            {
                //等待让进程启动
                Thread.Sleep(WaitTime);
                //验证进程名称是否以存在
                System.Diagnostics.Process[] calexes = System.Diagnostics.Process.GetProcessesByName(ProcessesName);
                if (calexes.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 启动另外进程
        /// </summary>
        /// <param name="ExeName">进程名称</param>
        /// <param name="args">参数</param>
        /// <returns>启动是否成功</returns>
        public static bool StartProcess(this string ExeName, string[] args)
        {
            try
            {
                string s = "";
                foreach (string arg in args) { s = s + arg + " "; }
                s = s.Trim();
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo(ExeName, s); // 括号里是(程序名,参数) 
                process.StartInfo = startInfo;
                process.Start();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("系统提示", "启动应用程序时出错！原因：" + ex.Message);
            }
            return false;
        }



        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="ProcessesName">进程名   任务管理器中的显示的名字</param>
        public static void KillProcess(this string ProcessesName)
        {
            System.Diagnostics.Process[] influxds = System.Diagnostics.Process.GetProcessesByName(ProcessesName);
            if (influxds.Length > 0)
            {
                foreach (Process influxd in influxds)
                {
                    influxd.Kill();
                }
            }
        }
        /// <summary>
        /// 验证系统中是否运行了当前程序
        /// </summary>
        /// <returns>true 找到 false 未找到</returns>
        public static bool VerifyRunning()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process item in processes)
            {
                if (item.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 验证系统中是否运行了某程序
        /// </summary>
        /// <param name="process">进程名称</param>
        /// <returns>true 找到 false 未找到</returns>
        public static bool VerifyRunning(this string process)
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(process);
            foreach (Process item in processes)
            {
                if (item.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// 获取正在运行的程序进程-匹配指定运行路径的程序 没有获取到说明程序没有被启动
        /// </summary>
        /// <param name="process">程序名称</param>
        /// <param name="localPath">指定的程序运行路径</param>
        /// <returns></returns>
        public static Process VerifyRunning(this string process, string localPath)
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(process);
            foreach (Process item in processes)
            {
                if (item.Id != current.Id)
                {
                    if (localPath == item.MainModule.FileName)
                    {
                        return item;
                    }
                }
            }
            return null;
        }


       /// <summary>
       /// 依据进程id结束进程
       /// </summary>
       /// <param name="list_pid"></param>
        public static void PidKill(List<int> list_pid)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            foreach (var item in list_pid)
            {
                p.StandardInput.WriteLine("taskkill /pid " + item + " /f");
                p.StandardInput.WriteLine("exit");
            }

            Task.Delay(1000).GetAwaiter().GetResult();
            p.Close();
            p.Dispose();
        }
        /// <summary>
        /// 依据程序端口获取进程id
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static List<int> GetPidByPort(int port)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            p.StandardInput.WriteLine(string.Format("netstat -ano|find \"{0}\"", port));
            p.StandardInput.WriteLine("exit");
            StreamReader reader = p.StandardOutput;
            string strLine = reader.ReadLine();
            List<int> list_pid = new List<int>();
            while (!reader.EndOfStream)
            {
                strLine = strLine.Trim();
                if (strLine.Length > 0 && ((strLine.Contains("TCP") || strLine.Contains("UDP"))))
                {
                    Regex r = new Regex(@"\s+");
                    string[] strArr = r.Split(strLine);
                    if (strArr.Length >= 4)
                    {
                        if (strArr[1].EndsWith($":{port}"))
                        {
                            int result;
                            bool b = int.TryParse(strArr[4], out result);
                            if (b && !list_pid.Contains(result))
                                list_pid.Add(result);
                        }
                    }
                }
                strLine = reader.ReadLine();
            }
            p.WaitForExit();
            reader.Close();
            p.Close();
            p.Dispose();
            return list_pid;
        }
        /// <summary>
        /// 依据进程Id获取进程名称
        /// </summary>
        /// <param name="list_pid"></param>
        /// <returns></returns>
        public static List<string> GetProcessNameByPid(List<int> list_pid)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            List<string> list_process = new List<string>();
            foreach (var pid in list_pid)
            {
                if (pid == 0)
                    continue;

                p.StandardInput.WriteLine(string.Format("tasklist |find \"{0}\"", pid));
                p.StandardInput.WriteLine("exit");
                StreamReader reader = p.StandardOutput;//截取输出流
                string strLine = reader.ReadLine();//每次读取一行

                while (!reader.EndOfStream)
                {
                    strLine = strLine.Trim();
                    if (strLine.Length > 0 && ((strLine.Contains(".exe"))))
                    {
                        Regex r = new Regex(@"\s+");
                        string[] strArr = r.Split(strLine);
                        if (strArr.Length > 0)
                        {
                            list_process.Add(strArr[0]);
                        }
                    }
                    strLine = reader.ReadLine();
                }
                p.WaitForExit();
                reader.Close();
            }
            p.Close();
            p.Dispose();
            return list_process;
        }
    }
}
