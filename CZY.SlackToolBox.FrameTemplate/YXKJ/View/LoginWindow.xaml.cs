﻿using CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel;
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

    }
}
