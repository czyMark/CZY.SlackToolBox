using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.AnimationBank.Other
{
    public static class ShakeAnimation
    {
        public enum Direction
        {
            Left,
            Up,
            Right,
            Down,
            Center
        }

        /// <summary>
        /// 窗体抖动 只有在窗体显示出来后才能使用
        /// </summary>
        /// <param name="window">窗口，如果等于null抖动激活的窗体</param>
        public static void WindowShake(this Window window , Direction direction= Direction.Left)
        {
            if (window == null)
                if (Application.Current.Windows.Count > 0)
                    window = Application.Current.Windows.OfType<Window>().FirstOrDefault(o => o.IsActive);
            double position = 0;
            switch (direction)
            {
                case Direction.Left:
                    position = window.Left;
                    break;
                case Direction.Up:
                    position = window.Top;
                    break;
                case Direction.Right:
                    position = window.Width;
                    break;
                case Direction.Down:
                    position = window.Height;
                    break; 
            }
            var doubleAnimation = new DoubleAnimation
            {
                From = position,
                To = position + 15,
                Duration = TimeSpan.FromMilliseconds(50),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(5),
                FillBehavior = FillBehavior.Stop
            };
            window.BeginAnimation(Window.LeftProperty, doubleAnimation);
        }

        /// <summary>
        /// 控件抖动
        /// </summary>
        /// <param name="control">控件，如果等于null抖动激活的窗体</param>
        public static void ControlShake(this FrameworkElement control , Direction direction = Direction.Left)
        {
            if (control == null)
                if (Application.Current.Windows.Count > 0)
                    control = Application.Current.Windows.OfType<Window>().FirstOrDefault(o => o.IsActive);

            double position = 0;
            switch (direction)
            {
                case Direction.Left:
                    position = control.Margin.Left;
                    break;
                case Direction.Up:
                    position = control.Margin.Top;
                    break;
                case Direction.Right:
                    position = control.Width;
                    break;
                case Direction.Down:
                    position = control.Height;
                    break;
            }

            var doubleAnimation = new DoubleAnimation
            {
                From = position,
                To = position + 15,
                Duration = TimeSpan.FromMilliseconds(50),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(3),
                FillBehavior = FillBehavior.Stop
            };
            control.BeginAnimation(Window.LeftProperty, doubleAnimation);
        }


        /// <summary>
        /// 控件抖动
        /// </summary>
        /// <param name="control">控件，如果等于null抖动激活的窗体</param>
        public static void ControlWobble(this FrameworkElement control , Direction direction = Direction.Left)
        {
            if (control == null)
                if (Application.Current.Windows.Count > 0)
                    control = Application.Current.Windows.OfType<Window>().FirstOrDefault(o => o.IsActive);
            if (direction == Direction.Center)
            {
                direction=Direction.Left;
            }
            VisualStateManager.GoToState(control, Direction.Center.ToString(), false);
            VisualStateManager.GoToState(control, direction.ToString(), true);
        }
    }
}
