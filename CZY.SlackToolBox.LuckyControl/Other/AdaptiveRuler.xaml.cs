using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.Other
{
    /// <summary>
    /// AdaptiveRuler.xaml 的交互逻辑
    /// </summary>
    public partial class AdaptiveRuler : UserControl
    {
        public AdaptiveRuler()
        {
            InitializeComponent();
        }


        #region UnitText
        public string UnitText
        {
            get { return (string)GetValue(UnitTextProperty); }
            set { SetValue(UnitTextProperty, value); }
        }

        public static readonly DependencyProperty UnitTextProperty = DependencyProperty.Register(
         "UnitText",
         typeof(string),
         typeof(AdaptiveRuler), new PropertyMetadata(UnitTextChanged));
        private static void UnitTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                AdaptiveRuler adaptiveRuler = d as AdaptiveRuler;
                adaptiveRuler.UnitText = e.NewValue.ToString();
            }
        }

        #endregion

        #region TextAngle 缩放
        private double TextAngleValue { get; set; }
        public string TextAngle
        {
            get { return (string)GetValue(TextAngleProperty); }
            set { SetValue(TextAngleProperty, value); }
        }

        public static readonly DependencyProperty TextAngleProperty = DependencyProperty.Register(
         "TextAngle",
         typeof(string),
         typeof(AdaptiveRuler), new PropertyMetadata(TextAngleChanged));
        private static void TextAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {

                AdaptiveRuler adaptiveRuler = d as AdaptiveRuler;
                adaptiveRuler.TextAngle = e.NewValue.ToString();
                adaptiveRuler.TextAngleValue = double.Parse(e.NewValue.ToString());
                adaptiveRuler.DrawRule();
            }
        }

        #endregion

        #region Zoom 缩放
        private double ZoomValue { get; set; }
        public string Zoom
        {
            get { return (string)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register(
         "Zoom",
         typeof(string),
         typeof(AdaptiveRuler), new PropertyMetadata(ZoomChanged));
        private static void ZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {

                AdaptiveRuler adaptiveRuler = d as AdaptiveRuler;
                adaptiveRuler.Zoom = e.NewValue.ToString();
                adaptiveRuler.ZoomValue = double.Parse(e.NewValue.ToString());
                adaptiveRuler.DrawRule();
            }
        }

        private double ActualWidthValue { get; set; }
        public string ActualWidth
        {
            get { return (string)GetValue(ActualWidthProperty); }
            set { SetValue(ActualWidthProperty, value); }
        }

        public static readonly DependencyProperty ActualWidthProperty = DependencyProperty.Register(
         "ActualWidth",
         typeof(string),
         typeof(AdaptiveRuler), new PropertyMetadata(ActualWidthChanged));
        private static void ActualWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {

                AdaptiveRuler adaptiveRuler = d as AdaptiveRuler;
                adaptiveRuler.ActualWidth = e.NewValue.ToString();
                adaptiveRuler.ActualWidthValue = double.Parse(e.NewValue.ToString());
                adaptiveRuler.DrawRule();
            }
        }
        #endregion

        //画标尺
        private void DrawRule()
        {

            if (cvRuler.Children != null)
            {
                cvRuler.Children.Clear();
            }
            RotateTransform sctr = new RotateTransform();
            sctr.Angle = TextAngleValue;
            TransformGroup trfg = new TransformGroup();
            trfg.Children.Add(sctr);
            ScaleBorder.BorderBrush = this.Foreground;

            System.Windows.Shapes.Line _line;
            TextBlock _textBlock;

            const double _minPixel = 30;
            string _unit = UnitText;
            double _interval;
            double _intervalPixel;
            double _scientificF;
            int _scientificE;
            string[] _strTemp = (_minPixel / ZoomValue).ToString("E").Split('E');
            double.TryParse(_strTemp[0], out _scientificF);
            int.TryParse(_strTemp[1], out _scientificE);
            if (_scientificE >= 2 || (_scientificE >= 1 && _scientificF >= 5))
            {
                //_unit = UnitText+"m";
                _scientificE -= 3;
            }

            _textBlock = new TextBlock();

            _textBlock.FontSize = 8;

            _textBlock.RenderTransformOrigin = new Point(0.5, 0.5);
            _textBlock.RenderTransform = trfg;
            _textBlock.Text = _unit;
            _textBlock.Foreground = this.Foreground;
            _textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            _textBlock.Margin = new Thickness(10, 25, 0, 0);
            cvRuler.Children.Add(_textBlock);

            if (_scientificF >= 5)
            {
                _interval = 10 * Math.Pow(10, _scientificE);
            }
            else if (_scientificF >= 2.5)
            {
                _interval = 5 * Math.Pow(10, _scientificE);
            }
            else
            {
                _interval = 2.5 * Math.Pow(10, _scientificE);
            }

            _intervalPixel = _interval * ZoomValue;

            int _lineIndex = 0;
            double _width = ActualWidthValue - 10;
            double _pixelDistence = _intervalPixel / 5;
            for (double i = 10; i < _width; i += _pixelDistence)
            {
                _line = new System.Windows.Shapes.Line();
                if (_lineIndex % 5 == 0)
                {
                    _line.Stroke =   this.Foreground;
                    _line.StrokeThickness = 1;
                    _line.X1 = i;
                    _line.Y1 = 0;
                    _line.X2 = i;
                    _line.Y2 = 20;

                    _textBlock = new TextBlock();
                    _textBlock.Foreground = this.Foreground;
                    _textBlock.Text = (_interval * (_lineIndex / 5)).ToString();
                    _textBlock.FontSize = 8;
                    _textBlock.RenderTransformOrigin = new Point(0.5, 0.5);
                    _textBlock.RenderTransform = trfg;

                    _textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    _textBlock.Margin = new Thickness(i - 5, 25, 0, 0);
                    cvRuler.Children.Add(_textBlock);
                }
                else
                {
                    _line.Stroke = this.Foreground;
                    _line.StrokeThickness = 1;
                    _line.X1 = i;
                    _line.Y1 = 0;
                    _line.X2 = i;
                    _line.Y2 = 15;
                }
                cvRuler.Children.Add(_line);

                _lineIndex++;
            }
        }
    }
}
