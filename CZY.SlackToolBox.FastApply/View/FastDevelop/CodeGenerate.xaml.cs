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
using FT = CZY.SlackToolBox.FrameTemplate;

namespace CZY.SlackToolBox.FastApply.View.FastDevelop
{
    /// <summary>
    /// CodeGenerate.xaml 的交互逻辑
    /// </summary>
    public partial class CodeGenerate : UserControl
    {
        public static CodeGenerate Instance = new CodeGenerate();
        public CodeGenerate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) { return; }
            switch (btn.Content)
            {
                case "英骁科技":
                    FT.YXKJ.Core.UserCache.Init();
                    new FT.YXKJ.View.LoginWindow().Show();
                    break;
                case "工具箱窗口":
                    FT.SettingWindow.Core.UserCache.Init();
                    new FT.SettingWindow.View.MainWindow().Show();
                    break;
                case "平台聊天":
                    new FT.ChartTool.View.LoginWindow().Show();
                    break;
                case "机场管理系统":
                    FT.AirportCS.Core.UserCache.Init();
                    new FT.AirportCS.View.LoginWindow().Show();
                    break;
                case "系统加载":
                    new FT.Functional.InitSysWindow().ShowDialog();
                    break;
                case "最小化窗口":
                    var win = new FT.Functional.TrayWindow();
                    win.ShowNotofy("程序退出", "正常退出");
                    break;


                case "微粉大师":
                    new ChatRobot.Imaging.MainWindow().ShowDialog();
                    break;

                case "映彩机器人":
                    new ChatRobot.Master.MainWindow().ShowDialog();
                    break;
                case "映彩机器人自定义":
                    new ChatRobot.ProcessDesign.MainWindow().ShowDialog();
                    break;


            }

        }
    }
}
