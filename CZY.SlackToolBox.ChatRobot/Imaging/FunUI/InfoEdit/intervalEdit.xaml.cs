using System.Windows;
using System.Windows.Controls;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI.NotificationPage
{
    /// <summary>
    /// IntervalEdit.xaml 的交互逻辑
    /// </summary>
    public partial class IntervalEdit : Window
    {
        public bool? OperationState { get; set; }

        public string IntervalText { get; set; }
        
        public int Index { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }

        public IntervalEdit()
        {
            InitializeComponent();
        }

        public IntervalEdit(int index,string Start,string Stop)
        {
            InitializeComponent();

            IntervalTime.SelectedIndex= index;
            InputSValue.Text = Start;
            InputEValue.Text = Stop;
        }

        private void Cancel_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OperationState = false;
            this.Close();
        }

        private void Confirm_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OperationState = true;

            Index = IntervalTime.SelectedIndex;
            Start = InputSValue.Text;
            Stop = InputEValue.Text;


            if (IntervalTime.SelectedIndex == 0)
            {
                IntervalText = $"间隔{InputSValue.Text}~{InputEValue.Text}秒";
            }
            else
            {
                switch (IntervalTime.SelectedIndex)
                {
                    case 1: IntervalText = $"间隔1~4秒"; break;
                    case 2: IntervalText = $"间隔2~6秒"; break;
                    case 3: IntervalText = $"间隔3~8秒"; break;
                    case 4: IntervalText = $"间隔4~10秒"; break;
                    case 5: IntervalText = $"间隔5~12秒"; break;
                    default:
                        break;
                }
            }

            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OperationState = false;
            this.Close();
        }

        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IntervalTimePanel == null)
                return;

            //只要不是0 就不显示 设置时间的 panel
            if (IntervalTime.SelectedIndex == 0)
            {
                IntervalTimePanel.Visibility = Visibility.Visible;
            }
            else
            {
                IntervalTimePanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
