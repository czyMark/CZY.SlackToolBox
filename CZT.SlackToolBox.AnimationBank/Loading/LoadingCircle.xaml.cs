using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CZY.SlackToolBox.AnimationBank.Loading
{
    /// <summary>
    /// LoadingCircle.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingCircle : UserControl
    {
        public LoadingCircle()
        {
            InitializeComponent();
            InitCircle();
        }

        private void InitCircle()
        {
            //右上角
            EllipseCircle rightUpCircle = new EllipseCircle();
            rightUpCircle.InitEllipse(new Point(0, 0));
            rightUpCircle.BeginPathAnimation(CircleGeometry(new Point(0, 0), new Point(141, 141), 100, true, SweepDirection.Clockwise), 2);
            //右下角
            EllipseCircle rightDownCircle = new EllipseCircle();
            rightDownCircle.InitEllipse(new Point(0, 0));
            rightDownCircle.BeginPathAnimation(CircleGeometry(new Point(0, 0), new Point(141, -141), 100, true, SweepDirection.Clockwise), 2);
            //左上角
            EllipseCircle leftUpCircle = new EllipseCircle();
            leftUpCircle.InitEllipse(new Point(0, 0));
            leftUpCircle.BeginPathAnimation(CircleGeometry(new Point(0, 0), new Point(-141, 141), 100, true, SweepDirection.Clockwise), 2);
            //左下角
            EllipseCircle leftDownCircle = new EllipseCircle();
            leftDownCircle.InitEllipse(new Point(0, 0));
            leftDownCircle.BeginPathAnimation(CircleGeometry(new Point(0, 0), new Point(-141, -141), 100, true, SweepDirection.Clockwise), 2);

            this.mainGrid.Children.Add(rightUpCircle);
            this.mainGrid.Children.Add(rightDownCircle);
            this.mainGrid.Children.Add(leftUpCircle);
            this.mainGrid.Children.Add(leftDownCircle);
        }

        /// <summary>
        /// 获取环形路径
        /// </summary>
        private PathGeometry CircleGeometry(Point firstPoint, Point secondPoint, double radius, bool isLargeArc, SweepDirection direction)
        {
            PathFigure pathFigure = new PathFigure { IsClosed = true };
            pathFigure.StartPoint = firstPoint;
            pathFigure.Segments.Add(
              new ArcSegment
              {
                  Point = secondPoint,
                  IsLargeArc = isLargeArc,
                  Size = new Size(radius, radius),
                  SweepDirection = direction
              });
            pathFigure.Segments.Add(
              new ArcSegment
              {
                  Point = firstPoint,
                  IsLargeArc = isLargeArc,
                  Size = new Size(radius, radius),
                  SweepDirection = direction
              });

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }
    }
}
