using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CZY.SlackToolBox.AnimationBank.Loading
{
    /// <summary>
    /// LoadingEllipsePendulum.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingEllipsePendulum : UserControl
    {
        public static DependencyProperty FillColorProperty = DependencyProperty.Register("FillColor", typeof(SolidColorBrush), typeof(LoadingEllipsePendulum), new PropertyMetadata(null));
        public SolidColorBrush FillColor
        {
            get { return (SolidColorBrush)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        public static DependencyProperty ShowTextProperty = DependencyProperty.Register("ShowText", typeof(string), typeof(LoadingEllipsePendulum), new PropertyMetadata(null));
        public string ShowText
        {
            get { return (string)GetValue(ShowTextProperty); }
            set { SetValue(ShowTextProperty, value); }
        }

        public LoadingEllipsePendulum()
        {
            InitializeComponent();
        }
    }
}
