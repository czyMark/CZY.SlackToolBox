using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CZY.SlackToolBox.LuckyControl.Input
{
    /// <summary>
    /// TextInfoComBox.xaml 的交互逻辑
    /// </summary>
    public partial class TextInfoComBox : UserControl
    {
        public TextInfoComBox()
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
         typeof(TextInfoComBox), new PropertyMetadata(TextDescriptionPropertyChanged));
        private static void TextDescriptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextInfoComBox up = d as TextInfoComBox;
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
         typeof(TextInfoComBox), new PropertyMetadata(TextValuePropertyChanged));
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
         typeof(TextInfoComBox), new PropertyMetadata(TextValueWidthPropertyChanged));
        private static void TextValueWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion
        #region TextDataSource
        [Bindable(true)]
        public IEnumerable TextDataSource
        {
            get { return (IEnumerable)GetValue(TextDataSourceProperty); }
            set { SetValue(TextDataSourceProperty, value); }
        }

        public static readonly DependencyProperty TextDataSourceProperty = DependencyProperty.Register(
         "TextDataSource",
         typeof(IEnumerable),
         typeof(TextInfoComBox), new PropertyMetadata(TextDataSourcePropertyChanged));
        private static void TextDataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             
        }
        #endregion



        #region TextDataSourceValueMember 
        public string TextDataSourceValueMember
        {
            get { return (string)GetValue(TextDataSourceValueMemberProperty); }
            set { SetValue(TextDataSourceValueMemberProperty, value); }
        }

        public static readonly DependencyProperty TextDataSourceValueMemberProperty = DependencyProperty.Register(
         "TextDataSourceValueMember",
         typeof(string),
         typeof(TextInfoComBox), new PropertyMetadata(TextDataSourceValueMemberPropertyChanged));
        private static void TextDataSourceValueMemberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextInfoComBox up = d as TextInfoComBox;
           
                up.InputValue.SelectedValuePath = up.TextDataSourceValueMember;
            }
        }
        #endregion



        #region TextDataSourceDisplayMember 
        public string TextDataSourceDisplayMember
        {
            get { return (string)GetValue(TextDataSourceDisplayMemberProperty); }
            set { SetValue(TextDataSourceDisplayMemberProperty, value); }
        }

        public static readonly DependencyProperty TextDataSourceDisplayMemberProperty = DependencyProperty.Register(
         "TextDataSourceDisplayMember",
         typeof(string),
         typeof(TextInfoComBox), new PropertyMetadata(TextDataSourceDisplayMemberPropertyChanged));
        private static void TextDataSourceDisplayMemberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TextInfoComBox up = d as TextInfoComBox;
                up.InputValue.DisplayMemberPath = up.TextDataSourceDisplayMember;
            }
        }
        #endregion
    }
}
