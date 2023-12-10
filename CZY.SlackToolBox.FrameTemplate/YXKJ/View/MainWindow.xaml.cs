using CZY.SlackToolBox.AnimationBank.Other;
using CZY.SlackToolBox.LuckyControl.ElementPanel;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DataV = CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.View
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

            this.DataContext = new DataV.MainWindowViewModel();

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
            navTitleLabel.Content = NavPath;
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
            Type type = assembly.GetType("CZY.SlackToolBox.FrameTemplate.YXKJ.View.HomeContent");
            MainContentControl.Content = Activator.CreateInstance(type);
            navTitleLabel.Content = "首页";
        }
    }
}
