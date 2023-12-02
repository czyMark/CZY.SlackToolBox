using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.Input
{
    /// <summary>
    /// TextInfoBox.xaml 的交互逻辑
    /// </summary>
    public partial class TextInfoBox : UserControl
    {
        public TextInfoBox()
        {
            InitializeComponent();
            TextValueWidth = 240;
            this.IsImportance = false;
            InputControl.DataContext = this;
        }
        #region TextDescription
        public string TextDescription
        {
            get { return (string)GetValue(TextDescriptionProperty); }
            set { SetValue(TextDescriptionProperty, value); }
        }

        public static readonly DependencyProperty TextDescriptionProperty = DependencyProperty.Register(
         "TextDescription",
         typeof(string),
         typeof(TextInfoBox), new PropertyMetadata(TextDescriptionPropertyChanged));
        private static void TextDescriptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextInfoBox up = d as TextInfoBox;
                up.DescriptionText.Text = (string)e.NewValue;
            }
        }
        #endregion

        #region TextValue
        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register(
         "TextValue",
         typeof(string),
         typeof(TextInfoBox), new PropertyMetadata(TextValuePropertyChanged));
        private static void TextValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextInfoBox up = d as TextInfoBox; 
                InputAttach.CancelTextBoxPrompt(up.InputValue);
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
         typeof(TextInfoBox), new PropertyMetadata(TextValueWidthPropertyChanged));
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
         typeof(TextInfoBox), new PropertyMetadata(TextPromptPropertyChanged));
        private static void TextPromptPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextInfoBox up = d as TextInfoBox;
                up.TextValue = (string)e.NewValue;
                InputAttach.RefreshTextBoxPrompt(up.InputValue, (string)e.NewValue);
            }
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
         typeof(TextInfoBox), new PropertyMetadata(IsImportancePropertyChanged));
        private static void IsImportancePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                bool temp = (bool)e.NewValue;
                TextInfoBox up = d as TextInfoBox;
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

    }
}
