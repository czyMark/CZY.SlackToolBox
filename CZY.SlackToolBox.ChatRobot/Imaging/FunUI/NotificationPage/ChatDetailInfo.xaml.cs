using System.Windows;
using System.Windows.Input;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI.NotificationPage
{
    /// <summary>
    /// ChatDetailInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ChatDetailInfo : Window
    {
        public bool? OperationState { get; set; }
        public ChatDetailInfo()
        {
            InitializeComponent(); 
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OperationState = null;
            this.Close();
        }

        private void BtnAgain_MouseDown(object sender, RoutedEventArgs e)
        {
            OperationState=false;
            this.Close();
        }

        private void BtnContinue_MouseDown(object sender, RoutedEventArgs e)
        {
            OperationState = false;
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OperationState = null;
            this.Close();
        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled= true;
        }

        private void RichTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
