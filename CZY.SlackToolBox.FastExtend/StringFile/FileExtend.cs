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

        #region 读取/存储图片
        /// <summary>
        /// 根据当前路径获取图片 并解除程序对图片的占用  当前路径为绝对路径
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
        /// <summary>
        /// 将图片保存到当前路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="image"></param>
        public static void SaveBitmapToLocal(this string fileName,BitmapSource image)
        {
            using (var fs = System.IO.File.Create(fileName))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fs);
            }
        }


        /// <summary>
        /// 将图片保存到当前路径
        /// </summary>
        /// <param name="picPath">图片地址</param>
        /// <param name="img">图片数组</param>
        /// <returns></returns>
        public static int SaveByteToLocal(this string picPath, byte[] img)
        {
            int result = 1;
            try
            {
                MemoryStream ms = new MemoryStream(img);
                System.Drawing.Image bm = System.Drawing.Image.FromStream(ms, true);
                //Bitmap bmp = new Bitmap(ms);
                bm.Save(picPath);
                ms.Close();
            }
            catch
            {
                result = 0;
            }

            return result;
        }


        /// <summary>
        /// 从当前的base64字符串读入图片
        /// </summary>
        /// <param name="base64">base64字符串</param>
        /// <returns></returns>
        public static System.Drawing.Image FromBase64ToImage(this string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
            return img;
        }

        #endregion

        #region 图片压缩


        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth">宽度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public static bool PicThumbnail(string sFile, string dFile, int dHeight, int dWidth, int flag)
        {
            using (var s = new FileStream(sFile, FileMode.Open))
            {
                using (var d = new FileStream(dFile, FileMode.Create))
                {
                    return PicThumbnail(s, d, dHeight, dWidth, flag);
                }
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
        /// <returns>返回的流需要自己释放，否则存在内存泄露</returns>
        public static MemoryStream PicThumbnail(string sFile, int dHeight, int dWidth, int flag)
        {
            using (var s = new FileStream(sFile, FileMode.Open))
            {
                var d = new MemoryStream();
                PicThumbnail(s, d, dHeight, dWidth, flag);
                return d;
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
        public static bool PicThumbnail(Stream sFile, Stream dFile, int dHeight, int dWidth, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromStream(sFile);
            ImageFormat tFormat = iSource.RawFormat;


            //计算按照缩放比例后的图像宽高
            int sW = 0, sH = 0;
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
            g.Clear(System.Drawing.Color.White);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new System.Drawing.Rectangle(
                (dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0,
                iSource.Width, iSource.Height, GraphicsUnit.Pixel);
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
        /// 获取文件路径中的目录位置（不包括文件名）
        /// </summary>
        /// <param name="path">文件位置</param>
        /// <returns></returns>
        public static string GetPathDirectory(this string filepath)
        {
            if (!filepath.Contains("\\"))
                return CurrentDir;

            string pattern = @"^(.*)\\.*?$";
            Match match = Regex.Match(filepath, pattern);

            return match.Groups[1].ToString();
        }
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
        /// 将文件名字变成符合系统存储规范的名字
        /// </summary>
        /// <param name="fileName">文件名字</param>
        /// <returns></returns>
        public static string TrimSpecialInFileName(this string fileName)
        {
            var chars = Path.GetInvalidFileNameChars();
            StringBuilder sb = new StringBuilder(fileName);
            foreach (var c in chars)
            {
                sb.Replace(c.ToString(), string.Empty);
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 将目录变成符合系统存储规范的名字
        /// </summary>
        /// <param name="path">目录名字</param>
        /// <returns></returns>
        public static string TrimSpecialInPath(string path)
        {
            var chars = Path.GetInvalidPathChars();
            StringBuilder sb = new StringBuilder(path);
            foreach (var c in chars)
            {
                sb.Replace(c.ToString(), string.Empty);
            }
            return sb.ToString().Trim();
        }
        /// <summary>
        /// 获取当前程序根目录
        /// </summary>
        /// <returns></returns>
        public static string CurrentDir => AppDomain.CurrentDomain.BaseDirectory;
       

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
                 path=$"{CurrentDir}\\logs\\{DateTime.Now.ToString("yyyy-MM-dd-HH")}.txt";
            }
            string content = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:{msg}";

            WriteTxt(content, path+ filename);
        }

        #endregion

        #region 拷贝

        /// <summary>
        /// 拷贝文件到指定目录下
        /// </summary>
        /// <param name="srcdir">原文件所在目录</param>
        /// <param name="dstdir">目标目录</param>
        /// <param name="overwrite">是否覆盖文件</param>
        public static void CopyFileToDestDirectory(this string srcdir, string dstdir, bool overwrite)
        {
            string todir = Path.GetDirectoryName(dstdir);

            if (!Directory.Exists(todir))
                Directory.CreateDirectory(todir);

            File.Copy(srcdir, Path.Combine(todir, Path.GetFileName(srcdir)), overwrite);
        }


        /// <summary>
        /// 拷贝目录
        /// </summary>
        /// <param name="srcPath">原路径</param>
        /// <param name="destPath">目标路径</param>
        public static void CopyDirectory(this string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    string destName = destPath + "\\" + i.Name;
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
                throw;
            }
        }

        /// <summary>
        /// 复制文件夹及文件,根目录除外
        /// </summary>
        /// <param name="srcdir">原文件路径</param>
        /// <param name="dstdir">目标文件路径</param>
        /// <returns></returns>
        public static int CopyFolderSub(this string srcdir, string dstdir)
        {
            try
            {
                //如果目标路径不存在,则创建目标路径
                if (!System.IO.Directory.Exists(dstdir))
                {
                    System.IO.Directory.CreateDirectory(dstdir);
                }
                //得到原文件根目录下的所有文件
                string[] files = System.IO.Directory.GetFiles(srcdir);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(dstdir, name);
                    System.IO.File.Copy(file, dest, true);//复制文件
                }
                //得到原文件根目录下的所有文件夹
                string[] folders = System.IO.Directory.GetDirectories(srcdir);
                foreach (string folder in folders)
                {
                    string name = System.IO.Path.GetFileName(folder);
                    string dest = System.IO.Path.Combine(dstdir, name);
                    CopyFolderSub(folder, dest);//构建目标路径,递归复制文件
                }
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }

        }

        /// <summary>
        /// 复制文件夹及文件，含根目录
        /// </summary>
        /// <param name="srcdir">原文件路径</param>
        /// <param name="dstdir">目标文件路径</param>
        /// <returns></returns>
        public static int CopyFolder(this string srcdir, string dstdir)
        {
            try
            {
                string folderName = System.IO.Path.GetFileName(srcdir);
                string destfolderdir = System.IO.Path.Combine(dstdir, folderName);
                string[] filenames = System.IO.Directory.GetFileSystemEntries(srcdir);
                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    if (System.IO.Directory.Exists(file))
                    {
                        string currentdir = System.IO.Path.Combine(destfolderdir, System.IO.Path.GetFileName(file));
                        if (!System.IO.Directory.Exists(currentdir))
                        {
                            System.IO.Directory.CreateDirectory(currentdir);
                        }
                        CopyFolder(file, destfolderdir);
                    }
                    else
                    {
                        string srcfileName = System.IO.Path.Combine(destfolderdir, System.IO.Path.GetFileName(file));
                        if (!System.IO.Directory.Exists(destfolderdir))
                        {
                            System.IO.Directory.CreateDirectory(destfolderdir);
                        }
                        System.IO.File.Copy(file, srcfileName, true);
                    }
                }

                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        #endregion
    }
}
