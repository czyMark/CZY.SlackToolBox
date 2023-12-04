using CZY.SlackToolBox.LuckyControl.IconResource;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CZY.SlackToolBox.LuckyControl.Input
{
    /// <summary>
    /// TextPasswordBox.xaml 的交互逻辑
    /// </summary>
    public partial class TextPasswordBox : UserControl
    {
        public TextPasswordBox()
        {
            InitializeComponent();
            InputControl.DataContext = this;
            IsImportance = true;
            TextValueWidth = 240;
            eyeBtn.AddHandler(Button.MouseDownEvent, new RoutedEventHandler(eyeBtn_Down), true);
            eyeBtn.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(eyeBtn_Up), true);
        }

        #region PasswordText
        public string PasswordText
        {
            get { return (string)GetValue(PasswordTextProperty); }
            set { SetValue(PasswordTextProperty, value); }
        }

        public static readonly DependencyProperty PasswordTextProperty = DependencyProperty.Register(
         "PasswordText",
         typeof(string),
         typeof(TextPasswordBox), new PropertyMetadata(PasswordTextPropertyChanged));
        private static void PasswordTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region TextDescription
        public string TextDescription
        {
            get { return (string)GetValue(TextDescriptionProperty); }
            set { SetValue(TextDescriptionProperty, value); }
        }

        public static readonly DependencyProperty TextDescriptionProperty = DependencyProperty.Register(
         "TextDescription",
         typeof(string),
         typeof(TextPasswordBox), new PropertyMetadata(TextDescriptionPropertyChanged));
        private static void TextDescriptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextPasswordBox up = d as TextPasswordBox;
                up.DescriptionText.Text = (string)e.NewValue;
            }
        }
        #endregion
          

        #region TextValueWidth
        public double TextValueWidth
        {
            get { return (double)GetValue(TextValueWidthProperty); }
            set { SetValue(TextValueWidthProperty, value); }
        }

        public static readonly DependencyProperty TextValueWidthProperty = DependencyProperty.Register(
         "TextValueWidth",
         typeof(double),
         typeof(TextPasswordBox), new PropertyMetadata(TextValueWidthPropertyChanged));
        private static void TextValueWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion
         

        #region TextPrompt
        public string TextPrompt
        {
            get { return (string)GetValue(TextPromptProperty); }
            set { SetValue(TextPromptProperty, value); }
        }

        public static readonly DependencyProperty TextPromptProperty = DependencyProperty.Register(
         "TextPrompt",
         typeof(string),
         typeof(TextPasswordBox), new PropertyMetadata(TextPromptPropertyChanged));
        private static void TextPromptPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             
        }
        #endregion

        #region IsImportance
        public bool IsImportance
        {
            get { return (bool)GetValue(IsImportanceProperty); }
            set { SetValue(IsImportanceProperty, value); }
        }

        public static readonly DependencyProperty IsImportanceProperty = DependencyProperty.Register(
         "IsImportance",
         typeof(bool),
         typeof(TextPasswordBox), new PropertyMetadata(IsImportancePropertyChanged));
        private static void IsImportancePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                bool temp = (bool)e.NewValue;
                TextPasswordBox up = d as TextPasswordBox;
                if (temp)
                {
                    up.DescriptionText.Foreground = Brushes.Red;
                }
                else
                {
                    up.DescriptionText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6F7174"));
                }
            }
        }
        #endregion

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
