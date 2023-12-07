using CZY.SlackToolBox.AnimationBank.Common;
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
using System.Windows.Threading;

namespace CZY.SlackToolBox.AnimationBank.Background
{
    /// <summary>
    /// WavesCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class WavesCanvas : UserControl
    {
        //动画时间器
        private DispatcherTimer updateTimer;
        public WavesCanvas()
        {
            InitializeComponent();
            WaveA = -5;
            WaveW = 1 / 30.0;
            WaveHeight = 1;
            WaveCount = 3;
            //启动刷新动画
            updateTimer = new System.Windows.Threading.DispatcherTimer();
            updateTimer.Tick += new EventHandler(DrawingWaves);
            updateTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            updateTimer.Start();
        }
        List<double> WaveOffset = new List<double>();


        private static System.Random random = new System.Random();
        private static double oldNum; 
        public static int GetSimpNum(int min, int max)
        {
            int temp = random.Next(min, max);

            if (temp == oldNum)
            {
                return GetSimpNum(min, max);
            }
            else
            {
                oldNum = temp;
            }
            return temp;
        }
        private void DrawingWaves(object sender, EventArgs e)
        {

            GeometryGroup group = new GeometryGroup();
            for (int i = 0; i < WaveCount; i++)
            {
                if (WaveOffset.Count <= i)
                {
                    WaveOffset.Add(GetSimpNum(1,30));
                }
                if (WaveOffset[i] <= 100)
                {
                    //获取水波绘制线
                    WaveOffset[i] = WaveOffset[i] + 0.08;//每个间隔百分5
                }
                else
                {
                    WaveOffset[i] = 0;
                }
                StreamGeometry streamGeometry = GetSinGeometry(WaveOffset[i]);
                group.Children.Add(streamGeometry);
            }

            MainCanvas.Children.RemoveRange(0, WaveCount);
            for (int i = 0; i < WaveCount; i++)
            { 
                //Todo:修改成 RepeatCollection
                SolidColorBrush solidColorBrush;
                if (WaveColor != null)
                {
                    solidColorBrush = (SolidColorBrush)WaveColor.Next;
                }
                else
                {
                    switch (i)
                    {
                        case 0: solidColorBrush = Brushes.White; break;
                        case 1: solidColorBrush = Brushes.WhiteSmoke; break;
                        case 2: solidColorBrush = Brushes.AntiqueWhite; break;
                        case 3: solidColorBrush = Brushes.BurlyWood; break;
                        default:
                            solidColorBrush = Brushes.WhiteSmoke;
                            break;
                    }
                }
                Path myPath = new Path
                {
                    Fill = solidColorBrush,
                    Data = group.Children[i]
                };
                //绘制
                MainCanvas.Children.Add(myPath);
            }
        }

        public RepeatCollection WaveColor
        {
            get { return (RepeatCollection)GetValue(WaveColorProperty); }
            set { SetValue(WaveColorProperty, value); }
        }
        public static readonly DependencyProperty WaveColorProperty = DependencyProperty.Register(
         "WaveColor",
         typeof(RepeatCollection),
         typeof(WavesCanvas), new PropertyMetadata(WaveColorChanged));
        private static void WaveColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public double WaveA
        {
            get { return (double)GetValue(WaveAProperty); }
            set { SetValue(WaveAProperty, value); }
        }
        public static readonly DependencyProperty WaveAProperty = DependencyProperty.Register(
         "WaveA",
         typeof(double),
         typeof(WavesCanvas), new PropertyMetadata(WaveAChanged));
        private static void WaveAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }


        public double WaveW
        {
            get { return (double)GetValue(WaveWProperty); }
            set { SetValue(WaveWProperty, value); }
        }
        public static readonly DependencyProperty WaveWProperty = DependencyProperty.Register(
         "WaveW",
         typeof(double),
         typeof(WavesCanvas), new PropertyMetadata(WaveWChanged));
        private static void WaveWChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }



        public int WaveCount
        {
            get { return (int)GetValue(WaveCountProperty); }
            set { SetValue(WaveCountProperty, value); }
        }
        public static readonly DependencyProperty WaveCountProperty = DependencyProperty.Register(
         "WaveCount",
         typeof(int),
         typeof(WavesCanvas), new PropertyMetadata(WaveCountChanged));
        private static void WaveCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public double WaveHeight
        {
            get { return (double)GetValue(WaveHeightProperty); }
            set { SetValue(WaveHeightProperty, value); }
        }
        public static readonly DependencyProperty WaveHeightProperty = DependencyProperty.Register(
         "WaveHeight",
         typeof(double),
         typeof(WavesCanvas), new PropertyMetadata(WaveHeightChanged));
        private static void WaveHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        /// <summary>
        /// 正弦曲线计算
        /// </summary>  
        /// <returns></returns>
        private StreamGeometry GetSinGeometry(double offset)
        {
            StreamGeometry g = new StreamGeometry();
            using (StreamGeometryContext ctx = g.Open())
            {
                double waveHeight = this.Height - this.Height * WaveHeight / 100;//计算出百分比高度
                double offsetX = -this.Width + this.Width * offset / 100;//求得水波从左往右移动位置
                ctx.BeginFigure(new Point(0, MainControl.ActualWidth), true, true);
                for (int x = 0; x < MainControl.ActualWidth; x += 1)
                {
                    double y = WaveA * Math.Sin(x * WaveW + offsetX) + waveHeight;
                    ctx.LineTo(new Point(x, y), true, true);
                }
                ctx.LineTo(new Point(MainControl.ActualWidth, MainControl.ActualWidth), true, true);
                return g;
            }
        }
    }
}
