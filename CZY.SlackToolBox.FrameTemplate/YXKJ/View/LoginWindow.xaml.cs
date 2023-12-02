using CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel;
using CZY.SlackToolBox.LuckyControl.IconResource;
using CZY.SlackToolBox.LuckyControl.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ
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
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            promptBox.Text = pwdBox.Password;
            if (string.IsNullOrEmpty(promptBox.Text))
            {
                return;
            }
            if (btn.Tag.ToString() == "0")//当前密码是不显示的状态
            {
                promptBox.Visibility = Visibility.Visible;
                pwdBox.Visibility = Visibility.Collapsed;
                IconFont icon= new IconFont();
                icon.IconName = "PasswordEye";
                btn.Content = icon;
                btn.Tag = 1;
            }
            else
            {
                promptBox.Visibility = Visibility.Collapsed;
                pwdBox.Visibility= Visibility.Visible;
                IconFont icon = new IconFont();
                icon.IconName = "PasswordEyeHiden";
                btn.Content = icon;
                btn.Tag = 0;
            }
        }

        private void promptBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //隐藏提示文本
            promptBox.Visibility = Visibility.Collapsed;
            pwdBox.Visibility = Visibility.Visible;
            pwdBox.Focus();
        }

        private void pwdBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //显示提示文本
            if (string.IsNullOrEmpty(pwdBox.Password))
            {
                promptBox.Text =String.Empty;
                InputAttach.RefreshTextBoxPrompt(promptBox,"密码");
                promptBox.Visibility = Visibility.Visible;
            }
        }
    }
}
