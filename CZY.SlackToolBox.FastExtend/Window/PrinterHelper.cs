using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend.Window
{
    /// <summary>
    /// Win32 API 调用命令打印
    /// </summary>
    public static class PrinterHelper
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName; // 文档名称
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile; // 输出文件（可选）
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType; // 数据类型（通常是 "RAW"）
        }

        // 声明 Win32 API 函数
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", CharSet = CharSet.Unicode, EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        /// <summary>
        /// 发生打印命令到打印机。  （可以将打印的内容直接转换成btye打印）
        /// </summary>
        /// <param name="szPrinterName">打印机名称</param>
        /// <param name="bytes">命令或内容</param>
        /// <param name="docName">打印队列显示文档名称</param>
        /// <param name="dataType">数据类型 默认为RAW</param>
        /// <returns></returns>
        public static bool SendBytesToPrinter(string szPrinterName, byte[] bytes,string docName= "RAW Document", string dataType= "RAW")
        {
            IntPtr hPrinter = IntPtr.Zero; // 打印机句柄
            DOCINFOA di = new DOCINFOA(); // 文档信息结构体
            bool bSuccess = false; // 成功标志

            // 设置文档信息
            di.pDocName = docName; // 文档名称
            di.pDataType = dataType; // 数据类型为原始数据

            // 打开打印机
            Console.WriteLine("Opening printer...");
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // 开始文档
                Console.WriteLine("Starting document...");
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // 开始页面
                    Console.WriteLine("Starting page...");
                    if (StartPagePrinter(hPrinter))
                    {
                        //// 写入设置字符集的命令
                        //Debug.WriteLine("Setting Chinese mode...");
                        int dwWritten = 0;
                        // 设置打印机为 GB2312 字符集
                        // 十六进制字节表示为字符串
                        dwWritten = 0;
                        //优点:
                        //1.更高的性能: 使用指针直接操作内存可以提高性能，特别是在处理大量数据或需要高效访问内存的场景下。
                        //2.与非托管代码交豆: 允许与非托管代码进行更直接的交豆，例如调用 Windows API或者使用一些底层的系统功能,
                        //3.灵活性: 可以执行一些 C# 中无法直接实现的操作，如访问特定的内存地址或进行底层的位操作。
                        //缺点:
                        //1.安全性风险: 使用 unsafe 可能导致程序出现潜在的安全漏洞，因为绕过了 C# 的类型安全检查和边界检查。
                        //2.可读性下降: 使用指针和不安全的操作会增加代码的复杂性，并且降低代码的可读性和可维护性。
                        //3.难以调试: 不安全的代码可能更难调试和定位错误，因为涉及到直接操作内存的技术细节。
                        unsafe
                        {
                            fixed (byte* pBytes = bytes)
                            {
                                bSuccess &= WritePrinter(hPrinter, (IntPtr)pBytes, bytes.Length, out dwWritten);
                            }
                        }
                        Console.WriteLine($"Bytes written: {dwWritten}");

                        // 结束页面
                        Console.WriteLine("Ending page...");
                        if (!EndPagePrinter(hPrinter))
                        {
                            Console.WriteLine($"Error ending page: {Marshal.GetLastWin32Error()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error starting page: {Marshal.GetLastWin32Error()}");
                    }
                    // 结束文档
                    Console.WriteLine("Ending document...");
                    if (!EndDocPrinter(hPrinter))
                    {
                        Console.WriteLine($"Error ending document: {Marshal.GetLastWin32Error()}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error starting document: {Marshal.GetLastWin32Error()}");
                }
                // 关闭打印机
                Console.WriteLine("Closing printer...");
                if (!ClosePrinter(hPrinter))
                {
                    Console.WriteLine($"Error closing printer: {Marshal.GetLastWin32Error()}");
                }
            }
            else
            {
                Console.WriteLine($"Error opening printer: {Marshal.GetLastWin32Error()}");
            }

            // 如果操作不成功，输出最后的错误代码
            if (!bSuccess)
            {
                int lastError = Marshal.GetLastWin32Error();
                Debug.WriteLine($"Error: {lastError}");
            }

            return bSuccess; // 返回操作是否成功的标志
        }
    }
}
