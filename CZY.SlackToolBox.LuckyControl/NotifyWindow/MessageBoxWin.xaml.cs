using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.NotifyWindow
{


    /// <summary>
    /// MessageBoxWin.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxWin : Window
    {
        public enum MessageBoxWinState { YesNo, Yes, No }
        public enum MessageBoxState { Normal, Success, Warn, Danegr }

        private MessageBoxWinState winState;
        private MessageBoxState state;

        private static MessageBoxWin msgWin;
        private MessageBoxWin(string yesStr, string noStr, MessageBoxWinState messageBoxWinState, string title)
        {
            InitializeComponent();
            this.winState = messageBoxWinState;
            this.Title = title;
            this.btnOk.Content = yesStr;
            this.btnNo.Content = noStr;
            this.ShowInTaskbar = false;
            this.KeyDown += MessageBoxControl_KeyDown;
        }

        private void MessageBoxControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    if (winState == MessageBoxWinState.Yes || winState == MessageBoxWinState.YesNo)
                        btnOk_Click(null, null);
                    break;
                case Key.F2:
                    if (winState == MessageBoxWinState.No || winState == MessageBoxWinState.YesNo)
                        btnNo_Click(null, null);
                    break;
            }
        }


        private static void InitPageState(MessageBoxState state)
        {
            switch (state)
            {
                case MessageBoxState.Normal:
                    msgWin.mainBorder.BorderBrush = (Brush)msgWin.FindResource("infoColorBorderBrush");
                    msgWin.topBorder.Background = (Brush)msgWin.FindResource("infoColorBackground");
                    msgWin.bottomBorder.BorderBrush = (Brush)msgWin.FindResource("infoColorBorderBrush");
                    break;
                case MessageBoxState.Success:
                    msgWin.mainBorder.BorderBrush = (Brush)msgWin.FindResource("successBorderBrush");
                    msgWin.topBorder.Background = (Brush)msgWin.FindResource("successBackground");
                    msgWin.bottomBorder.BorderBrush = (Brush)msgWin.FindResource("successBorderBrush"); break;
                case MessageBoxState.Warn:
                    msgWin.mainBorder.BorderBrush = (Brush)msgWin.FindResource("warningBorderBrush");
                    msgWin.topBorder.BorderBrush = (Brush)msgWin.FindResource("warningBackground");
                    msgWin.bottomBorder.BorderBrush = (Brush)msgWin.FindResource("warningBorderBrush"); break;
                case MessageBoxState.Danegr:
                    msgWin.mainBorder.BorderBrush = (Brush)msgWin.FindResource("dangerBorderBrush");
                    msgWin.topBorder.Background = (Brush)msgWin.FindResource("dangerBackground");
                    msgWin.bottomBorder.BorderBrush = (Brush)msgWin.FindResource("dangerBorderBrush"); break;
            }
        }
        private static void InitButtonGroup(MessageBoxWinState winState)
        {
            switch (winState)
            {
                case MessageBoxWinState.YesNo:
                    break;
                case MessageBoxWinState.Yes:
                    msgWin.btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxWinState.No:
                    msgWin.btnOk.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }
        private void lblTile_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { this.DragMove(); }
        }

        private void closeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var isDialog = typeof(Window).GetField("_showingAsDialog", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (isDialog != null && (bool)isDialog.GetValue(this)) 
                this.DialogResult = false;
            this.Close();
        }

        public void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var isDialog = typeof(Window).GetField("_showingAsDialog", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (isDialog != null && (bool)isDialog.GetValue(this))
                this.DialogResult = true;
            this.Close();

        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            var isDialog = typeof(Window).GetField("_showingAsDialog", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (isDialog != null && (bool)isDialog.GetValue(this))
                this.DialogResult = false;
            this.Close();
        }


        public static MessageBoxWin GetMessageBoxWin(string content, string title = "系统通知",
            MessageBoxWinState winState = MessageBoxWinState.Yes,
            string yesText = "确认", string noText = "取消",
            MessageBoxState state = MessageBoxState.Normal
            )
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                msgWin = new MessageBoxWin(yesText, noText, winState, title);
                msgWin.Width = WinWidth;
                msgWin.Height = WinHeight;
                InitButtonGroup(winState);
                InitPageState(state);
                msgWin.lblTile.Content = title;
                msgWin.txtContent.Inlines.Add(content);
                msgWin.btnOk.Focus();
            });
            return msgWin;
        }

        public static void Show(string content, string title = "系统通知",
            MessageBoxWinState winState = MessageBoxWinState.Yes,
            string yesText = "确认", string noText = "取消",
            MessageBoxState state = MessageBoxState.Normal
            )
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                msgWin = new MessageBoxWin(yesText, noText, winState, title);
                msgWin.Width = WinWidth;
                msgWin.Height = WinHeight;
                InitButtonGroup(winState);
                InitPageState(state);
                msgWin.lblTile.Content = title;
                msgWin.txtContent.Inlines.Add(content);
                msgWin.btnOk.Focus();
                msgWin.ShowDialog();
            });
        }


        public static double WinWidth { get; set; } = 600;
        public static double WinHeight { get; set; } = 500;
    }
}
