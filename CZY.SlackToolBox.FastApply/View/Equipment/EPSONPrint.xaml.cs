using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CZY.SlackToolBox.FastExtend;
using System.Drawing.Printing;
using CZY.SlackToolBox.FastExtend.Window;
using System.Data.SqlTypes;
namespace CZY.SlackToolBox.FastApply.View.Equipment
{
    /// <summary>
    /// EPSONPrint.xaml 的交互逻辑
    /// </summary>
    public partial class EPSONPrint : UserControl
    {
        public static EPSONPrint Instance = new EPSONPrint();
        public EPSONPrint()
        {
            InitializeComponent();


            #region 初始化打印机列表
            List<string> Printers = new List<string>();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.ToUpper().Contains("XPS")
                    || printer.ToUpper().Contains("FAX"))
                {
                    continue;
                }
                Printers.Add(printer);
            }
            ComboxStripPrinters.ItemsSource = Printers;
            ComboxStripPrinters.SelectedIndex = 0;
            #endregion


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument pdf = new PdfDocument();
            string filepath = "./AttachFile/Demo.pdf";
            pdf.LoadFromFile(filepath.UrlRelativeToAbsolute());
            pdf.PrintSettings.PrinterName = ComboxStripPrinters.SelectedItem.ToString(); //这就是打印机设备名称
            pdf.Print();
        }
        private void Button_Click_Print(object sender, RoutedEventArgs e)
        {

            //string initializePrinter = "\x1B\x40"; // ESC @ (ASCII 27, ASCII 64) 初始化打印机
            //string setChineseMode = "\x1B\x2E"; // ESC . (ASCII 27, ASCII 46) 切换到 GB2312 字符集  
            string setChineseMode1 = "\x1C"; // FS & (ASCII 28, ASCII 38) 设置为中文模式
            string chineseText = "这是一个测试"; //要打印的文本
            string newline = "\x0A"; // 换行  10
            string carriageReturn = "\x0D"; // 回车 13

            // 组合所有部分成一个完整的字符串 
            string fullCommand = setChineseMode1 + chineseText + newline + carriageReturn;
            byte[] bytes = fullCommand.ToBytes("GB2312"); //大部分打印机都是双字节打印，中文字符集是GB2312 所以通过这个转
            PrinterHelper.SendBytesToPrinter(ComboxStripPrinters.SelectedItem.ToString(), bytes);
        }
    }
}
