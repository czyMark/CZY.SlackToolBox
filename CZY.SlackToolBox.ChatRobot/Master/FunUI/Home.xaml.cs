using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CZY.SlackToolBox.ChatRobot.Core;
using CZY.SlackToolBox.ChatRobot.Master.Style;

namespace CZY.SlackToolBox.ChatRobot.Master.FunUI
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

            Recommend.Children.Add(new FileListRowStyle(new FileListRowStyle.FileListModel()
            {
                Title = "于一科技产品的七麦采集",
                Time = "2024年01月09日20:37:02",
                Detail = "用户/桌面/2024-01-09于一科技产品的数据采集"
            }));


            Recommend.Children.Add(new FileListRowStyle(new FileListRowStyle.FileListModel()
            {
                Title = "小宝宝的衣服京东购买最近最低价对比采集",
                Time = "2024年01月09日20:37:02",
                Detail = "用户/桌面/小宝宝的衣服京东购买最近最低价对比采集"
            }));


            Recommend.Children.Add(new FileListRowStyle(new FileListRowStyle.FileListModel()
            {
                Title = "AJ 8淘宝最近最低售价对比采集",
                Time = "2024年01月09日20:37:02",
                Detail = "用户/桌面/AJ 8淘宝最近最低售价对比"
            }));


            //测试添加数据。
            for (int i = 0; i < 7; i++)
            {
                Recommend.Children.Add(new FileListRowStyle(new FileListRowStyle.FileListModel()
                {
                    Title = "AJ 8淘宝最近最低售价对比采集",
                    Time = "2024年01月09日20:37:02",
                    Detail = $"用户/桌面/AJ 8淘宝最近最低售价对比{i}"
                }));
            }
        }

        private void MorePanel_MouseEnter(object sender, MouseEventArgs e)
        {
            arrow.Visibility = Visibility.Visible;
            arrown.Visibility = Visibility.Collapsed;
            MorePanelTitle.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#1A1A1A"));
        }

        private void MorePanel_MouseLeave(object sender, MouseEventArgs e)
        {
            arrow.Visibility = Visibility.Collapsed;
            arrown.Visibility = Visibility.Visible;
            MorePanelTitle.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#666666"));
        }


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowManager.GoToPage("HistoryFile");
        }

        private void MorePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            WindowManager.GoToPage("CreateFlow");
        }
    }
}
