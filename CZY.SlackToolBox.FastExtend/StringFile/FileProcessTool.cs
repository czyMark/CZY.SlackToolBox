using System;
using System.IO;
using System.Text.RegularExpressions;

namespace  CZY.SlackToolBox.FastExtend
{
	/// <summary>
	/// 创建文件的方式
	/// </summary>
	public enum CreateType
	{
		Overrite,
		NewName,
		DoNothing,
	}
	/// <summary>
	/// 文件处理操作帮助类
	/// </summary>
	public static class FileProcessTool
	{
		/// <summary>
		/// 创建文件路径
		/// </summary>
		/// <param name="src">文件路径</param>
		public static void CreateDirectory(this string src)
		{
			if (!Directory.Exists(src))
			{
				Directory.CreateDirectory(src);
			}
		}
		/// <summary>
		/// 创建文件或目录
		/// </summary>
		/// <param name="path">目录或者文件路径</param>
		/// <param name="createType">创建方式</param>
		public static void CreateFileOrDirectory(this string path, CreateType createType = CreateType.DoNothing)
		{
			if (path.Contains("."))
			{
				FileInfo fileInfo = new FileInfo(path);
				CreateFile(fileInfo, createType);
			}
			else
			{
				CreateDirectory(path); 
			}
		}

		/// <summary>
		/// 创建文件
		/// </summary>
		/// <param name="path">文件路径</param>
		/// <param name="createType">创建方式</param>
		public static void CreateFile(FileInfo fileInfo, CreateType createType = CreateType.DoNothing)
		{
			if (fileInfo.Exists)
			{
				switch (createType)
				{
					case CreateType.Overrite:
						{
							fileInfo.Create().Close();
						}
						break;

					case CreateType.NewName:
						{
							string directoryName = fileInfo.DirectoryName;
							string fileName = fileInfo.Name;

							string[] fileNameAndSuffix = fileName.Split('.');
							string fileHead = fileNameAndSuffix[0];
							string suffix = fileNameAndSuffix[1];

							int i = 1;
							string newFilePath;
							do
							{
								string newFileName = $"{fileHead}_{i}.{suffix}";
								newFilePath = $@"{directoryName}\{newFileName}";

								i++;
							} while (File.Exists(newFilePath));

							File.Create(newFilePath).Close();
						}
						break;

					case CreateType.DoNothing:
						break;
				}
			}
			else
			{
				Directory.CreateDirectory(fileInfo.DirectoryName);
				// 创建完文件，关闭
				fileInfo.Create().Close();
			}
		}




		/// <summary>
		/// 删除目录下所有文件
		/// </summary>
		/// <param name="srcPath">目录</param>
		public static void DelectDirectorys(this string srcPath)
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo(srcPath);
				FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
				foreach (FileSystemInfo i in fileinfo)
				{
					if (i is DirectoryInfo)//判断是否文件夹
					{
						DirectoryInfo subdir = new DirectoryInfo(i.FullName);
						subdir.Delete(true); //删除子目录和文件
					}
					else
					{ 
						File.Delete(i.FullName); 
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("删除文件夹文件错误：" + e.Message);
			}
		}


		/// <summary>
		/// 删除目录下所有文件中指定后缀的文件
		/// </summary>
		/// <param name="srcPath">目录</param>
		/// <param name="Extension">后缀名称数组,中存储的数据 .jpg  .png 内容</param>
		public static void DelectDirectorys(this string srcPath, string[] Extension)
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo(srcPath);
				FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
				foreach (FileSystemInfo i in fileinfo)
				{
					if (i is DirectoryInfo)//判断是否文件夹
					{
						DirectoryInfo subdir = new DirectoryInfo(i.FullName);
						subdir.Delete(true); //删除子目录和文件
					}
					else
					{
						for (int exten = 0; exten < Extension.Length; exten++)
						{
							if (Extension[exten] == i.Extension.ToLower())
							{
								File.Delete(i.FullName);
							}

						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("删除文件夹文件错误：" + e.Message);
			}
		}


		/// <summary>
		/// 删除目录下指定文件要求的文件
		/// </summary>
		/// <param name="srcPath">目录</param>
		public static void DelectDirectorys(this string srcPath, string regular)
		{
			try
			{
				Regex regex = new Regex(regular);
				DirectoryInfo dir = new DirectoryInfo(srcPath);
				FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
				foreach (FileSystemInfo i in fileinfo)
				{
					if (i is DirectoryInfo)//判断是否文件夹
					{
						DirectoryInfo subdir = new DirectoryInfo(i.FullName);
						subdir.Delete(true); //删除子目录和文件
					}
					else
					{
						if (regex.IsMatch(i.FullName))
						{
							File.Delete(i.FullName);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("删除文件夹文件错误：" + e.Message);
			}
		}

		/// <summary>
		/// 删除目录下所有文件中指定后缀且创建时间超过某个时间的过期文件
		/// </summary>
		/// <param name="srcPath">目录</param>
		/// <param name="Extension">后缀名称数组,中存储的数据 .jpg  .png 内容</param>
		/// <param name="Expiration">过期时间</param>
		public static void DelectDirectorys(this string srcPath, string[] Extension, DateTime Expiration)
		{
			try
			{
				DirectoryInfo dir = new DirectoryInfo(srcPath);
				FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
				foreach (FileSystemInfo i in fileinfo)
				{
					if (i is DirectoryInfo)//判断是否文件夹
					{
						DirectoryInfo subdir = new DirectoryInfo(i.FullName);
						subdir.Delete(true); //删除子目录和文件
					}
					else
					{
						for (int exten = 0; exten < Extension.Length; exten++)
						{
							if (Extension[exten] == i.Extension.ToLower() && i.CreationTime >= Expiration)
							{
								File.Delete(i.FullName);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("删除文件夹文件错误：" + e.Message);
			}
		}
	}
}
