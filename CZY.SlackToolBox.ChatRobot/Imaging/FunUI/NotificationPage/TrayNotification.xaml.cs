using System.Windows;
using System.Windows.Input;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI.NotificationPage
{
    /// <summary>
    /// TrayNotification.xaml 的交互逻辑
    /// </summary>
    public partial class TrayNotification : Window
    {
        public TrayNotification()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();   
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.ShowInTaskbar = true;
            this.Close();
        }
    }
}
