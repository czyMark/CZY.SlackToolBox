using System.Windows;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI.NotificationPage
{
    /// <summary>
    /// LabelDetection.xaml 的交互逻辑
    /// </summary>
    public partial class LabelDetection : Window
    {
        public LabelDetection()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Confirm_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
