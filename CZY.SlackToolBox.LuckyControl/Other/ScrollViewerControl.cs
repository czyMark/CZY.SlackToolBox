﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.LuckyControl.Other
{
    public class ScrollViewerControl : ScrollViewer
    {
        //记录上一次的滚动位置
        private double LastLocation;

        //重写鼠标滚动事件
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            double WheelChange = e.Delta;
            //可以更改一次滚动的距离倍数 (WheelChange可能为正负数!)
            var newOffset = LastLocation - WheelChange * 2;
            //Animation并不会改变真正的VerticalOffset(只是它的依赖属性) 所以将VOffset设置到上一次的滚动位置 (相当于衔接上一个动画)
            ScrollToVerticalOffset(LastLocation);
            //碰到底部和顶部时的处理
            if (newOffset < 0)
                newOffset = 0;
            if (newOffset > ScrollableHeight)
                newOffset = ScrollableHeight;

            AnimateScroll(newOffset);
            LastLocation = newOffset;
            //告诉ScrollViewer我们已经完成了滚动
            e.Handled = true;
        }

        private void AnimateScroll(double ToValue)
        {
            //为了避免重复，先结束掉上一个动画
            BeginAnimation(ScrollViewerAttach.VerticalOffsetProperty, null);
            var Animation = new DoubleAnimation();
            Animation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut };
            Animation.From = VerticalOffset;
            Animation.To = ToValue;
            //动画速度
            Animation.Duration = TimeSpan.FromMilliseconds(800);
            //考虑到性能，可以降低动画帧数
            //Timeline.SetDesiredFrameRate(Animation, 40);
            BeginAnimation(ScrollViewerAttach.VerticalOffsetProperty, Animation);
        }
    }


    public static class ScrollViewerAttach
    {
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(ScrollViewerAttach),
                new UIPropertyMetadata(0.0, OnVerticalOffsetChanged));

        public static void SetVerticalOffset(FrameworkElement target, double value)
        {
            target.SetValue(VerticalOffsetProperty, value);
        }

        public static double GetVerticalOffset(FrameworkElement target)
        {
            return (double)target.GetValue(VerticalOffsetProperty);
        }

        private static void OnVerticalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            (target as ScrollViewer)?.ScrollToVerticalOffset((double)e.NewValue);
        }
    }
}
