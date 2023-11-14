using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.LuckyControl.NotifyWindow
{
    /// <summary>
    /// FoldPanel.xaml 的交互逻辑
    /// </summary>
    public partial class FoldPanel : UserControl
    {
        public enum FoldPanelState { Top, Left, Bottom, Right }
        public FoldPanel()
        {
            InitializeComponent();
        }

        #region TipState
        public FoldPanelState TipState
        {
            get { return (FoldPanelState)GetValue(TipStateProperty); }
            set { SetValue(TipStateProperty, value); }
        }

        public static readonly DependencyProperty TipStateProperty = DependencyProperty.Register(
         "TipState",
         typeof(FoldPanelState),
         typeof(FoldPanel), new PropertyMetadata(TipStateChanged));
        private static void TipStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                FoldPanel control = d as FoldPanel;
                FoldPanelState FoldPanelState = (FoldPanelState)e.NewValue;
                switch (FoldPanelState)
                {
                    case FoldPanelState.Left:
                        control.btn.HorizontalAlignment = HorizontalAlignment.Left;
                        control.btn.VerticalAlignment = VerticalAlignment.Center;
                        control.btnRotate.Angle = 0;
                        break;
                    case FoldPanelState.Top:
                        control.btn.HorizontalAlignment = HorizontalAlignment.Center;
                        control.btn.VerticalAlignment = VerticalAlignment.Top;
                        control.mainGrid.VerticalAlignment = VerticalAlignment.Top;
                        control.btnRotate.Angle = 90; 
                        break;
                    case FoldPanelState.Right:
                        control.btn.HorizontalAlignment = HorizontalAlignment.Right;
                        control.btn.VerticalAlignment = VerticalAlignment.Center;
                        control.btnRotate.Angle = 180;

                        break;
                    case FoldPanelState.Bottom:
                        control.btn.HorizontalAlignment = HorizontalAlignment.Center;
                        control.btn.VerticalAlignment = VerticalAlignment.Bottom;
                        control.btnRotate.Angle = 270; 
                        break;
                }
            }
        }

        #endregion



        #region TipWidth
        public double TipWidth
        {
            get { return (double)GetValue(TipWidthProperty); }
            set { SetValue(TipWidthProperty, value); }
        }

        public static readonly DependencyProperty TipWidthProperty = DependencyProperty.Register(
         "TipWidth",
         typeof(double),
         typeof(FoldPanel));

        #endregion

        #region TipHeight
        public double TipHeight
        {
            get { return (double)GetValue(TipHeightProperty); }
            set { SetValue(TipHeightProperty, value); }
        }

        public static readonly DependencyProperty TipHeightProperty = DependencyProperty.Register(
         "TipHeight",
         typeof(double),
         typeof(FoldPanel));

        #endregion

        #region TipContent
        public FrameworkElement TipContent
        {
            get { return (FrameworkElement)GetValue(TipContentProperty); }
            set { SetValue(TipContentProperty, value); }
        }

        public static readonly DependencyProperty TipContentProperty = DependencyProperty.Register(
         "TipContent",
         typeof(FrameworkElement),
         typeof(FoldPanel), new PropertyMetadata(TipContentChanged));
        private static void TipContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                FoldPanel up = d as FoldPanel;
                up.panelContent.Content = (FrameworkElement)e.NewValue;
            }
        }

        #endregion


        private void btn_Checked(object sender, RoutedEventArgs e)
        {
            ThicknessAnimationUsingKeyFrames thicknessAnimationUsingKeyFrames = new ThicknessAnimationUsingKeyFrames();
            SplineThicknessKeyFrame splineThicknessKeyFrame = new SplineThicknessKeyFrame();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = new System.TimeSpan(0, 0, 0, 0, 300);
            splineThicknessKeyFrame.KeyTime = new System.TimeSpan(0, 0, 0, 0, 300);
            double margin =0;

            switch (TipState)
            {
                case FoldPanelState.Left:
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.VerticalAlignment = VerticalAlignment.Center;
                    mainBorder.Height = TipHeight;
                    doubleAnimation.To = TipWidth;
                    margin = TipWidth - 26;
                    mainGrid.HorizontalAlignment = HorizontalAlignment.Left;
                    mainGrid.VerticalAlignment = VerticalAlignment.Center;

                    splineThicknessKeyFrame.Value = new Thickness(margin, 0, 0, 0);
                    mainBorder.BeginAnimation(Border.WidthProperty, doubleAnimation);
                    break;
                case FoldPanelState.Top:
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    mainBorder.Width = TipWidth;

                    doubleAnimation.To = TipHeight;
                    margin = TipHeight - 26;
                    mainGrid.VerticalAlignment = VerticalAlignment.Top;
                    mainGrid.HorizontalAlignment = HorizontalAlignment.Center;
                    splineThicknessKeyFrame.Value = new Thickness(0, margin, 0, 0);
                    mainBorder.BeginAnimation(Border.HeightProperty, doubleAnimation);
                    break;
                case FoldPanelState.Bottom:
                    btn.VerticalAlignment = VerticalAlignment.Bottom;
                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    mainBorder.Width = TipWidth;
                    doubleAnimation.To = TipHeight;
                    margin = TipHeight - 26;

                    mainGrid.VerticalAlignment = VerticalAlignment.Bottom;
                    mainGrid.HorizontalAlignment = HorizontalAlignment.Center;
                    splineThicknessKeyFrame.Value = new Thickness(0, 0, 0, margin);
                    mainBorder.BeginAnimation(Border.HeightProperty, doubleAnimation);
                    break;
                case FoldPanelState.Right:
                    btn.HorizontalAlignment = HorizontalAlignment.Right;
                    btn.VerticalAlignment = VerticalAlignment.Center;

                    mainBorder.Height = TipHeight;
                    doubleAnimation.To = TipWidth;
                    margin = TipWidth - 26;
                    mainGrid.HorizontalAlignment = HorizontalAlignment.Right;
                    mainGrid.VerticalAlignment = VerticalAlignment.Center;
                    splineThicknessKeyFrame.Value = new Thickness(0, 0, margin, 0);
                    mainBorder.BeginAnimation(Border.WidthProperty, doubleAnimation);
                    break;
                default:
                    break;
            }
            thicknessAnimationUsingKeyFrames.KeyFrames.Add(splineThicknessKeyFrame);
            btn.BeginAnimation(ToggleButton.MarginProperty, thicknessAnimationUsingKeyFrames);
        }

        private void btn_Unchecked(object sender, RoutedEventArgs e)
        {

            ThicknessAnimationUsingKeyFrames thicknessAnimationUsingKeyFrames = new ThicknessAnimationUsingKeyFrames();
            SplineThicknessKeyFrame splineThicknessKeyFrame = new SplineThicknessKeyFrame();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = new System.TimeSpan(0, 0, 0, 0, 300);
            splineThicknessKeyFrame.KeyTime = new System.TimeSpan(0, 0, 0, 0, 300);
            double margin = 0;
            doubleAnimation.To = 0;
            switch (TipState)
            {
                case FoldPanelState.Left:
                    splineThicknessKeyFrame.Value = new Thickness(margin, 0, 0, 0);
                    mainBorder.BeginAnimation(Border.WidthProperty, doubleAnimation);
                    break;
                case FoldPanelState.Top:
                    splineThicknessKeyFrame.Value = new Thickness(0, margin, 0, 0);
                    mainBorder.BeginAnimation(Border.HeightProperty, doubleAnimation);
                    break;
                case FoldPanelState.Bottom:
                    splineThicknessKeyFrame.Value = new Thickness(0, 0, 0, margin);
                    mainBorder.BeginAnimation(Border.HeightProperty, doubleAnimation);
                    break;
                case FoldPanelState.Right:
                    splineThicknessKeyFrame.Value = new Thickness(0, 0, margin, 0);
                    mainBorder.BeginAnimation(Border.WidthProperty, doubleAnimation);
                    break;
                default:
                    break;
            }
            thicknessAnimationUsingKeyFrames.KeyFrames.Add(splineThicknessKeyFrame);
            btn.BeginAnimation(ToggleButton.MarginProperty, thicknessAnimationUsingKeyFrames);
        }
    }
}
