 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CZY.SlackToolBox.ChatRobot.Core;
using CZY.SlackToolBox.ChatRobot.Master.FunUI;

namespace CZY.SlackToolBox.ChatRobot.Master
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            this.HomeNavFun.IsChecked=true;
            WindowManager.GoToAnsyPage += WindowManager_GoToAnsyPage;
        }

        private void WindowManager_GoToAnsyPage(string PageName)
        {
            switch (PageName)
            {
                case "Home":
                    HomeNavFun.IsChecked=true;  
                    MainUIControl.Content = new Home(); 
                    break;
                case "CreateFlow":
                    CreateNavFun .IsChecked = true;
                    MainUIControl.Content = new CreateFlow();
                    break;
                case "HistoryFile":
                    NowNavFun.IsChecked = true;
                    MainUIControl.Content = new HistoryFile();
                    break;
            }
        }


        #region 窗口功能

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void Label_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region 菜单

        private void MainNavFun_Checked(object sender, RoutedEventArgs e)
        {
            if (MainUIControl == null)
                return;
            var raadio = sender as RadioButton;
            switch (raadio.Tag.ToString())
            {
                case "Home": MainUIControl.Content = new Home(); break;
                case "Create": MainUIControl.Content = new CreateFlow(); break;
                case "Now": MainUIControl.Content = new HistoryFile(); break;
            }
        } 

        #endregion
 
        private void FastNav_MouseEnter(object sender, MouseEventArgs e)
        {
            var lable = sender as Label;
            lable.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#D8D8D8"));
        }

        private void FastNav_MouseLeave(object sender, MouseEventArgs e)
        {
            var lable = sender as Label;
            lable.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#F7F7F9"));
        }
         
    }
}
