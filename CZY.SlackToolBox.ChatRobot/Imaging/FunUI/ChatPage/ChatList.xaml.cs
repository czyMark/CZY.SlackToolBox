using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CZY.SlackToolBox.ChatRobot.Imaging.FunUI.NotificationPage;
using CZY.SlackToolBox.ChatRobot.Imaging.Style;
using Application = System.Windows.Application;
using Timer = System.Timers.Timer;

namespace CZY.SlackToolBox.ChatRobot.Imaging.FunUI
{
    /// <summary>
    /// ChatList.xaml 的交互逻辑
    /// </summary>
    public partial class ChatList : UserControl
    {
        Timer timer = new Timer();
        bool AddNameState = false;

        SolidColorBrush nickNameColor = new SolidColorBrush(Color.FromRgb(253, 52, 89));
        SolidColorBrush textColor = new SolidColorBrush(Color.FromRgb(51, 51, 51));
        public ChatList()
        {
            InitializeComponent();
            timer.Interval = 1500;
            timer.Elapsed += Timer_Elapsed;
            LoadChatInfo();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //记录状态，当不满足要求的1.5秒后显示出来 

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                TipNickname.Visibility = Visibility.Visible;
            }));

            timer.Stop();
        }

        private void LoadChatInfo()
        {
            ChatInfo chatInfo = new ChatInfo();
            chatInfo.AgainNotification += ChatInfo_AgainNotification;
            chatInfo.ContinueNotification += ChatInfo_ContinueNotification;
            ChatHistory.Children.Add(chatInfo);
        }

        private void ChatInfo_ContinueNotification(string NotificationName)
        {
            LoadChatInfo();
            ChatHistoryScroll.ScrollToBottom();
        }

        private void ChatInfo_AgainNotification(string NotificationName)
        {
            LoadChatInfo();
            ChatHistoryScroll.ScrollToBottom();
        }

        private void VerifyInput()
        {
            if (IsRichTextBoxEmpty(ChatInput))
                InputTip.Visibility = Visibility.Visible;
            else
                InputTip.Visibility = Visibility.Collapsed;
        }
        public bool IsRichTextBoxEmpty(RichTextBox rtb)
        {
            if (rtb.Document.Blocks.Count == 0) return true;
            TextPointer startPointer = rtb.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            TextPointer endPointer = rtb.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
            return startPointer.CompareTo(endPointer) == 0;
        }

        private void ChatInput_GotFocus(object sender, RoutedEventArgs e)
        {
            InputTip.Visibility = Visibility.Collapsed;
        }

        private void ChatInput_LostFocus(object sender, RoutedEventArgs e)
        {

            if (IsRichTextBoxEmpty(ChatInput))
                InputTip.Visibility = Visibility.Visible;
            else
                InputTip.Visibility = Visibility.Collapsed;
        }


        private void ChatInput_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (IsRichTextBoxEmpty(ChatInput))
            {
                SendBtn.IsEnabled = true;
                SendBtn.Opacity = 0.1;
            }
            else
            {
                SendBtn.IsEnabled = false;
                SendBtn.Opacity = 1;
            }
        }

        private void ChatInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {


            //ChatInput.SelectAll();
            //ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);

            //ConvertKeyToHyperLink("亲密昵称",nickNameColor);


            //if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift && e.Key == Key.D2)
            //{
            //    ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, nickNameColor);
            //    AddNameState = true;
            //}


            if (IsRichTextBoxEmpty(ChatInput))
            {
                SendBtn.IsEnabled = true;
                SendBtn.Opacity = 0.1;
            }
            else
            {
                SendBtn.IsEnabled = false;
                SendBtn.Opacity = 1;
            }

            //检查删除的是否是亲密昵称。如果是就删除
            //ChatInput.Selection.Start.GetOffsetToPosition(ChatInput.Selection.End)
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                TextPointer start = ChatInput.Selection.Start;


                //var value = start.GetPositionAtOffset(-1);
                //if (value == null)
                //{
                //    ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);
                //    return;
                //}
                //TextRange textRange = new TextRange(value, newtart);
                //if (textRange.Text.Contains("@"))
                //{
                //    AddNameState = false;
                //    ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);
                //    return;
                //}
                var value = start.GetPositionAtOffset(-8);
                if (value == null)
                    return;

                var textRange = new TextRange(value, start);
                if (textRange.Text == "亲密昵称")
                {
                    textRange.Text = "";
                    e.Handled = true;
                }
            }
            //else
            //{
            //    TextPointer start = ChatInput.Selection.Start;
            //    TextPointer newtart = this.ChatInput.CaretPosition;
            //    var value = start.GetPositionAtOffset(-4);
            //    if (value == null)
            //        return;

            //    var textRange = new TextRange(value, newtart);
            //    if (textRange.Text != "亲密昵称")
            //        ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);
            //}
            if (e.Key == Key.Enter)
            {
                //e.Handled = true;
                //RichTextBox textBox = this.ChatInput;
                //TextPointer start = textBox.Selection.Start;
                //var value = start.GetPositionAtOffset(-16);
                //if (value == null)
                //    return;
                //TextPointer newtart = this.ChatInput.CaretPosition;
                //TextRange textRange = new TextRange(value, newtart);
                //if (textRange.Text.Contains("@") && AddNameState == true)
                //{
                //    AddNameState = false; 
                //    ChatInput.CaretPosition = ChatInput.Selection.End; 
                //    e.Handled = true;
                //}
                //ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);
            }
        }

        public int Index = 0;
        public string Start;
        public string Stop;

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IntervalEdit intervalEdit = new IntervalEdit(Index, Start, Stop);
            intervalEdit.Owner = Window.GetWindow(this);
            intervalEdit.Width = Window.GetWindow(this).Width;
            intervalEdit.Height = Window.GetWindow(this).Height;
            if (intervalEdit.Width == 830 || intervalEdit.Height == 556)
            {
                intervalEdit.Left = Window.GetWindow(this).Left;
                intervalEdit.Top = Window.GetWindow(this).Top;
            }
            else
            {
                intervalEdit.Left = 0;
                intervalEdit.Top = 0;
            }
            intervalEdit.ShowInTaskbar = false;
            intervalEdit.Closed += IntervalEdit_Closed;
            intervalEdit.Show();
        }

        private void IntervalEdit_Closed(object sender, EventArgs e)
        {
            IntervalEdit win = sender as IntervalEdit;
            if (!string.IsNullOrEmpty(win.IntervalText))
            {
                Index = win.Index;
                Start = win.Start;
                Stop = win.Stop;
                IntervalTime.Content = win.IntervalText;
            }
            win.Closed -= IntervalEdit_Closed;
            //win.OperationState
        }

        /// <summary>
        /// 插入亲密昵称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNameStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            InputTip.Visibility = Visibility.Collapsed;

            string text = "亲密昵称";

            TextRange tr = new TextRange(ChatInput.Selection.Start, ChatInput.Selection.End);
            tr.Text = text;
            Hyperlink hyperlink = new Hyperlink(tr.Start, tr.End);
            hyperlink.Tag = text;
            hyperlink.FontSize = 12;
            hyperlink.TextDecorations = null;
            hyperlink.Foreground = nickNameColor;


            //ChatInput.CaretPosition.InsertTextInRun(text);
            //ConvertKeyToHyperLink(text, nickNameColor);
            try
            {

                ChatInput.CaretPosition = ChatInput.Selection.End.GetPositionAtOffset(8);
            }
            catch
            {
            }



            //ChatInput.Selection.Text = text;
            //ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);

            //TextPointer tp = ChatInput.CaretPosition.GetPositionAtOffset(text.Length, LogicalDirection.Backward);
            //if (tp != null)
            //{
            //    ChatInput.CaretPosition = tp;
            //}
        }

        List<TextRange> m_TextList;
        /// <summary>
        /// 设置热点词为超链接
        /// </summary>
        /// <param name="key">热点词</param>
        /// <param name="foreground">热点词字体颜色</param>
        public void ConvertKeyToHyperLink(string key, SolidColorBrush foreground)
        {
            m_TextList = new List<TextRange>();
            //设置文字指针为Document初始位置           
            //richBox.Document.FlowDirection           
            TextPointer position = ChatInput.Document.ContentStart;
            while (position != null)
            {
                //向前搜索,需要内容为Text       
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    //拿出Run的Text        
                    string text = position.GetTextInRun(LogicalDirection.Forward);
                    //可能包含多个keyword,做遍历查找           
                    int index = 0;
                    index = text.IndexOf(key, 0);
                    if (index != -1)
                    {
                        TextPointer start = position.GetPositionAtOffset(index);
                        TextPointer end = start.GetPositionAtOffset(key.Length);
                        linke(new TextRange(start, end), key, foreground);
                    }
                    else
                    {
                        TextPointer start = position.GetPositionAtOffset(index);
                        TextPointer end = start.GetPositionAtOffset(key.Length);
                        linke(new TextRange(start, end), text, textColor);
                    }
                }
                //文字指针向前偏移   
                position = position.GetNextContextPosition(LogicalDirection.Forward);
            }
        }

        private void linke(TextRange tp, string key, SolidColorBrush foreground)
        {
            try
            {
                if (key == "亲密昵称")
                {
                    TextRange tr = new TextRange(tp.Start, tp.End);
                    tr.Text = key;
                    tr.ApplyPropertyValue(TextElement.ForegroundProperty, nickNameColor);
                }
                else
                {
                    TextRange tr = new TextRange(tp.Start, tp.End);
                    tr.Text = key;
                    tr.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);
                }
                //Hyperlink hlink = new Hyperlink(tr.Start, tr.End);
                //if (hlink == null)
                //    return;
                //hlink.Tag = key;
                //hlink.TextDecorations = null;
                //hlink.Foreground = foreground;
            }
            catch
            {

            }
        }


        /// <summary>
        /// 插入图片逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendImageStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Text Files (*.jpg; *.png; *.gif)|*.jpg;*.png;*.gif";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                BitmapImage bImg = new BitmapImage();
                bImg.BeginInit();
                bImg.UriSource = new Uri(ofd.FileName, UriKind.RelativeOrAbsolute);
                bImg.EndInit();
                img.Source = bImg;

                //调整图片大小
                if (bImg.Height > 100 || bImg.Width > 100)
                {
                    img.Height = bImg.Height * 0.2;
                    img.Width = bImg.Width * 0.2;
                }
                else
                {
                    img.Height = bImg.Height;
                    img.Width = bImg.Width;
                }
                //图片缩放模式
                //img.Stretch = Stretch.Uniform;
                InlineUIContainer inlineUI = new InlineUIContainer(img, ChatInput.Selection.Start); //插入图片到选定位置

                ChatInput.CaretPosition = inlineUI.ContentEnd;



                ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);
                //AddNameState = false;

                InputTip.Visibility = Visibility.Collapsed;
                SendBtn.IsEnabled = false;
                SendBtn.Opacity = 1;
            }
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //1.5秒后显示
            timer.Start();
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //1.5秒后显示
            timer.Stop();

            TipNickname.Visibility = Visibility.Collapsed;
        }

        private void ChatInput_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //ChatInput.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);
        }
    }
}
