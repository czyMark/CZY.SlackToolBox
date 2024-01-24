using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI.NotificationPage
{
    /// <summary>
    /// NicknameTip.xaml 的交互逻辑
    /// </summary>
    public partial class NicknameTip : UserControl
    {
        public NicknameTip()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }
    }
}
