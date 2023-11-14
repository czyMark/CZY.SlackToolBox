using System;
using System.Diagnostics;
using System.Reflection;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class ProcessTool
    {
        /// <summary>
        /// 验证系统中是否运行了当前程序
        /// </summary>
        /// <returns>true 找到 false 未找到</returns>
        public static bool VerifyRunning()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
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
        /// <param name="processName">进程名称</param>
        /// <returns>true 找到 false 未找到</returns>
        public static bool VerifyRunning(this string processName)
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
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
        /// <param name="processName">程序名称</param>
        /// <param name="localPath">指定的程序运行路径</param>
        /// <returns></returns>
        public static Process VerifyRunning(this string processName, string localPath)
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (localPath == process.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// 启动另外进程
        /// </summary>
        /// <param name="exename">进程名称</param>
        /// <param name="args">参数</param>
        /// <returns>启动是否成功</returns>
        public static bool StartProcess(string exename, string[] args)
        {
            try
            {
                string s = "";
                foreach (string arg in args) { s = s + arg + " "; }
                s = s.Trim();
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo(exename, s); // 括号里是(程序名,参数) 
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
    }
}
