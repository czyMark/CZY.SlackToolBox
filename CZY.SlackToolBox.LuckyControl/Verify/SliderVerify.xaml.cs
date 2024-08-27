using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CZY.SlackToolBox.LuckyControl.Verify
{
    /// <summary>
    /// SliderVerify.xaml 的交互逻辑
    /// </summary>
    public partial class SliderVerify : UserControl
    { 
        public SliderVerify()
        {
            InitializeComponent();
            this.Loaded += SliderVerify_Loaded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
             
            if (PART_Slider != null)
            {
                PART_Slider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(Slider_DragCompleted));
            }
        }

        public string ImageUri
        {
            get
            {
                return (string)GetValue(ImageUriProperty);
            }
            set
            {
                SetValue(ImageUriProperty, value);
            }
        }

        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register(nameof(ImageUri), typeof(string), typeof(SliderVerify), new PropertyMetadata(null, (p, d) => {
                (p as SliderVerify).Restart();
            }));

        public bool Result
        {
            get
            {
                return (bool)GetValue(ResultProperty);
            }
            set
            {
                SetValue(ResultProperty, value);
            }
        }


        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register(nameof(Result), typeof(bool), typeof(SliderVerify), new PropertyMetadata(false));

        #region Routed Event
        public static readonly RoutedEvent ResultChangedEvent = EventManager.RegisterRoutedEvent(nameof(ResultChanged), RoutingStrategy.Bubble, typeof(EventHandler), typeof(SliderVerify));
        public event EventHandler ResultChanged
        {
            add
            {
                AddHandler(ResultChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ResultChangedEvent, value);
            }
        }
        void RaiseResultChanged(bool result)
        {
            var arg = new RoutedEventArgs(ResultChangedEvent, result);
            RaiseEvent(arg);
        }
        #endregion

        private double _width = 48;

        private async void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            var thumb = PART_Slider.Template.FindName("thumb", this.PART_Slider) as Thumb;
            Path icon = thumb.Template.FindName("icon", thumb) as Path;
            var data = icon.Data;
            var fill = icon.Fill;

            if (Math.Abs(Canvas.GetLeft(PART_Path) - Canvas.GetLeft(PART_Pathfix)) <= 3)
            {
                icon.Fill = Brushes.Green;
                string sData = "M912 190h-69.9c-9.8 0-19.1 4.5-25.1 12.2L404.7 724.5 207 474a32 32 0 0 0-25.1-12.2H112c-6.7 0-10.4 7.7-6.3 12.9l273.9 347c12.8 16.2 37.4 16.2 50.3 0l488.4-618.9c4.1-5.1.4-12.8-6.3-12.8z";
                var converter = TypeDescriptor.GetConverter(typeof(Geometry));
                icon.Data = (Geometry)converter.ConvertFrom(sData);
                Result = true;
                RaiseResultChanged(Result);
            }
            else
            {
                icon.Fill = Brushes.Red;

                string sData = "M5.22675 -4.10175A0.5625 0.5625 0 0 0 6.02325 -4.10175L9 -7.079625L11.97675 -4.10175A0.5625 0.5625 0 0 0 12.77325 -4.89825L9.795375 -7.875L12.77325 -10.85175A0.5625 0.5625 0 0 0 11.97675 -11.64825L9 -8.670375L6.02325 -11.64825A0.5625 0.5625 0 0 0 5.22675 -10.85175L8.204625 -7.875L5.22675 -4.89825A0.5625 0.5625 0 0 0 5.22675 -4.10175z";
                var converter = TypeDescriptor.GetConverter(typeof(Geometry));
                icon.Data = (Geometry)converter.ConvertFrom(sData);
                RaiseResultChanged(Result);
            }

            icon.Fill = fill;
            icon.Data = data;
            Restart();
        }

        private void SliderVerify_Loaded(object sender, RoutedEventArgs e)
        {
            //Restart();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Restart();
        }

        private void Restart()
        {
            if (PART_Canvas == null || !PART_Canvas.IsVisible)
                return;

            Result = false;

            Random ran = new Random();
            double value = ran.Next((int)(PART_Canvas.ActualWidth - _width) / 3, (int)(PART_Canvas.ActualWidth - _width));
            SetTopCenter(PART_Pathfix);
            SetLeft(PART_Pathfix, value);

            BitmapImage image = GetBitmapImage();
            SetBackground(image);

            SetTopCenter(PART_Path);
            SetFill(PART_Path, image, value);

            PART_Slider.Value = 0;
            PART_Slider.Maximum = this.PART_Canvas.ActualWidth - _width;
            PART_Slider.ValueChanged -= Slider_ValueChanged;
            PART_Slider.ValueChanged += Slider_ValueChanged;
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetLeft(PART_Path, PART_Slider.Value);
        }

        private BitmapImage GetBitmapImage()
        {
            //BitmapImage image = new BitmapImage(new Uri("pack://application:,,,/CZY.SlackToolBox.LuckyControl.Styles.Bootstrap;component/Resources/1.jpg"));
            if (!string.IsNullOrEmpty(ImageUri))
            {
                // Create source.
                BitmapImage image = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.
                image.BeginInit();

                image.UriSource = new Uri(ImageUri);

                image.DecodePixelWidth = (int)PART_Canvas.ActualWidth;
                image.DecodePixelHeight = (int)PART_Canvas.ActualHeight;
                image.EndInit();

                return image;
            }

            return null;
        }

        private void SetBackground(BitmapImage image)
        {
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = image;

            PART_Canvas.Background = ib;
        }

        private void SetFill(Shape element, BitmapImage image, double value)
        {
            ImageBrush ib = new ImageBrush();
            ib.AlignmentX = AlignmentX.Left;
            ib.AlignmentY = AlignmentY.Top;
            ib.ImageSource = image;
            ib.Viewbox = new Rect(value, (PART_Canvas.ActualHeight - PART_Path.ActualHeight) / 2, PART_Path.ActualWidth, PART_Path.ActualHeight);
            ib.ViewboxUnits = BrushMappingMode.Absolute; //按百分比设置宽高
            ib.TileMode = TileMode.None; //按百分比应该不会出现 image小于要切的值的情况
            ib.Stretch = Stretch.None;

            element.Fill = ib;
        }

        private void SetTopCenter(FrameworkElement element)
        {
            double top = (PART_Canvas.ActualHeight - element.ActualHeight) / 2;
            Canvas.SetTop(element, top);
        }

        private void SetLeft(FrameworkElement element, double left)
        {
            Canvas.SetLeft(element, left);
        }
    }
}
