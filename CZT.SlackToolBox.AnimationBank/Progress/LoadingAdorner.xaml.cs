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

namespace CZY.SlackToolBox.AnimationBank
{
    /// <summary>
    /// LoadingAdorner.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingAdorner : UserControl
    {
        #region 组织界面数据
        private readonly DispatcherTimer animationTimer;

        public int TextSize
        {
            get { return (int)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextSizeProperty =
            DependencyProperty.Register("TextSize", typeof(int), typeof(LoadingAdorner), new PropertyMetadata(
                defaultValue: 24,
                propertyChangedCallback: new PropertyChangedCallback(
                    (sender, e) =>
                    {
                        var loading = sender as LoadingAdorner;
                        if (loading != null)
                        {
                            loading.TextControl.FontSize = (int)e.NewValue;
                        }
                    })
                ));


        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Color), typeof(LoadingAdorner), new PropertyMetadata(
                defaultValue: Colors.Black,
                propertyChangedCallback: new PropertyChangedCallback(
                    (sender, e) =>
                    {
                        var loading = sender as LoadingAdorner;
                        if (loading != null)
                        {
                            loading.TextControl.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(e.NewValue.ToString()));
                        }
                    }),
                coerceValueCallback: new CoerceValueCallback((sender, e) =>
                {
                    LoadingAdorner loading = (LoadingAdorner)sender;
                    try
                    {
                        return (Color)ColorConverter.ConvertFromString(e.ToString());
                    }
                    catch (Exception ex)
                    {
                        return Colors.Black;
                    }
                })
                ));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LoadingAdorner), new PropertyMetadata(
                defaultValue: string.Empty,
                propertyChangedCallback: new PropertyChangedCallback(
                    (sender, e) =>
                    {
                        var loading = sender as LoadingAdorner;
                        if (loading != null)
                        {
                            if (e.NewValue == null)
                                loading.TextControl.Text = "";
                            else
                                loading.TextControl.Text = e.NewValue.ToString();
                        }
                    })
                ));


        public string Tip
        {
            get { return (string)GetValue(TipProperty); }
            set { SetValue(TipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Tip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipProperty =
            DependencyProperty.Register("Tip", typeof(string), typeof(LoadingAdorner), new PropertyMetadata(
                defaultValue: string.Empty,
                propertyChangedCallback: new PropertyChangedCallback(
                    (sender, e) =>
                    {
                        var loading = sender as LoadingAdorner;
                        if (loading != null)
                        {
                            loading.LayoutRoot.ToolTip = e.NewValue;
                        }
                    })
                ));

        #endregion

        public LoadingAdorner()
        {
            InitializeComponent();
            //动画
            animationTimer = new DispatcherTimer(
               DispatcherPriority.ContextIdle, Dispatcher);
            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 75);
        }
        #region 绘制界面
        private void Start()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            animationTimer.Tick += HandleAnimationTick;
            animationTimer.Start();
        }

        private void Stop()
        {
            animationTimer.Stop();
            Mouse.OverrideCursor = Cursors.Arrow;
            animationTimer.Tick -= HandleAnimationTick;
        }

        private void HandleAnimationTick(object sender, EventArgs e)
        {
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
        }

        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            const double offset = Math.PI;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(C0, offset, 0.0, step);
            SetPosition(C1, offset, 1.0, step);
            SetPosition(C2, offset, 2.0, step);
            SetPosition(C3, offset, 3.0, step);
            SetPosition(C4, offset, 4.0, step);
            SetPosition(C5, offset, 5.0, step);
            SetPosition(C6, offset, 6.0, step);
            SetPosition(C7, offset, 7.0, step);
            SetPosition(C8, offset, 8.0, step);
        }

        //设置动画中小圆的位置
        private void SetPosition(Ellipse ellipse, double offset,
            double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50.0
                + Math.Sin(offset + posOffSet * step) * 50.0);

            ellipse.SetValue(Canvas.TopProperty, 50
                + Math.Cos(offset + posOffSet * step) * 50.0);
        }

        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        //显示控件就启动动画，隐藏(不显示）就停止动画
        private void HandleVisibleChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            bool isVisible = (bool)e.NewValue;

            if (isVisible)
                Start();
            else
                Stop();
        }
        #endregion
    }
}
