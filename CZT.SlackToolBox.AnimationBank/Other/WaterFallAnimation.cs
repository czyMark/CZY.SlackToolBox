using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows;

namespace CZY.SlackToolBox.AnimationBank.Other
{
   public  class WaterFallAnimation
    {
        DispatcherTimer _timer = null;
        PathGeometry pathGeometry = null;
        double _rectangleSize = 10 + 50;
        double _offset = 5;
        int _waterFallHeight = 50;
        double _width = 0;
        double _height = 0;
        Random random = new Random();
        FrameworkElement _element = null;
        RectangleGeometry myRectGeometry2 = null;
        PathGeometry pathGeometry2 = null;
        RectangleGeometry myRectGeometry5 = null;
        public static event Action AnimationCompleted;

        public WaterFallAnimation(FrameworkElement element, double width, double height, TimeSpan timeSpan)
        {
            _element = element;
            _height = height;
            _width = width;
            myRectGeometry2 = new RectangleGeometry();
            pathGeometry2 = new PathGeometry();
            myRectGeometry5 = new RectangleGeometry();
            double steps = (_height / _offset);
            double tickTime = timeSpan.TotalSeconds / steps;
            pathGeometry = new PathGeometry();
            element.Clip = pathGeometry;
            _timer = new DispatcherTimer(DispatcherPriority.Input);
            _timer.Interval = TimeSpan.FromSeconds(tickTime);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.IsEnabled = true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            myRectGeometry2.Rect = new Rect(0, 0, _width, _rectangleSize);
            pathGeometry2 = Geometry.Combine(pathGeometry2, myRectGeometry2, GeometryCombineMode.Union, null);

            for (int i = 1; i <= _width; i = i + 2)
            {
                myRectGeometry5.Rect = new Rect(new Point(i, _rectangleSize), new Point(i + 2, _rectangleSize - random.Next(0, _waterFallHeight)));
                pathGeometry2 = Geometry.Combine(pathGeometry2, myRectGeometry5, GeometryCombineMode.Exclude, null);
            }
            _element.Clip = pathGeometry2;
            if (_rectangleSize == _height + _waterFallHeight)
            {
                _timer.IsEnabled = false;
                if (AnimationCompleted != null)
                {
                    AnimationCompleted();
                }
            }
            _rectangleSize += _offset;
        }
    }
}
