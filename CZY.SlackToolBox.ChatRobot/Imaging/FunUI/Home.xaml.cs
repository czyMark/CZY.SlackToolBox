using System.Windows.Controls;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

            FriendsControl.Content = new FriendsList();
            ChatControl.Content = new ChatList();
        }
    }
}
