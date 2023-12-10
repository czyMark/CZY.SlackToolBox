using CZY.SlackToolBox.FrameTemplate.ChartTool.ViewModel;
using CZY.SlackToolBox.LuckyControl.IconResource;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CZY.SlackToolBox.FrameTemplate.ChartTool.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginWindowViewModel();
            eyeBtn.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(eyeBtn_Down), true);
            eyeBtn.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(eyeBtn_Up), true);
        }

        private void eyeBtn_Down(object sender, RoutedEventArgs e)
        {
            //显示文本
            Button btn = sender as Button;
            promptBox.Text = pwdBox.Password;
            if (string.IsNullOrEmpty(promptBox.Text))
            { 
                return;
            }

            promptBox.Visibility = Visibility.Visible;
            pwdBox.Visibility = Visibility.Collapsed;
            IconFont icon = new IconFont();
            icon.IconName = "PasswordEye";
            btn.Content = icon;
            btn.Tag = 1;
        }
        private void eyeBtn_Up(object sender, RoutedEventArgs e)
        {
            //隐藏文本
            Button btn = sender as Button;
            if (string.IsNullOrEmpty(pwdBox.Password))
            {
                return;
            }
            promptBox.Text = string.Empty;
            promptBox.Visibility = Visibility.Collapsed;
            pwdBox.Visibility = Visibility.Visible;
            IconFont icon = new IconFont();
            icon.IconName = "PasswordEyeHiden";
            btn.Content = icon;
            btn.Tag = 0;

        }




        private void pwdBox_KeyDown(object sender, KeyEventArgs e)
        {
            promptBox.Text = String.Empty;
            promptBox.Visibility = Visibility.Collapsed;
        }


        private void pwdBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(pwdBox.Password))
            {
                promptBox.Text = String.Empty;
                promptBox.Visibility = Visibility.Visible;
            }
            else
            {
                promptBox.Text = String.Empty;
                promptBox.Visibility = Visibility.Collapsed;
            }
        }
         
         

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            this.WindowState = WindowState.Normal;
            this.DragMove();
        }
    }
}
