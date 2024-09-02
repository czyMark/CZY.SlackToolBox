using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CZY.SlackToolBox.ChatRobot.Imaging.FunUI.NotificationPage;
using static CZY.SlackToolBox.ChatRobot.Core.WindowManager;

namespace CZY.SlackToolBox.ChatRobot.Imaging.Style
{
    /// <summary>
    /// ChatInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ChatInfo : UserControl
    {
        public event Notification AgainNotification;
        void SendAgainNotification(string NotificationName)
        {
            if (AgainNotification != null)
            {
                AgainNotification(NotificationName);
            }
        }


        public event Notification ContinueNotification;
        void SendContinueNotification(string NotificationName)
        {
            if (ContinueNotification != null)
            {
                ContinueNotification(NotificationName);
            }
        }

        public ChatInfo()
        {
            InitializeComponent();
        }

        private void LabelAgain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AgainNotification("");
            e.Handled = true;
        }

        private void LabelContinue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SendContinueNotification("");
            e.Handled = true;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //查看详情预留的接口， 不知道应该点击哪里进入

            ChatDetailInfo MessageWin = new ChatDetailInfo();
            MessageWin.Owner = Window.GetWindow(this);
            MessageWin.Width = Window.GetWindow(this).Width;
            MessageWin.Height = Window.GetWindow(this).Height;
            MessageWin.Left = Window.GetWindow(this).Left;
            MessageWin.Top = Window.GetWindow(this).Top;
            MessageWin.ShowInTaskbar = false;
            MessageWin.Closed += MessageWin_Closed;
            MessageWin.Show();

        }

        private void MessageWin_Closed(object sender, EventArgs e)
        {
            ChatDetailInfo win = sender as ChatDetailInfo;
            if (win.OperationState == null)
                return;
            if (win.OperationState == false)
                AgainNotification("");
            else
                SendContinueNotification("");
        }

        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            Label label= sender as Label;
            label.Foreground = new SolidColorBrush(Color.FromRgb(244, 78, 42));
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label.Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51));
        }
    }
}
