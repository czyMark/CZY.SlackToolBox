using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class ImageTool
    {
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



        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
        /// <summary>
        /// 将System.Drawing.Bitmap 图像转换成 BitmapSource 图片这么处理后 将被释放，主要用于解决wpf图像内存溢出问题
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static BitmapSource ToBitmapSource(this System.Drawing.Bitmap bmp)
        {
            try
            {
                var ptr = bmp.GetHbitmap();
                var source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(ptr);
                return source;
            }
            catch
            {
                return null;
            }
        }




        /// <summary>
        /// 将BitmapImage 转换为 Bitmap
        /// </summary>
        /// <param name="bitmAPImage"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(this BitmapImage bitmAPImage)
        {
            // BitmAPImage bitmAPImage = new BitmAPImage(new Uri("../Images/test.png", UriKind.relative));
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmAPImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }
        /// <summary>
        /// 将imageSource 转换为 Bitmap
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(this ImageSource imageSource)
        {
            BitmapSource bitmapSource = (BitmapSource)imageSource;
            Bitmap bmp = new Bitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            BitmapData data = bmp.LockBits(
                new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            bitmapSource.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }

        /// <summary>
        /// 将Bitmap 转换为 BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }

        /// <summary>
        /// 图片转二进制
        /// </summary> 
        /// <returns>二进制</returns>
        public static byte[] ToByte(this Bitmap bitmap)
        {
            //将Image转换成流数据，并保存为byte[]
            MemoryStream mstream = new MemoryStream();
            bitmap.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length);
            mstream.Close();
            return byData;
        }

        /// <summary>
        /// 图像镜像
        /// </summary>
        /// <param name="curBitmap"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="direction">0水平镜像 1垂直镜像</param>
        public static void BitmapMirror(this Bitmap curBitmap, int width, int height, int direction)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = width * height * 4;
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            int halfWidth = width / 2;
            int halfHeight = height / 2;
            byte temp;
            if (direction == 0)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < halfWidth; j++)
                    {
                        int index1 = i * width * 4 + 4 * j;               // B
                        int index2 = (i + 1) * width * 4 - (1 + j) * 4;
                        temp = rgbValues[index1];
                        rgbValues[index1] = rgbValues[index2];
                        rgbValues[index2] = temp;
                        index1 = i * width * 4 + 4 * j + 1;               // G
                        index2 = (i + 1) * width * 4 - (1 + j) * 4 + 1;
                        temp = rgbValues[index1];
                        rgbValues[index1] = rgbValues[index2];
                        rgbValues[index2] = temp;
                        index1 = i * width * 4 + 4 * j + 2;               // R
                        index2 = (i + 1) * width * 4 - (1 + j) * 4 + 2;
                        temp = rgbValues[index1];
                        rgbValues[index1] = rgbValues[index2];
                        rgbValues[index2] = temp;
                        index1 = i * width * 4 + 4 * j + 3;               // A
                        index2 = (i + 1) * width * 4 - (1 + j) * 4 + 3;
                        temp = rgbValues[index1];
                        rgbValues[index1] = rgbValues[index2];
                        rgbValues[index2] = temp;
                    }
                }
            }
            else
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < halfHeight; j++)
                    {
                        int index1 = j * width * 4 + i * 4;
                        int index2 = (height - j - 1) * width * 4 + i * 4;    // B
                        temp = rgbValues[index1];
                        rgbValues[index1] = rgbValues[index2];
                        rgbValues[index2] = temp;
                        index1 = j * width * 4 + i * 4 + 1;
                        index2 = (height - j - 1) * width * 4 + i * 4 + 1;    // G
                        temp = rgbValues[index1];
                        rgbValues[index1] = rgbValues[index2];
                        rgbValues[index2] = temp;
                        index1 = j * width * 4 + i * 4 + 2;
                        index2 = (height - j - 1) * width * 4 + i * 4 + 2;    // R
                        temp = rgbValues[index1];
                        rgbValues[index1] = rgbValues[index2];
                        rgbValues[index2] = temp;
                        index1 = j * width * 4 + i * 4 + 3;
                        index2 = (height - j - 1) * width * 4 + i * 4 + 3;    // A
                        temp = rgbValues[index1];
                        rgbValues[index1] = rgbValues[index2];
                        rgbValues[index2] = temp;
                    }
                }
            }
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            curBitmap.UnlockBits(bmpData);
        }


        /// <summary>  
        ///  重新设置图片宽高
        /// </summary>  
        /// <param name="bmp">原始Bitmap </param>  
        /// <param name="newW">新的宽度</param>  
        /// <param name="newH">新的高度</param>  
        /// <returns>处理以后的图片</returns>  
        public static Bitmap ResizeImage(this Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量   
                //g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
