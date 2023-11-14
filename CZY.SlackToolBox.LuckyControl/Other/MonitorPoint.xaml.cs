using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.LuckyControl.Other
{
    /// <summary>
    /// 监控提示点-图片 的交互逻辑
    /// </summary>
    public partial class MonitorPoint : UserControl
    {
        public enum MonitorPointState { Normal, Flicker }

        public MonitorPoint()
        {
            InitializeComponent();
            this.DataContext = this;

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #region TipText
        public string TipText
        {
            get { return (string)GetValue(TipTextProperty); }
            set { SetValue(TipTextProperty, value); }
        }

        public static readonly DependencyProperty TipTextProperty = DependencyProperty.Register(
         "TipText",
         typeof(string),
         typeof(MonitorPoint), new PropertyMetadata(TipTextChanged));
        private static void TipTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                MonitorPoint up = d as MonitorPoint;
                up.TipLab.Content = e.NewValue.ToString();
            }
        }

        #endregion


        #region PointState
        public ImageSource PointState
        {
            get { return (ImageSource)GetValue(PointStateProperty); }
            set { SetValue(PointStateProperty, value); }
        }

        public static readonly DependencyProperty PointStateProperty = DependencyProperty.Register(
         "PointState",
         typeof(ImageSource),
         typeof(MonitorPoint), new PropertyMetadata(PointStateChanged));
        private static void PointStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MonitorPoint up = d as MonitorPoint;
            if (e.NewValue != null)
            {
                up.PointImg.Source = e.NewValue as ImageSource;
            }
            else
            {
                up.PointImg.Source = null;
            }
        }

        #endregion


        #region AnimationState
        public MonitorPointState AnimationState
        {
            get { return (MonitorPointState)GetValue(AnimationStateProperty); }
            set { SetValue(AnimationStateProperty, value); }
        }

        public static readonly DependencyProperty AnimationStateProperty = DependencyProperty.Register(
         "AnimationState",
         typeof(MonitorPointState),
         typeof(MonitorPoint), new PropertyMetadata(AnimationStateChanged));
        private static void AnimationStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                MonitorPoint up = d as MonitorPoint;
                MonitorPointState monitorPointState = (MonitorPointState)e.NewValue;
                switch (monitorPointState)
                {
                    case MonitorPointState.Normal:
                        {
                            DoubleAnimation OpacityValue = new DoubleAnimation()
                            {
                                From = 1,
                                To = 1,
                                Duration = new TimeSpan(0, 0, 0, 1, 0),
                            };
                            up.PointImg.BeginAnimation(Image.OpacityProperty, OpacityValue);
                        }
                        break;
                    case MonitorPointState.Flicker:
                        {
                            DoubleAnimation OpacityValue = new DoubleAnimation()
                            {
                                From = 0.5,
                                To = 1,
                                AutoReverse = true,
                                RepeatBehavior = RepeatBehavior.Forever,
                                Duration = new TimeSpan(0, 0, 0, 1, 0),
                            };
                            up.PointImg.BeginAnimation(Image.OpacityProperty, OpacityValue);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion




        #region PointHeight
        public double PointHeight
        {
            get { return (double)GetValue(PointHeightProperty); }
            set { SetValue(PointHeightProperty, value); }
        }

        public static readonly DependencyProperty PointHeightProperty = DependencyProperty.Register(
         "PointHeight",
         typeof(double),
         typeof(MonitorPoint), new PropertyMetadata(PointHeightChanged));
        private static void PointHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                MonitorPoint up = d as MonitorPoint;
                up.PointImg.Height = double.Parse(e.NewValue.ToString());
            }
        }

        #endregion


        #region PointWidth
        public double PointWidth
        {
            get { return (double)GetValue(PointWidthProperty); }
            set { SetValue(PointWidthProperty, value); }
        }

        public static readonly DependencyProperty PointWidthProperty = DependencyProperty.Register(
         "PointWidth",
         typeof(double),
         typeof(MonitorPoint), new PropertyMetadata(PointWidthChanged));
        private static void PointWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                MonitorPoint up = d as MonitorPoint;
                up.PointImg.Width = double.Parse(e.NewValue.ToString());
            }
        }

        #endregion


        #region SPanelHeight
        public double SPanelHeight
        {
            get { return (double)GetValue(SPanelHeightProperty); }
            set { SetValue(SPanelHeightProperty, value); }
        }

        public static readonly DependencyProperty SPanelHeightProperty = DependencyProperty.Register(
         "SPanelHeight",
         typeof(double),
         typeof(MonitorPoint), new PropertyMetadata(SPanelHeightChanged));
        private static void SPanelHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                MonitorPoint up = d as MonitorPoint;
                up.SPanel.Height = double.Parse(e.NewValue.ToString());
            }
        }

        #endregion
    }
}
