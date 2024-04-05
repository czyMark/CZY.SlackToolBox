using CZY.SlackToolBox.AnimationBank.Other;
using CZY.SlackToolBox.FrameTemplate.AirportCS.Core;
using CZY.SlackToolBox.FrameTemplate.AirportCS.ViewModel;
using CZY.SlackToolBox.LuckyControl.ElementPanel;
using CZY.SlackToolBox.LuckyControl.IconResource;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using DataV = CZY.SlackToolBox.FrameTemplate.AirportCS.ViewModel;

namespace CZY.SlackToolBox.FrameTemplate.AirportCS.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //默认显示主页面
            StackPanel_MouseDown(null, null);

            PersonalCenter personalCenter = new PersonalCenter();
            personalCenter.selectedFuntion += PersonalCenter_selectedFuntion;
            PersonContentPanel.TipContent = personalCenter;

            //AgentMattersControl
            //绑定相关内容
            this.DataContext = new DataV.MainWindowViewModel();
            eyeBtn.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(eyeBtn_Down), true);
            eyeBtn.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(eyeBtn_Up), true);

        }
        /// <summary>
        /// 信息通知
        /// </summary>
        /// <param name="tipText"></param>
        /// <param name="panelState"></param>
        public void MessageTip(string tipText, CZY.SlackToolBox.LuckyControl.ElementPanel.TipPanel.TipPanelState panelState)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                TipPanel tipPanel = new TipPanel();
                tipPanel.PanelState = CZY.SlackToolBox.LuckyControl.ElementPanel.TipPanel.TipPanelState.Success;
                tipPanel.TipText = tipText;
                WindowTip.AddTipPanel(tipPanel);
            }));
        }
        /// <summary>
        /// 信息窗体通知
        /// </summary>
        /// <param name="notityTitle"></param>
        /// <param name="notityContent"></param>
        /// <param name="panelState"></param>
        public void MessageNotity(string notityTitle, string notityContent, CZY.SlackToolBox.LuckyControl.ElementPanel.NotityPanel.NotityPanelState panelState)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                NotityPanel notityPanel = new NotityPanel();
                notityPanel.Foreground = new SolidColorBrush(Colors.White);
                notityPanel.FontSize = 18;
                notityPanel.Width = 240;
                notityPanel.Height = 150;
                notityPanel.NotityTitle = notityTitle;
                notityPanel.NotityContent = new TextBlock() { Text = notityContent, FontSize = 16 };
                notityPanel.NotityState = panelState;
                WindowNotify.AddNotityPanel(notityPanel);
            }));
        }



        private void ExpanderMenu_SelectedIndexChanged(string ID, string NavPath, string NameSpaceName, string ContentName)
        {
            Assembly assembly = Assembly.Load("CZY.SlackToolBox.FrameTemplate");
            Type type = assembly.GetType("CZY.SlackToolBox.FrameTemplate.YXKJ.View.DataListContent");
            MainContentControl.Content = Activator.CreateInstance(type);
        }

        private void PersonalCenter_ButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (PersonContentPanel.Visibility == Visibility.Collapsed || PersonContentPanel.Visibility == Visibility.Hidden)
                PersonContentPanel.ShowMe();
            else
                PersonContentPanel.HideMe();
            e.Handled = true;
        }

        private void PersonalCenter_selectedFuntion(PersonalCenter.PersonalFunction personalFunction)
        {
            PersonContentPanel.HideMe();
            switch (personalFunction)
            {
                case PersonalCenter.PersonalFunction.PersonalCenter:
                    //切换到个人中心

                    break;
                case PersonalCenter.PersonalFunction.LockScreen:
                    //挂机锁屏
                    pwdBox.Password = string.Empty;
                    whiteCloudsLock.Visibility = Visibility.Visible;
                    break;
                case PersonalCenter.PersonalFunction.EditPwd:

                    //弹出修改密码
                    break;
                case PersonalCenter.PersonalFunction.ExitLogin:
                    //退出用户登录
                    break;

                case PersonalCenter.PersonalFunction.ExitSys:
                    //系统退出
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (PersonContentPanel.Visibility == Visibility.Visible)
                PersonContentPanel.HideMe();
        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Assembly assembly = Assembly.Load("CZY.SlackToolBox.FrameTemplate");
            Type type = assembly.GetType("CZY.SlackToolBox.FrameTemplate.AirportCS.View.HomeContent");
            MainContentControl.Content = Activator.CreateInstance(type);
        }

        int lockNum = 3;
        /// <summary>
        /// 验证锁屏密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (pwdBox.Password == UserCache.LockScreenPwd)
            {
                whiteCloudsLock.Visibility ^= Visibility.Collapsed;
                LockBtn.IsChecked = false;
            }
            else
            {
                //提示密码错误
                MessageTip($"锁屏密码错误!还有{lockNum}次机会。",TipPanel.TipPanelState.Warn);
                lockNum--;
                if (lockNum == -1)
                {
                    UserCache.ExitLogin.Execute(null);
                }
            }
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
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            whiteCloudsLock.Visibility ^= Visibility.Collapsed;
        }
    }
}
