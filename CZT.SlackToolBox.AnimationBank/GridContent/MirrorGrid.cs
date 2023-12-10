using System.Windows;
using System.Windows.Media;

namespace CZY.SlackToolBox.AnimationBank
{
    /// <summary>
    /// 镜像格子
    /// </summary>
    public class MirrorGrid : System.Windows.Controls.Grid
    {
        /// <summary>
        /// 目标对象
        /// </summary>
        public FrameworkElement Target
        {
            get { return (FrameworkElement)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        /// <summary>
        /// 目标对象属性
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
        DependencyProperty.Register("Target", typeof(FrameworkElement), typeof(MirrorGrid),
        new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 重写基类 Margin
        /// </summary>
        public new Thickness Margin
        {
            get { return (Thickness)GetValue(MarginProperty); }
            set { SetValue(MarginProperty, value); }
        }
        public new static readonly DependencyProperty MarginProperty = DependencyProperty.Register("Margin", typeof(Thickness), typeof(MirrorGrid), new FrameworkPropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnMarginChanged)));
        private static void OnMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((FrameworkElement)target).Margin = (Thickness)e.NewValue;
            //强制渲染
            ((FrameworkElement)target).InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            var Rect = new Rect(0, 0, RenderSize.Width, RenderSize.Height);
            if (Target == null)
            {
                dc.DrawRectangle(this.Background, null, Rect);
            }
            else
            {
                this.DrawImage(dc, Rect);
            }

        }

        private void DrawImage(DrawingContext dc, Rect rect)
        {
            VisualBrush brush = new VisualBrush(Target)
            {
                Stretch = Stretch.Fill,
            };
            var tl = this.GetElementLocation(Target);
            var sl = this.GetElementLocation(this);
            var lx = (sl.X - tl.X) / Target.ActualWidth;
            var ly = (sl.Y - tl.Y) / Target.ActualHeight;
            var pw = this.ActualWidth / Target.ActualWidth;
            var ph = this.ActualHeight / Target.ActualHeight;
            brush.Viewbox = new Rect(lx, ly, pw, ph);
            dc.DrawRectangle(brush, null, rect);
        }


        /// <summary>
        /// 获取控件元素在窗口的实际位置
        /// </summary>
        /// <param x:Name="Control"></param>
        /// <returns></returns>
        public Point GetElementLocation(FrameworkElement Control)
        {
            Point location = new Point(0, 0);
            FrameworkElement element = Control;
            while (element != null)
            {
                var Offset = VisualTreeHelper.GetOffset(element);
                location = location + Offset;
                element = (FrameworkElement)VisualTreeHelper.GetParent(element);
            }
            return location;
        }

    }
}
