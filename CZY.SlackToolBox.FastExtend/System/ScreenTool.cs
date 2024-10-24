using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing.Imaging;

namespace CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// 与屏幕操作相关工具
    /// </summary>
    public static class ScreenTool
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(
            IntPtr hWnd,
            IntPtr hDc
            );

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(
            IntPtr hdc, // handle to DC
            int nIndex // index of capability
            );
        public enum DeviceCap
        {
            Desktopvertres = 117,
            Desktophorzres = 118
        }

        /// <summary>
        /// 获取屏幕显示分辨率
        /// </summary>
        /// <returns></returns>
        public static System.Drawing.Size GetPhysicalDisplaySize()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int physicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.Desktopvertres);
            int physicalScreenWidth = GetDeviceCaps(desktop, (int)DeviceCap.Desktophorzres);
            ReleaseDC(IntPtr.Zero, desktop);
            g.Dispose();
            return new System.Drawing.Size(physicalScreenWidth, physicalScreenHeight);
        }

        /// <summary>
        /// 重置屏幕缩放
        /// </summary>
        /// <returns></returns>
        public static double ResetScreenScale()
        {
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr desktop = g.GetHdc();
                int physicalScreenWidth = GetDeviceCaps(desktop, (int)DeviceCap.Desktophorzres);
                return physicalScreenWidth * 1.0000 / System.Windows.SystemParameters.PrimaryScreenWidth;
            }
        }

        #region 截图
        /// <summary>
        /// 截取屏幕指定位置
        /// </summary>
        /// <param name="x">开始位置X</param>
        /// <param name="y">开始位置Y</param>
        /// <param name="width">截取宽</param>
        /// <param name="height">截取高</param>
        /// <returns></returns>
        public static BitmapSource GetBitmapSource(int x, int y, int width, int height)
        {
            var bounds = GetPhysicalDisplaySize();
            var screenWidth = bounds.Width;
            var screenHeight = bounds.Height;
            var scaleWidth = (screenWidth * 1.0) / SystemParameters.PrimaryScreenWidth;
            var scaleHeight = (screenHeight * 1.0) / SystemParameters.PrimaryScreenHeight;
            var w = (int)(width * scaleWidth);
            var h = (int)(height * scaleHeight);
            var l = (int)(x * scaleWidth);
            var t = (int)(y * scaleHeight);
            using (var bm = new Bitmap(w, h, PixelFormat.Format32bppArgb))
            {
                using (var g = Graphics.FromImage(bm))
                {
                    g.CopyFromScreen(l, t, 0, 0, bm.Size);
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        bm.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }
        /// <summary>
        /// 窗口截图
        /// </summary>
        /// <param name="ScreenWidth">窗口宽    MainWindow.ScreenWidth</param>
        /// <param name="ScreenHeight">窗口高  MainWindow.ScreenHeight</param>
        /// <param name="ScreenScale">窗口缩放    MainWindow.ScreenScale</param>
        /// <returns></returns>
        public static BitmapSource GetFullBitmapSource(double ScreenWidth,double ScreenHeight, double ScreenScale)
        {
            var bounds = new System.Drawing.Size((int)(ScreenWidth * ScreenScale), (int)(ScreenHeight * ScreenScale));// ScreenHelper.GetPhysicalDisplaySize();

            Bitmap _Bitmap = new Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var bmpGraphics = Graphics.FromImage(_Bitmap);
            var x = System.Windows.SystemParameters.VirtualScreenLeft;
            var y = System.Windows.SystemParameters.VirtualScreenTop;
            bmpGraphics.CopyFromScreen((int)x, (int)y, 0, 0, _Bitmap.Size);
            return Imaging.CreateBitmapSourceFromHBitmap(
                _Bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
        #endregion

        //public static void GetScreenSize()
        //{
        //    foreach (Screen screen in Screen.AllScreens)
        //    {
        //        var dm = new DEVMODE();
        //        dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
        //        EnumDisplaySettings(screen.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

        //        Debug.WriteLine($"Device: {screen.DeviceName}");
        //        Debug.WriteLine($"Real Resolution: {dm.dmPelsWidth}x{dm.dmPelsHeight}");
        //        Debug.WriteLine($"Virtual Resolution: {screen.Bounds.Width}x{screen.Bounds.Height}");
        //    }            
        //}

        //const int ENUM_CURRENT_SETTINGS = -1;

        //[DllImport("user32.dll")]
        //static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        //[StructLayout(LayoutKind.Sequential)]
        //public struct DEVMODE
        //{
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        //    public string dmDeviceName;
        //    public short dmSpecVersion;
        //    public short dmDriverVersion;
        //    public short dmSize;
        //    public short dmDriverExtra;
        //    public int dmFields;
        //    public int dmPositionX;
        //    public int dmPositionY;
        //    public ScreenOrientation dmDisplayOrientation;
        //    public int dmDisplayFixedOutput;
        //    public short dmColor;
        //    public short dmDuplex;
        //    public short dmYResolution;
        //    public short dmTTOption;
        //    public short dmCollate;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        //    public string dmFormName;
        //    public short dmLogPixels;
        //    public int dmBitsPerPel;
        //    public int dmPelsWidth;
        //    public int dmPelsHeight;
        //    public int dmDisplayFlags;
        //    public int dmDisplayFrequency;
        //    public int dmICMMethod;
        //    public int dmICMIntent;
        //    public int dmMediaType;
        //    public int dmDitherType;
        //    public int dmReserved1;
        //    public int dmReserved2;
        //    public int dmPanningWidth;
        //    public int dmPanningHeight;
        //}
    }
}
