using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

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

		#region 识别path是否是网络路径
		/// <summary>
		/// 识别path是否是网络路径
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool UrlDiscern(this string path)
		{
			if (Regex.IsMatch(path, @"(http|ftp|https)://"))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
        #endregion
		 
        #region 读取图片
        /// <summary>
        /// 根据绝对路径获取图片 并解除程序对图片的占用
        /// </summary>
        /// <param name="imgPath">绝对路径</param>
        /// <returns></returns>
        public static BitmapImage GetLocalImage(this string imgPath)
        {
            // Read byte[] from png file
            BinaryReader binReader = new BinaryReader(File.Open(imgPath, FileMode.Open));
            FileInfo fileInfo = new FileInfo(imgPath);
            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();

            // Init bitmap 
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = new MemoryStream(bytes);
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
        #endregion

        #region 创建文件

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

        #endregion

        #region 删除文件


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
        #endregion

        #region 快捷操作

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件目录</param>
        /// <returns></returns>
        public static bool Exists(this string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 获取当前程序根目录
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        #endregion

        #region 写操作

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用系统默认编码;若文件不存在则创建新的,若存在则覆盖
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        public static void WriteTxt(this string content, string path)
        {
            WriteTxt(content, path, null, null);
        }

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用自定义编码;若文件不存在则创建新的,若存在则覆盖
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码</param>
        public static void WriteTxt(this string content, string path, Encoding encoding)
        {
            WriteTxt(content, path, encoding, null);
        }

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用自定义模式,使用UTF-8编码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="fileModel">输出方法</param>
        public static void WriteTxt(this string content, string path, FileMode fileModel)
        {
            WriteTxt(content, path, Encoding.UTF8, fileModel);
        }

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用自定义编码以及写入模式
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="fileModel">写入模式</param>
        public static void WriteTxt(this string content, string path, Encoding encoding, FileMode fileModel)
        {
            WriteTxt(content, path, encoding, (FileMode?)fileModel);
        }

        /// <summary>
        /// 输出字符串到文件
        /// 注：使用自定义编码以及写入模式
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="fileModel">写入模式</param>
        private static void WriteTxt(this string content, string path, Encoding encoding, FileMode? fileModel)
        {
            encoding = encoding ?? Encoding.UTF8;
            fileModel = fileModel ?? FileMode.Create;

            //检查文件夹是否存在，不存在就创建
            string dir = Path.GetDirectoryName(path);
            dir.CreateDirectory(); 

            using (FileStream fileStream = new FileStream(path, fileModel.Value))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, encoding))
                {
                    streamWriter.Write(content);
                    streamWriter.Flush();
                }
            }
        }

        /// <summary>
        /// 输出日志到指定文件
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="path">日志文件位置路径 当为空时：程序运行目录下的logs文件夹下时间为当前小时的文件名称</param>
        /// <param name="filename">日志文件名称 当为空时：程序运行目录下的logs文件夹下时间为当前小时的文件名称</param>
        public static void WriteLog(this string msg, string path="",string filename="" )
        {
            if (string.IsNullOrEmpty(path))
            { 
                 path=$"{GetCurrentDir()}\\logs\\{DateTime.Now.ToString("yyyy-MM-dd-HH")}.txt";
            }
            string content = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:{msg}";

            WriteTxt(content, path+ filename);
        }

        #endregion
    }
}
