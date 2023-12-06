using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
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
	public static class FileExtend
	{
        #region 文件字符串验证

        #region 验证文件扩展名
        /// <summary>
        /// 依据文件扩展名验证文字是否是图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool VerifyImage(this string file)
        {
            string str = System.IO.Path.GetExtension(file);
            if (str.ToUpper() == ".JPG" || str.ToUpper() == ".JPEG" || str.ToUpper() == ".PNG" || str.ToUpper() == ".GIF")
                return true;
            return false;
        }

        /// <summary>
        /// 验证文件是否是指定扩展名
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool VerifyImage(this string file, string extension)
        {
            string str = System.IO.Path.GetExtension(file);
            if (str.ToUpper() == ".JPG" || str.ToUpper() == ".JPEG" || str.ToUpper() == ".PNG" || str.ToUpper() == ".GIF")
                return true;
            return false;
        }
        #endregion

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


        #region 图片压缩
         
        /// <summary>
        /// 将当前文件压缩并存储
        /// </summary>
        /// <param name="file"></param>

        public static void PicThumbnail(this string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                int width = image.Width;//宽
                int height = image.Height;//高
                if (width > 1920)
                    width = 1920;
                if (height > 1080)
                    height = 1080;
                PicThumbnail(file, file, height, width, 55);
            }
        }

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth">宽度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public static bool PicThumbnail(this string sFile, string dFile, int dHeight, int dWidth, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            System.Drawing.Size tem_size = new System.Drawing.Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth) //将**改成c#中的或者操作符号
            {
                if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(System.Drawing.Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new System.Drawing.Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();

                ImageCodecInfo jpegICIinfo = null;

                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();

            }
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


        #region 拷贝

        /// <summary>
        /// 拷贝目录
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        public static void CopyDirectory(this string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    string destName = destPath +"\\" + i.Name;
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destName))
                        {
                            Directory.CreateDirectory(destName);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destName);    //递归调用复制子文件夹
                    }
                    else
                    {
                        File.Copy(i.FullName, destName);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception e)
            {
                throw ;
            }
        }
        /// <summary>
        /// 拷贝文件到指定目录下
        /// </summary>
        /// <param name="srcdir"></param>
        /// <param name="dstdir"></param>
        /// <param name="overwrite"></param>
        public static void CopyFileToDestDirectory(this string srcdir, string dstdir, bool overwrite)
        {
            string todir = Path.GetDirectoryName(dstdir);

            if (!Directory.Exists(todir))
                Directory.CreateDirectory(todir);

            File.Copy(srcdir, Path.Combine(todir, Path.GetFileName(srcdir)), overwrite);
        }

        #endregion
    }
}
