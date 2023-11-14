using System.Diagnostics;

namespace  CZY.SlackToolBox.FastExtend
{
	/// <summary>
	/// cmd命令相关工具
	/// </summary>
	public static class CMDCommandTool
	{
		///// <summary>
		///// 启动进程执行cmd命令
		///// </summary>
		///// <param name="Command">cmd命令</param>
		//public static void ExecCommand(this string Command)
		//{
		//	Process p = new Process();
		//	p.StartInfo.FileName = "cmd.exe";
		//	p.StartInfo.CreateNoWindow = true;
		//	p.StartInfo.UseShellExecute = false;
		//	p.StartInfo.RedirectStandardError = true;
		//	p.StartInfo.RedirectStandardInput = true;
		//	p.StartInfo.RedirectStandardOutput = true;
		//	p.StandardInput.WriteLine(Command + "&exit");
		//	p.Start();
		//	//p.StandardInput.WriteLine("exit");
		//	p.Close();
		//}



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
	}
}
