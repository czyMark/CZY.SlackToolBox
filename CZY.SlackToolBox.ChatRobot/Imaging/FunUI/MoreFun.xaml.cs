using System.Windows;
using System.Windows.Controls;
using CZY.SlackToolBox.ChatRobot.Core;
using CZY.SlackToolBox.ChatRobot.Imaging.FunUI.NotificationPage;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI
{
    /// <summary>
    /// MoreFun.xaml 的交互逻辑
    /// </summary>
    public partial class MoreFun : UserControl
    {
        public MoreFun()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示消息窗口
        /// </summary>
        /// <param name="MessageWin"></param>
        private void ShowMessage(Window MessageWin)
        {
            MessageWin.Owner = Application.Current.MainWindow;
            MessageWin.Width = Application.Current.MainWindow.Width;
            MessageWin.Height = Application.Current.MainWindow.Height;
            if (MessageWin.Width == 830 || MessageWin.Height == 556)
            {
                MessageWin.Left = Application.Current.MainWindow.Left;
                MessageWin.Top = Application.Current.MainWindow.Top;
            }
            else
            {
                MessageWin.Left = 0;
                MessageWin.Top = 0;
            }
            MessageWin.ShowInTaskbar = false;
            ClickBtn.Content = MessageWin.ShowDialog() == true ? "确定" : "取消";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage(new ConfirmUpdate());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowMessage(new LabelDetection());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShowMessage(new UpdateOk());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ShowMessage(new DetectionOk());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WindowManager.ShowWinTipMessage("请选择标签");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WindowManager.ShowWinTipMessage("功能开发中，敬请期待~");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Hide();
            Application.Current.MainWindow.ShowInTaskbar = false;
            TrayNotification trayNotification = new TrayNotification();


            trayNotification.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 415;
            trayNotification.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 152;
            trayNotification.Show();
        }
    }
}
