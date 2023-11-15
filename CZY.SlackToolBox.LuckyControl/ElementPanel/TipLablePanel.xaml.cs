using System.Windows;
using System.Windows.Controls;

namespace CZY.SlackToolBox.LuckyControl.ElementPanel
{
    /// <summary>
    /// TipLablePanel.xaml 的交互逻辑
    /// </summary>
    public partial class TipLablePanel : UserControl
    {

        public enum TipLableState { Normal, Success, Warn, Danegr }
        public TipLablePanel()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty =
       DependencyProperty.Register(nameof(Text), typeof(string), typeof(TipLablePanel), new UIPropertyMetadata(OnTextChanged));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TipLablePanel control = (TipLablePanel)d;
            control.contentLabel.Content = e.NewValue;
        }



        public static readonly DependencyProperty TipStateProperty =
       DependencyProperty.Register(nameof(TipState), typeof(TipLableState), typeof(TipLablePanel), new UIPropertyMetadata(OnTipStateChanged));
        public TipLableState TipState
        {
            get => (TipLableState)GetValue(TipStateProperty);
            set => SetValue(TipStateProperty, value);
        }

        private static void OnTipStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TipLablePanel control = (TipLablePanel)d;
            TipLableState tipLableState=(TipLableState)e.NewValue  ;
            switch (tipLableState)
            {
                case TipLableState.Normal:
                    control.contentLabel.Style = (Style)control.FindResource("infoTipLable");
                    break;
                case TipLableState.Success:
                    control.contentLabel.Style = (Style)control.FindResource("successTipLable");
                    break;
                case TipLableState.Warn:
                    control.contentLabel.Style = (Style)control.FindResource("warningTipLable");
                    break;
                case TipLableState.Danegr:
                    control.contentLabel.Style = (Style)control.FindResource("dangerTipLable");
                    break;
                default:
                    break;
            }
        }
    }
}
