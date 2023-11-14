using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; 

namespace CZY.SlackToolBox.LuckyControl.Other
{
    /// <summary>
    /// DragControl.xaml 的交互逻辑
    /// </summary>
    public partial class DragControl : UserControl
    {
        public enum DragInitPosition
        {
            Center,
            CenterTop,
            CenterBottom,
            LeftTop,
            LeftCenter,
            LeftBottom,
            RightTop,
            RightCenter,
            RightBottom,
        }

        public static readonly DependencyProperty DragContentProperty =
            DependencyProperty.Register(nameof(DragContent), typeof(FrameworkElement), typeof(DragControl), new UIPropertyMetadata(OnDragContentChanged));
        public FrameworkElement DragContent
        {
            get => (FrameworkElement)GetValue(DragContentProperty);
            set => SetValue(DragContentProperty, value);
        }

        private static void OnDragContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DragControl control = (DragControl)d;
            if (control.DragContent != null)
            {
                control.DragContent.MouseLeftButtonDown -= control.Drag_MouseLeftButtonDown;
                control.DragContent.MouseMove -= control.Drag_MouseMove;
                control.DragContent.MouseLeftButtonUp -= control.Drag_MouseLeftButtonUp;
            }
            control.DragCanvas.Children.Clear();
            control.DragCanvas.Children.Add(control.DragContent);
        }


        public static readonly DependencyProperty DragContentWidthProperty =
          DependencyProperty.Register(nameof(DragContentWidth), typeof(double), typeof(DragControl));
        public double DragContentWidth
        {
            get => (double)GetValue(DragContentWidthProperty);
            set => SetValue(DragContentWidthProperty, value);
        }


        public static readonly DependencyProperty DragContentHeightProperty =
          DependencyProperty.Register(nameof(DragContentHeight), typeof(double), typeof(DragControl));
        public double DragContentHeight
        {
            get => (double)GetValue(DragContentHeightProperty);
            set => SetValue(DragContentHeightProperty, value);
        }

        public static readonly DependencyProperty OffsetLeftProperty =
          DependencyProperty.Register(nameof(OffsetLeft), typeof(double), typeof(DragControl));
        public double OffsetLeft
        {
            get => (double)GetValue(OffsetLeftProperty);
            set => SetValue(OffsetLeftProperty, value);
        }



        public static readonly DependencyProperty OffsetTopProperty =
          DependencyProperty.Register(nameof(OffsetTop), typeof(double), typeof(DragControl));
        public double OffsetTop
        {
            get => (double)GetValue(OffsetTopProperty);
            set => SetValue(OffsetTopProperty, value);
        }



        public static readonly DependencyProperty DragPositionProperty =
            DependencyProperty.Register(nameof(DragPosition), typeof(DragInitPosition), typeof(DragControl), new UIPropertyMetadata(OnDragPositionChanged));
        public DragInitPosition DragPosition
        {
            get => (DragInitPosition)GetValue(DragPositionProperty);
            set => SetValue(DragPositionProperty, value);
        }

        private static void OnDragPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DragControl control = (DragControl)d;
            control.LoadUIElement(control.DragContent);
        }

        public DragControl()
        {
            InitializeComponent();
            this.Loaded += DragControl_Loaded;
        }

        private void DragControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DragContent == null)
                return;
            // 拖动方块事件
            DragContent.MouseLeftButtonDown += Drag_MouseLeftButtonDown;
            DragContent.MouseMove += Drag_MouseMove;
            DragContent.MouseLeftButtonUp += Drag_MouseLeftButtonUp;
        }

        /// <summary>
        /// 加载绘制的UI控件
        /// </summary>
        /// <param name="uI">要拖动的控件</param>
        public void LoadUIElement(FrameworkElement uI)
        {
            DragContent = uI;
            DrawMenu();

            if (DragContent == null)
                return; 

            // 拖动方块事件
            DragContent.MouseLeftButtonDown += Drag_MouseLeftButtonDown;
            DragContent.MouseMove += Drag_MouseMove;
            DragContent.MouseLeftButtonUp += Drag_MouseLeftButtonUp;
        }
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DragCanvas.Width = GridPanel.ActualWidth;
            DragCanvas.Height = GridPanel.ActualHeight;
            DrawMenu();

        }
        private void DrawMenu()
        {
            if (DragContent == null)
                return;
            switch (DragPosition)
            {
                case DragInitPosition.Center:
                    Canvas.SetLeft(DragContent, DragCanvas.Width / 2 - DragContentWidth / 2 + OffsetLeft);
                    Canvas.SetTop(DragContent, DragCanvas.Height / 2 - DragContentHeight / 2 + OffsetTop);
                    break;
                case DragInitPosition.CenterTop:
                    Canvas.SetLeft(DragContent, DragCanvas.Width / 2 - DragContentWidth / 2 + OffsetLeft);
                    Canvas.SetTop(DragContent, 0 + OffsetTop);
                    break;
                case DragInitPosition.CenterBottom:
                    Canvas.SetLeft(DragContent, DragCanvas.Width / 2 - DragContentWidth / 2 + OffsetLeft);
                    Canvas.SetTop(DragContent, DragCanvas.Height - DragContentHeight + OffsetTop);
                    break;
                case DragInitPosition.LeftTop:
                    Canvas.SetLeft(DragContent, 0 + OffsetLeft);
                    Canvas.SetTop(DragContent, 0 + OffsetTop);
                    break;
                case DragInitPosition.LeftCenter:
                    Canvas.SetLeft(DragContent, 0 + OffsetLeft);
                    Canvas.SetTop(DragContent, DragCanvas.Height / 2 - DragContentHeight / 2 + OffsetTop);
                    break;
                case DragInitPosition.LeftBottom:
                    Canvas.SetLeft(DragContent, 0 + OffsetLeft);
                    Canvas.SetTop(DragContent, DragCanvas.Height - DragContentHeight + OffsetTop);
                    break;
                case DragInitPosition.RightTop:
                    Canvas.SetLeft(DragContent, DragCanvas.Width - DragContentWidth + OffsetLeft);
                    Canvas.SetTop(DragContent, 0 + OffsetTop);
                    break;
                case DragInitPosition.RightCenter:
                    Canvas.SetLeft(DragContent, DragCanvas.Width - DragContentWidth + OffsetLeft);
                    Canvas.SetTop(DragContent, DragCanvas.Height / 2 - DragContentHeight / 2 + OffsetTop);
                    break;
                case DragInitPosition.RightBottom:
                    Canvas.SetLeft(DragContent, DragCanvas.Width - DragContentWidth + OffsetLeft);
                    Canvas.SetTop(DragContent, DragCanvas.Height - DragContentHeight + OffsetTop);
                    break;
                default:
                    break;
            }

        }

        private void Drag_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((System.Windows.Controls.Control)sender).ReleaseMouseCapture();
            ((System.Windows.Controls.Control)sender).Cursor = Cursors.Arrow;
        }

        private void Drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // 鼠标位置
                Point point = e.GetPosition(DragCanvas);
                System.Windows.Controls.Control rec = (System.Windows.Controls.Control)sender;
                if (rec.ActualWidth == 0 || rec.ActualHeight==0)
                    return;
                double marginLeft = point.X - rec.ActualWidth / 2;
                double marginTop = point.Y - rec.ActualHeight / 2;
                // 超出边界
                if (marginLeft < 0)
                {
                    marginLeft = 0;
                }
                else if ((marginLeft + rec.ActualWidth) > DragCanvas.ActualWidth)
                {
                    marginLeft = DragCanvas.ActualWidth - rec.ActualWidth;
                }
                if (marginTop < 0)
                {
                    marginTop = 0;
                }
                else if ((marginTop + rec.ActualHeight) > DragCanvas.ActualHeight)
                {
                    marginTop = DragCanvas.ActualHeight - rec.ActualHeight;
                }
                Canvas.SetLeft(rec, marginLeft);
                Canvas.SetTop(rec, marginTop);
            }
        }

        private void Drag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((System.Windows.Controls.Control)sender).CaptureMouse();
            ((System.Windows.Controls.Control)sender).Cursor = Cursors.Hand;
           
        }
    }
}
