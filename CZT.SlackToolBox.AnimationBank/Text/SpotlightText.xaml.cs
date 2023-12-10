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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CZY.SlackToolBox.AnimationBank.Text
{
    /// <summary>
    /// SpotlightText.xaml 的交互逻辑
    /// </summary>
    public partial class SpotlightText : UserControl
    {
        public SpotlightText()
        {
            InitializeComponent();
            PART_EllipseGeometry.Center = new Point(GeometryText.FontSize / 2, GeometryText.FontSize / 2);
            this.Loaded += SpotlightText_Loaded;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
         "Text",
         typeof(string),
         typeof(SpotlightText), new PropertyMetadata(TextChanged));
        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpotlightText up = (d as SpotlightText);
            up.GeometryText.Text=up.Text;
        }
        private void SpotlightText_Loaded(object sender, RoutedEventArgs e)
        {
            var doubleAnimation = new PointAnimation
            {
                To = new Point(GeometryText.ActualWidth, GeometryText.FontSize),
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            PART_EllipseGeometry.BeginAnimation(EllipseGeometry.CenterProperty, doubleAnimation);
        }
    }
}
