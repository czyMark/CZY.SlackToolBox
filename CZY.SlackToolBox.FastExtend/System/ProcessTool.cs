using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class ProcessTool
    {
        /// <summary>
        /// 启动进程执行cmd命令
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
