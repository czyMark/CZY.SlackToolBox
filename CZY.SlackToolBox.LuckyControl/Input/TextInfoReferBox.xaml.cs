using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CZY.SlackToolBox.LuckyControl.Input
{
    /// <summary>
    /// TextInfoReferBox.xaml 的交互逻辑
    /// </summary>
    public partial class TextInfoReferBox : UserControl
    {
        public TextInfoReferBox()
        {
            InitializeComponent();
            TextValueWidth = 240;
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
         typeof(TextInfoReferBox), new PropertyMetadata(TextDescriptionPropertyChanged));
        private static void TextDescriptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextInfoReferBox up = d as TextInfoReferBox;
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
         typeof(TextInfoReferBox), new PropertyMetadata(TextValuePropertyChanged));
        private static void TextValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

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
         typeof(TextInfoReferBox), new PropertyMetadata(TextValueWidthPropertyChanged));
        private static void TextValueWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

         #region Command
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
         "Command",
         typeof(ICommand),
         typeof(TextInfoReferBox), new PropertyMetadata(CommandPropertyChanged));
        private static void CommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             
        }
        #endregion
         
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Command.Execute(TextValue);
        }
    }
}
