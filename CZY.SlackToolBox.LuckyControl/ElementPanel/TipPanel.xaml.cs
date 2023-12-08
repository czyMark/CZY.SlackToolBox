using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.ElementPanel
{
    /// <summary>
    /// 提示面板 的交互逻辑
    /// </summary>
    public partial class TipPanel : UserControl
    {
        public enum TipPanelState { Success, Warn, Danegr }
        public TipPanel()
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
         typeof(TipPanel), new PropertyMetadata(TipTextChanged));
        private static void TipTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TipPanel up = d as TipPanel;
                up.TipContent.Text = e.NewValue.ToString();
            }
        }

        #endregion


        #region PanelState
        public TipPanelState PanelState
        {
            get { return (TipPanelState)GetValue(PanelStateProperty); }
            set { SetValue(PanelStateProperty, value); }
        }

        public static readonly DependencyProperty PanelStateProperty = DependencyProperty.Register(
         "PanelState",
         typeof(TipPanelState),
         typeof(TipPanel), new PropertyMetadata(PanelStateChanged));
        private static void PanelStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TipPanel up = d as TipPanel;
               var t= (TipPanelState)e.NewValue;
                switch (t)
                {
                    case TipPanelState.Success:
                        up.TipMask.Background = (Brush)up.FindResource("successBackground");
                        up.TipMask.Background = (Brush)up.FindResource("successBorderBrush");
                        break;

                    case TipPanelState.Warn:
                        up.TipMask.Background = (Brush)up.FindResource("warningBackground");
                        up.TipMask.Background = (Brush)up.FindResource("warningBorderBrush");
                        break;

                    case TipPanelState.Danegr:
                        up.TipMask.Background = (Brush)up.FindResource("dangerBackground");
                        up.TipMask.Background = (Brush)up.FindResource("dangerBorderBrush");
                        break;
                    default:
                        up.TipMask.Background = (Brush)up.FindResource("dangerBackground");
                        up.TipMask.Background = (Brush)up.FindResource("dangerBorderBrush");
                        break;
                }
            }
        }

        #endregion



        #region TipWidth
        public string TipWidth
        {
            get { return (string)GetValue(TipWidthProperty); }
            set { SetValue(TipWidthProperty, value); }
        }

        public static readonly DependencyProperty TipWidthProperty = DependencyProperty.Register(
         "TipWidth",
         typeof(string),
         typeof(TipPanel), new PropertyMetadata(TipWidthChanged));
        private static void TipWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TipPanel up = d as TipPanel;
                up.TipContent.Width =double.Parse(e.NewValue.ToString());
            }
        }

        #endregion


        #region TipHeight
        public string TipHeight
        {
            get { return (string)GetValue(TipHeightProperty); }
            set { SetValue(TipHeightProperty, value); }
        }

        public static readonly DependencyProperty TipHeightProperty = DependencyProperty.Register(
         "TipHeight",
         typeof(string),
         typeof(TipPanel), new PropertyMetadata(TipHeightChanged));
        private static void TipHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TipPanel up = d as TipPanel;
                up.mainGrid.Height =double.Parse(e.NewValue.ToString());
            }
        }

        #endregion

        #region TipAlignment
        public string TipAlignment
        {
            get { return (string)GetValue(TipAlignmentProperty); }
            set { SetValue(TipAlignmentProperty, value); }
        }

        public static readonly DependencyProperty TipAlignmentProperty = DependencyProperty.Register(
         "TipAlignment",
         typeof(string),
         typeof(TipPanel), new PropertyMetadata(TipAlignmentChanged));
        private static void TipAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TipPanel up = d as TipPanel;
                switch (e.NewValue.ToString())
                {
                    case "1": up.TipContent.TextAlignment = TextAlignment.Left;  break;
                    case "2": up.TipContent.TextAlignment = TextAlignment.Center; break;
                    case "3": up.TipContent.TextAlignment = TextAlignment.Right; break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
