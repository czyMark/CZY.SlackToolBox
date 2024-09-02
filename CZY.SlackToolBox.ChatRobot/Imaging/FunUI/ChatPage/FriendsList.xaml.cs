using System.Windows;
using System.Windows.Controls;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI
{
    /// <summary>
    /// FriendsList.xaml 的交互逻辑
    /// </summary>
    public partial class FriendsList : UserControl
    {
        public FriendsList()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TagCheck.Visibility = Visibility.Collapsed;
            TagUpdate.Visibility = Visibility.Visible;
            NoFriendsTip.Visibility = Visibility.Collapsed;
            TagList.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            TagCheck.Visibility = Visibility.Collapsed;
            TagUpdate.Visibility = Visibility.Visible;
            NoFriendsTip.Visibility = Visibility.Collapsed;
            TagList.Visibility = Visibility.Visible;

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox ck = sender as CheckBox;

            A1.IsChecked = ck.IsChecked; A2.IsChecked = ck.IsChecked;
            A3.IsChecked = ck.IsChecked; A6.IsChecked = ck.IsChecked;
            A4.IsChecked = ck.IsChecked; A7.IsChecked = ck.IsChecked;
            A5.IsChecked = ck.IsChecked; A8.IsChecked = ck.IsChecked;

        }

        private void A1_Checked(object sender, RoutedEventArgs e)
        {

            CheckBox ck = sender as CheckBox;
            if (ck.IsChecked == false)
            {
                A.IsChecked = false;
            }
        }

        private void A_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox ck = sender as CheckBox;

            A1.IsChecked = ck.IsChecked; A2.IsChecked = ck.IsChecked;
            A3.IsChecked = ck.IsChecked; A6.IsChecked = ck.IsChecked;
            A4.IsChecked = ck.IsChecked; A7.IsChecked = ck.IsChecked;
            A5.IsChecked = ck.IsChecked; A8.IsChecked = ck.IsChecked;
        }
    }
}
