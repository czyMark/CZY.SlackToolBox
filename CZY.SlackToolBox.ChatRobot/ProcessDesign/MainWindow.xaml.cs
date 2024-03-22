
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CZY.SlackToolBox.ChatRobot.Core;
using CZY.SlackToolBox.ChatRobot.ProcessDesign.DevControl; 

namespace CZY.SlackToolBox.ChatRobot.ProcessDesign
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            WindowManager.GoToAnsyPage += WindowManager_GoToAnsyPage;
            WindowManager.WinTipMessage += WindowManager_WinTipMessage;
        }

        private void WindowManager_WinTipMessage(string TipText)
        {
            ShowError(TipText);
        }

        private void WindowManager_GoToAnsyPage(string PageName)
        {
            ChangePage(PageName);
        }

        private void ChangePage(string PageName)
        {
            switch (PageName)
            {
                case "Info":
                     
                    //MainControl.Content = new Home();
                    break;
                case "Mark":
                    
                    //MainControl.Content = new MoreFun();
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
            var raadio = sender as RadioButton;
            ChangePage(raadio.Tag.ToString());
        }

        #endregion

        #region 信息提示

        public void ShowError(string ErrorText)
        {
            TipPanel tipPanel = new TipPanel();
            tipPanel.TipText = ErrorText;
            TipControl.Content = tipPanel;
        }

        #endregion

    }
}
