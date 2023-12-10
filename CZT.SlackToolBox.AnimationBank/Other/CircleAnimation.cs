using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.AnimationBank.Other
{
    public class CircleAnimation
    {
        public static event Action AnimationCompleted;
        public CircleAnimation(FrameworkElement element, double width, double height, TimeSpan timeSpan)
        {
            EllipseGeometry ellipseGeometry = new EllipseGeometry();
            ellipseGeometry.RadiusX = 0;
            ellipseGeometry.RadiusY = 0;
            double centrex = width / 2;
            double centrey = height / 2;
            ellipseGeometry.Center = new Point(centrex, centrey);
            element.Clip = ellipseGeometry; //The most important line           
            double halfWidth = width / 2;
            double halfheight = height / 2;
            DoubleAnimation a = new DoubleAnimation();
            a.Completed += new EventHandler(a_Completed);
            a.From = 0;
            a.To = Math.Sqrt(halfWidth * halfWidth + halfheight * halfheight);
            a.Duration = new Duration(timeSpan);
            ellipseGeometry.BeginAnimation(EllipseGeometry.RadiusXProperty, a);
            ellipseGeometry.BeginAnimation(EllipseGeometry.RadiusYProperty, a);

        }

        void a_Completed(object sender, EventArgs e)
        {
            if (AnimationCompleted != null)
            {
                AnimationCompleted();
            }
        }
    }
}
