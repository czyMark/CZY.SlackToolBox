using CZY.SlackToolBox.LuckyControl.ElementPanel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static CZY.SlackToolBox.LuckyControl.ElementPanel.TipPanel;
using Random = CZY.SlackToolBox.FastExtend;

namespace CZY.SlackToolBox.LuckyControl.Verify
{
    /// <summary>
    /// TextClickVerify.xaml 的交互逻辑
    /// </summary>
    public partial class TextClickVerify : UserControl
    {
        public enum TextVerifyEnum
        {
            Emoj,
            Idioms,
            Chinese
        }
        public TextClickVerify()
        {
            InitializeComponent();
            this.Loaded += TextClickVerify_Loaded;
            PART_Canvas.MouseLeftButtonDown += MyCanvas_MouseLeftButtonDown;
            PART_Reset.Click += new RoutedEventHandler(BtnReset_Click);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PART_Reset.Visibility == Visibility.Visible)
            {
                Restart();
                return;
            }

            var position = e.GetPosition(PART_Canvas);
            AddPath(clicklength - strs.Count + 1, position.X, position.Y);

            if (e.OriginalSource.GetType() == typeof(Grid))
            {
                Grid grid = (Grid)e.OriginalSource;
                if (grid.Tag.ToString() == strs.FirstOrDefault())
                {
                    strs.RemoveAt(0);
                    if (strs.Count == 0)
                    {
                        Result = true;
                        RaiseResultChanged(Result);
                        PART_Info.Visibility = Visibility.Collapsed;
                        PART_Reset.Visibility = Visibility.Visible;
                        PART_Reset.Content = "验证成功";
                        PART_Reset.Background = Brushes.Green;
                    }
                }
                else
                {

                    RaiseResultChanged(Result);
                    PART_Info.Visibility = Visibility.Collapsed;
                    PART_Reset.Visibility = Visibility.Visible;
                    PART_Reset.Content = "验证失败，请重试";
                    PART_Reset.Background = Brushes.Red;
                }
            }
            else
            {
                RaiseResultChanged(Result);
                PART_Info.Visibility = Visibility.Collapsed;
                PART_Reset.Visibility = Visibility.Visible;
                PART_Reset.Content = "验证失败，请重试";
                PART_Reset.Background = Brushes.Red;
            }
        }

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
            DependencyProperty.Register(nameof(Result), typeof(bool), typeof(TextClickVerify), new PropertyMetadata(false));

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
            DependencyProperty.Register(nameof(ImageUri), typeof(string), typeof(TextClickVerify), new PropertyMetadata(null));

        #region Routed Event
        public static readonly RoutedEvent ResultChangedEvent = EventManager.RegisterRoutedEvent(nameof(ResultChanged), RoutingStrategy.Bubble, typeof(EventHandler), typeof(TextClickVerify));
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

        private void TextClickVerify_Loaded(object sender, RoutedEventArgs e)
        {
            //Restart();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Restart();
        }

        private List<string> strs;
        private int clicklength;
        private void Restart()
        {
            if (!PART_Canvas.IsVisible)
                return;

            Result = false;


            BitmapImage image = GetBitmapImage();
            SetBackground(image);


            string tooltip = string.Empty;
            switch (TextVerify)
            {
                case TextVerifyEnum.Emoj:
                    {

                    }
                    break;
                case TextVerifyEnum.Idioms:
                    {
                        tooltip = Random.RandomTool.GetVcodeNum(1, Random.RandomData.Instance.Idioms);
                        clicklength = tooltip.Length;

                        strs = new List<string>();
                        for (int i = 0; i < clicklength; i++)
                        {
                            strs.Add(tooltip.Substring(i, 1));
                        }
                    }
                    break;
                case TextVerifyEnum.Chinese:

                    {
                        strs = new List<string>();
                        for (int i = 0; i < 4; i++)
                        {
                            //调用函数产生4个随机中文汉字编码 
                            strs.Add(Random.RandomTool.GetChinese().ToString());
                        }
                        clicklength = 3;
                    }
                    break;
                default:
                    {
                        strs = new List<string>();
                        for (int i = 0; i < 4; i++)
                        {
                            //调用函数产生4个随机中文汉字编码 
                            strs.Add(Random.RandomTool.GetChinese().ToString());
                        }
                        clicklength = 3;
                    }
                    break;
            }

            int width = (int)(PART_Canvas.ActualWidth - 30);
            int height = (int)(PART_Canvas.ActualHeight - 40);
            var brush = this.Background;

            var clickstrs = strs.OrderBy(p => Random.RandomTool.NextDouble()).ToArray();
            PART_Canvas.Children.Clear();
            for (int i = 0; i < strs.Count; i++)
            {
                AddChild(clickstrs[i], i, strs.Count, brush, width, height);
            }
            strs = strs.Take(clicklength).ToList();

            PART_Info.Visibility = Visibility.Visible;
            if (clicklength == 3)
            {
                PART_Info.Text = $"请依次点击\"{strs[0]}\"\"{strs[1]}\"\"{strs[2]}\"";
                PART_Info.ToolTip = PART_Info.Text;
            }
            else
            {
                PART_Info.Text = "请按语序依次点击文字";
                PART_Info.ToolTip = $"\"{string.Join("", strs)}\"{tooltip}";
            }
            PART_Reset.Visibility = Visibility.Collapsed;
            PART_Reset.Background = Brushes.Transparent;
        }

        public void AddChild(string str, int index, int totalindex, Brush brush, int width, int height)
        {
            Grid grid = new Grid();
            grid.Tag = str;
            //Todo:显示的文字可以优化
            TextBlock outlinetext = new TextBlock()
            {
                FontSize = 30,
                Text = str,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(Random.RandomTool.GetSimpNum(0, 255)), Convert.ToByte(Random.RandomTool.Next(0, 255)), Convert.ToByte(Random.RandomTool.Next(0, 255)))),//brush,
                IsHitTestVisible = false,
            };
            grid.Children.Add(outlinetext);

            SetLeft(grid, Random.RandomTool.GetSimpNum((int)(width * index / totalindex), (int)(width * (index + 1) / totalindex)));
            SetTop(grid, Random.RandomTool.GetSimpNum(0, (int)height));
            RotateTransform rtf = new RotateTransform(Random.RandomTool.GetSimpNum(0, 360), 15, 20);
            grid.RenderTransform = rtf;

            grid.Background = new SolidColorBrush(Colors.Transparent);
            PART_Canvas.Children.Add(grid);
        }

        private void AddPath(int number, double left, double top)
        {
            Grid grid = new Grid();

            Path path = new Path();
            path.Fill = this.Background;
            path.Stroke = this.Foreground;
            string sData = "M12,2A7,7 0 0,0 5,9C5,14.25 12,22 12,22C12,22 19,14.25 19,9A7,7 0 0,0 12,2Z";
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            path.Data = (Geometry)converter.ConvertFrom(sData);
            path.Height = 40;
            path.Width = 30;
            path.StrokeThickness = 2;
            path.Stretch = Stretch.Fill;
            path.HorizontalAlignment = HorizontalAlignment.Center;
            path.VerticalAlignment = VerticalAlignment.Center;

            grid.Children.Add(path);

            TextBlock text = new TextBlock();
            text.Text = number.ToString();
            text.Foreground = this.Foreground;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.Margin = new Thickness(0, 0, 0, 2);
            grid.Children.Add(text);

            PART_Canvas.Children.Add(grid);
            SetLeft(grid, left - path.Width / 2);
            SetTop(grid, top - path.Height);
        }

        private BitmapImage GetBitmapImage()
        {
            int value = Random.RandomTool.GetSimpNum(1, 3);

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


        private void SetVerCenter(FrameworkElement element)
        {
            double top = (PART_Canvas.ActualHeight - element.ActualHeight) / 2;
            Canvas.SetTop(element, top);
        }

        private void SetLeft(FrameworkElement element, double left)
        {
            Canvas.SetLeft(element, left);
        }

        private void SetTop(FrameworkElement element, double top)
        {
            Canvas.SetTop(element, top);
        }

        public TextVerifyEnum TextVerify
        {
            get { return (TextVerifyEnum)GetValue(TextVerifyProperty); }
            set { SetValue(TextVerifyProperty, value); }
        }

        public static readonly DependencyProperty TextVerifyProperty = DependencyProperty.Register(
         "TextVerify",
         typeof(TextVerifyEnum),
         typeof(TextClickVerify), new PropertyMetadata(TextVerifyChanged));
        private static void TextVerifyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        { 
        
        }

    }
}
