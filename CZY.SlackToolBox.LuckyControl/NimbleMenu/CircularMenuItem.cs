using System;
using System.Windows; 
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.NimbleMenu
{
    [TemplatePart(Name = RotateTransformTemplateName, Type = typeof(RotateTransform))]
    public class CircularMenuItem : System.Windows.Controls.Control
    {
        private static readonly Type _typeofSelf = typeof(CircularMenuItem);
        private const string RotateTransformTemplateName = "PART_RotateTransform";
        private RotateTransform _angleRotateTransform;
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(CircularMenuItem), new UIPropertyMetadata(OnAngleChanged));

        private static void OnAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularMenuItem control = (CircularMenuItem)d;
            control.UpdateAngle();
        }
        void UpdateAngle()
        {
            if (_angleRotateTransform == null) return;
            _angleRotateTransform.Angle = Angle;
        }
        public string MenuTxt
        {
            get { return (string)GetValue(MenuTxtProperty); }
            set { SetValue(MenuTxtProperty, value); }
        }

        public static readonly DependencyProperty MenuTxtProperty =
            DependencyProperty.Register("MenuTxt", typeof(string), typeof(CircularMenuItem), new UIPropertyMetadata(OnMenuTxtChanged));


        private static void OnMenuTxtChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularMenuItem control = (CircularMenuItem)d;
            control.ToolTip = control.MenuTxt;
        }
        public Visibility ShowFill
        {
            get { return (Visibility)GetValue(ShowFillProperty); }
            set { SetValue(ShowFillProperty, value); }
        }

        public static readonly DependencyProperty ShowFillProperty =
            DependencyProperty.Register("ShowFill", typeof(Visibility), typeof(CircularMenuItem));
        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColorProperty =
           DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(CircularMenuItem), new PropertyMetadata(null));

        public ImageSource IconImage
        {
            get { return (ImageSource)GetValue(IconImageProperty); }
            set { SetValue(IconImageProperty, value); }
        }
        public static readonly DependencyProperty IconImageProperty =
            DependencyProperty.Register("IconImage", typeof(ImageSource), typeof(CircularMenuItem), new PropertyMetadata(null));

        static CircularMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(_typeofSelf, new FrameworkPropertyMetadata(_typeofSelf));

        }

        public string MenuItemID
        {
            get { return (string)GetValue(MenuItemIDProperty); }
            set { SetValue(MenuItemIDProperty, value); }
        }

        public static readonly DependencyProperty MenuItemIDProperty =
            DependencyProperty.Register("MenuItemID", typeof(string), typeof(CircularMenuItem));

         
         

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _angleRotateTransform = GetTemplateChild(RotateTransformTemplateName) as RotateTransform;
            UpdateAngle();
        }


    }
}
