using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.ElementPanel
{
    /// <summary>
    /// TipNotify.xaml 的交互逻辑
    /// </summary>
    public partial class TipContentPanel : UserControl
    {

        public enum TipPanelArrowState { None, Top, Left, Bottom, Right }
        public enum TipContentPanelState { Normal, Success, Warn, Danegr, Custom }
        public TipContentPanel()
        {
            InitializeComponent();
            TipArrowState = TipPanelArrowState.None;
            TipState = TipContentPanelState.Normal;
        }
        #region TipState
        public static readonly DependencyProperty TipStateProperty =
DependencyProperty.Register(nameof(TipState), typeof(TipContentPanelState), typeof(TipContentPanel), new UIPropertyMetadata(OnTipStateChanged));
        public TipContentPanelState TipState
        {
            get => (TipContentPanelState)GetValue(TipStateProperty);
            set => SetValue(TipStateProperty, value);
        }

        private static void OnTipStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TipContentPanel control = (TipContentPanel)d;
            TipContentPanelState TipContentPanelState = (TipContentPanelState)e.NewValue;
            switch (TipContentPanelState)
            {
                case TipContentPanelState.Normal:
                    control.panelContentBorder.BorderBrush = (Brush)control.FindResource("infoColorBorderBrush");
                    control.panelContentBorder.Background = (Brush)control.FindResource("infoColorBackground");
                    break;
                case TipContentPanelState.Success:
                    control.panelContentBorder.BorderBrush = (Brush)control.FindResource("successBorderBrush");
                    control.panelContentBorder.Background = (Brush)control.FindResource("successBackground");
                    break;
                case TipContentPanelState.Warn:
                    control.panelContentBorder.BorderBrush = (Brush)control.FindResource("warningBorderBrush");
                    control.panelContentBorder.Background = (Brush)control.FindResource("warningBackground");
                    break;
                case TipContentPanelState.Danegr:
                    control.panelContentBorder.BorderBrush = (Brush)control.FindResource("dangerBorderBrush");
                    control.panelContentBorder.Background = (Brush)control.FindResource("dangerBackground");
                    break;
                case TipContentPanelState.Custom:
                    control.panelContentBorder.BorderBrush = control.CustomBackground;
                    control.panelContentBorder.Background = control.CustomBackground;
                    break;
                default:
                    break;
            }
        } 
        #endregion

        #region CustomBackground
        public Brush CustomBackground
        {
            get { return (Brush)GetValue(CustomBackgroundProperty); }
            set { SetValue(CustomBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CustomBackgroundProperty = DependencyProperty.Register(
         "CustomBackground",
         typeof(Brush),
         typeof(TipContentPanel), new PropertyMetadata(CustomBackgroundChanged));
        private static void CustomBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TipContentPanel control = (TipContentPanel)d;
            if (control.TipState == TipContentPanelState.Custom)
            {
                control.panelContentBorder.BorderBrush = control.CustomBackground;
                control.panelContentBorder.Background = control.CustomBackground;
            }
        } 
        #endregion

        #region TipArrowState
        public TipPanelArrowState TipArrowState
        {
            get { return (TipPanelArrowState)GetValue(TipArrowStateProperty); }
            set { SetValue(TipArrowStateProperty, value); }
        }

        public static readonly DependencyProperty TipArrowStateProperty = DependencyProperty.Register(
         "TipArrowState",
         typeof(TipPanelArrowState),
         typeof(TipContentPanel), new PropertyMetadata(TipArrowStateChanged));
        private static void TipArrowStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TipContentPanel control = d as TipContentPanel;
                TipPanelArrowState tipPanelState = (TipPanelArrowState)e.NewValue;
                switch (tipPanelState)
                {
                    case TipPanelArrowState.Top:
                        control.icon.Angle = 0;
                        control.iconPanel.Visibility = Visibility.Visible;
                        control.iconPanel.HorizontalAlignment = HorizontalAlignment.Center;
                        control.iconPanel.VerticalAlignment = VerticalAlignment.Top;
                        control.panelContentBorder.Margin = new Thickness(0, 8.5, 0, 0);
                        break;
                    case TipPanelArrowState.Right:
                        control.icon.Angle = 90;

                        control.iconPanel.Visibility = Visibility.Visible;
                        control.iconPanel.HorizontalAlignment = HorizontalAlignment.Right;
                        control.iconPanel.VerticalAlignment = VerticalAlignment.Center;
                        control.panelContentBorder.Margin = new Thickness(0, 0, 8.5, 0);

                        break;
                    case TipPanelArrowState.Bottom:
                        control.icon.Angle = 180;
                        control.iconPanel.Visibility = Visibility.Visible;
                        control.iconPanel.HorizontalAlignment = HorizontalAlignment.Center;
                        control.iconPanel.VerticalAlignment = VerticalAlignment.Bottom;
                        control.panelContentBorder.Margin = new Thickness(0, 0, 0, 8.5);
                        break;
                    case TipPanelArrowState.Left:
                        control.icon.Angle = 270;
                        control.iconPanel.Visibility = Visibility.Visible;
                        control.iconPanel.HorizontalAlignment = HorizontalAlignment.Left;
                        control.iconPanel.VerticalAlignment = VerticalAlignment.Center;
                        control.panelContentBorder.Margin = new Thickness(8.5, 0, 0, 0);
                        break;
                    case TipPanelArrowState.None:
                        control.iconPanel.Visibility=Visibility.Collapsed;
                        control.panelContentBorder.Margin = new Thickness(0, 0, 0, 0);

                        break;
                }
            }
        }

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
         typeof(TipContentPanel), new PropertyMetadata(TipContentChanged));
        private static void TipContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TipContentPanel up = d as TipContentPanel;
                up.panelContent.Content = (FrameworkElement)e.NewValue;
            }
        }

        #endregion

    }
}
