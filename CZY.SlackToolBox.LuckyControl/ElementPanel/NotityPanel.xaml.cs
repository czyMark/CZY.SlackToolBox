using CZY.SlackToolBox.AnimationBank.Other;
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

namespace CZY.SlackToolBox.LuckyControl.ElementPanel
{
    /// <summary>
    /// NotityPanel.xaml 的交互逻辑
    /// </summary>
    public partial class NotityPanel : UserControl
    {
        public enum NotityPanelState { Normal, Success, Warn, Danegr, Custom }
        public NotityPanel()
        {
            InitializeComponent();

            NotityState=NotityPanelState.Normal;
            mainBorder.BorderBrush = (Brush)FindResource("infoColorBorderBrush");
            mainBorder.Background = (Brush)FindResource("infoColorBackground");
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.HideMe();
        }

        #region TipState
        public static readonly DependencyProperty TipStateProperty =
DependencyProperty.Register(nameof(NotityState), typeof(NotityPanelState), typeof(NotityPanel), new UIPropertyMetadata(OnTipStateChanged));
        public NotityPanelState NotityState
        {
            get => (NotityPanelState)GetValue(TipStateProperty);
            set => SetValue(TipStateProperty, value);
        }

        private static void OnTipStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotityPanel control = (NotityPanel)d;
            NotityPanelState NotityPanelState = (NotityPanelState)e.NewValue;
            switch (NotityPanelState)
            {
                case NotityPanelState.Normal:
                    control.mainBorder.BorderBrush = (Brush)control.FindResource("infoColorBorderBrush");
                    control.mainBorder.Background = (Brush)control.FindResource("infoColorBackground");
                    break;
                case NotityPanelState.Success:
                    control.mainBorder.BorderBrush = (Brush)control.FindResource("successBorderBrush");
                    control.mainBorder.Background = (Brush)control.FindResource("successBackground");
                    break;
                case NotityPanelState.Warn:
                    control.mainBorder.BorderBrush = (Brush)control.FindResource("warningBorderBrush");
                    control.mainBorder.Background = (Brush)control.FindResource("warningBackground");
                    break;
                case NotityPanelState.Danegr:
                    control.mainBorder.BorderBrush = (Brush)control.FindResource("dangerBorderBrush");
                    control.mainBorder.Background = (Brush)control.FindResource("dangerBackground");
                    break;
                case NotityPanelState.Custom:
                    control.mainBorder.BorderBrush = control.CustomBackground;
                    control.mainBorder.Background = control.CustomBackground;
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
         typeof(NotityPanel), new PropertyMetadata(CustomBackgroundChanged));
        private static void CustomBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotityPanel control = (NotityPanel)d;
            if (control.NotityState == NotityPanelState.Custom)
            {
                control.mainBorder.BorderBrush = control.CustomBackground;
                control.mainBorder.Background = control.CustomBackground;
            }
        }
        #endregion

        #region NotityTitle
        public string NotityTitle
        {
            get { return (string)GetValue(TipTextProperty); }
            set { SetValue(TipTextProperty, value); }
        }

        public static readonly DependencyProperty TipTextProperty = DependencyProperty.Register(
         "NotityTitle",
         typeof(string),
         typeof(NotityPanel), new PropertyMetadata(TipTextChanged));
        private static void TipTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                NotityPanel up = d as NotityPanel;
                up.titleLable.Content = e.NewValue.ToString();
            }
        }

        #endregion


        #region NotityContent
        public FrameworkElement NotityContent
        {
            get { return (FrameworkElement)GetValue(TipContentProperty); }
            set { SetValue(TipContentProperty, value); }
        }

        public static readonly DependencyProperty TipContentProperty = DependencyProperty.Register(
         "NotityContent",
         typeof(FrameworkElement),
         typeof(NotityPanel), new PropertyMetadata(TipContentChanged));
        private static void TipContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                NotityPanel up = d as NotityPanel;
                up.panelContent.Content = (FrameworkElement)e.NewValue;
            }
        }

        #endregion
    }
}
