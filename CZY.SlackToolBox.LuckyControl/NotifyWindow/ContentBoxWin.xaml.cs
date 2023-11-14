using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.NotifyWindow
{


    /// <summary>
    /// ContentBoxWin.xaml 的交互逻辑
    /// </summary>
    public partial class ContentBoxWin : Window
    {
        public enum ContentBoxWinState { YesNo, Yes, No }
        public enum ContentBoxState { Normal, Success, Warn, Danegr }

        private ContentBoxWinState winState;
        private ContentBoxState state;

        private static ContentBoxWin msgWin;
        private ContentBoxWin(string yesStr, string noStr, ContentBoxWinState messageBoxWinState, string title)
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
                    if (winState == ContentBoxWinState.Yes || winState == ContentBoxWinState.YesNo)
                        btnOk_Click(null, null);
                    break;
                case Key.F2:
                    if (winState == ContentBoxWinState.No || winState == ContentBoxWinState.YesNo)
                        btnNo_Click(null, null);
                    break;
            }
        }


        private static void InitPageState(ContentBoxState state)
        {
            switch (state)
            {
                case ContentBoxState.Normal:
                    msgWin.mainBorder.BorderBrush = (Brush)msgWin.FindResource("infoColorBorderBrush");
                    msgWin.topBorder.Background = (Brush)msgWin.FindResource("infoColorBackground");
                    msgWin.bottomBorder.BorderBrush = (Brush)msgWin.FindResource("infoColorBorderBrush");
                    break;
                case ContentBoxState.Success:
                    msgWin.mainBorder.BorderBrush = (Brush)msgWin.FindResource("successBorderBrush");
                    msgWin.topBorder.Background = (Brush)msgWin.FindResource("successBackground");
                    msgWin.bottomBorder.BorderBrush = (Brush)msgWin.FindResource("successBorderBrush"); break;
                case ContentBoxState.Warn:
                    msgWin.mainBorder.BorderBrush = (Brush)msgWin.FindResource("warningBorderBrush");
                    msgWin.topBorder.BorderBrush = (Brush)msgWin.FindResource("warningBackground");
                    msgWin.bottomBorder.BorderBrush = (Brush)msgWin.FindResource("warningBorderBrush"); break;
                case ContentBoxState.Danegr:
                    msgWin.mainBorder.BorderBrush = (Brush)msgWin.FindResource("dangerBorderBrush");
                    msgWin.topBorder.Background = (Brush)msgWin.FindResource("dangerBackground");
                    msgWin.bottomBorder.BorderBrush = (Brush)msgWin.FindResource("dangerBorderBrush"); break;
            }
        }
        private static void InitButtonGroup(ContentBoxWinState winState)
        {
            switch (winState)
            {
                case ContentBoxWinState.YesNo:
                    break;
                case ContentBoxWinState.Yes:
                    msgWin.btnNo.Visibility = Visibility.Collapsed;
                    break;
                case ContentBoxWinState.No:
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
            this.DialogResult = false;
            this.Close();
        }

        public void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();

        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


        public static ContentBoxWin GetContentBoxWin(FrameworkElement content, string title = "查看",
            ContentBoxWinState winState = ContentBoxWinState.Yes,
            string yesText = "确认", string noText = "取消",
            ContentBoxState state = ContentBoxState.Normal
            )
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                msgWin = new ContentBoxWin(yesText, noText, winState, title);
                msgWin.Width = WinWidth;
                msgWin.Height = WinHeight;
                InitButtonGroup(winState);
                InitPageState(state);
                msgWin.lblTile.Content = title;
                msgWin.boxContent.Content = content;
                msgWin.btnOk.Focus();
            });
            return msgWin;
        }

        public static void Show(FrameworkElement content, string title = "查看",
            ContentBoxWinState winState = ContentBoxWinState.Yes,
            string yesText = "确认", string noText = "取消",
            ContentBoxState state = ContentBoxState.Normal
            )
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                msgWin = new ContentBoxWin(yesText, noText, winState, title);
                msgWin.Width = WinWidth;
                msgWin.Height = WinHeight;
                InitButtonGroup(winState);
                InitPageState(state);
                msgWin.lblTile.Content = title;
                msgWin.boxContent.Content = content;
                msgWin.btnOk.Focus();
                msgWin.ShowDialog();
            });
        }


        public static double WinWidth { get; set; } = 1000;
        public static double WinHeight { get; set; } = 800;
    }
}
