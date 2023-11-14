using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.AnimationBank
{
    public static class RainText
    {
        public static FontFamily FontFamily = new FontFamily("宋体");
        public static Brush Foreground = Brushes.Black;
        public static int FontSize = 16;

        /// <summary>
        /// 充满控件的文字雨
        /// </summary>
        /// <param name="text"></param>
        /// <param name="playground">文字雨容器，Orientation="Horizontal"从上往下显示文字雨</param>
        public static void RainFullText(this string text, StackPanel playground)
        {
            var textCollection = new List<TextBlock>();


            var rnd = new Random();

            var baseAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever
            };
            //初始化好文字的位置 一共是150列 
            for (int columnindex = 0; columnindex < 150; columnindex++)
            {
                //定义每列间隔
                var column = new StackPanel();
                column.VerticalAlignment = VerticalAlignment.Stretch;
                column.HorizontalAlignment = HorizontalAlignment.Left;
                column.Margin = new Thickness(2, 0, 2, 0);

                //随机间隔
                var waitTIme = rnd.Next(1000, 8000);
                //每列随机10到200个字符
                for (int i = 0; i < rnd.Next(10, 200); i++)
                {
                    //随机获得文字
                    var txt = new TextBlock()
                    {
                        Text = text[rnd.Next(0, text.Length)].ToString(),
                        FontSize = FontSize,
                        FontFamily = FontFamily,
                        Foreground = Foreground,
                        Opacity = 0
                    };
                    var animation = baseAnimation.Clone();
                    //每个文字的动画间隔
                    animation.BeginTime = TimeSpan.FromMilliseconds(i * 100 + waitTIme);
                    txt.Tag = animation;
                    column.Children.Add(txt);
                    textCollection.Add(txt);
                }
                playground.Children.Add(column);
            }
            //让每个文字都执行动画并且一直执行。
            foreach (var item in textCollection)
            {
                var animation = item.Tag as DoubleAnimation;
                item.BeginAnimation(System.Windows.UIElement.OpacityProperty, animation);
            }
        }


        public static void RainCloumenText(this string text, StackPanel playground)
        {
            var column = new StackPanel();
            var textCollection = new List<TextBlock>();
            column.VerticalAlignment = VerticalAlignment.Stretch;
            column.HorizontalAlignment = HorizontalAlignment.Left;
            var textStr = text;
            var rnd = new Random();
            var baseAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever
            };
            var waitTIme = rnd.Next(1000, 8000);
            for (int i = 0; i < 250; i++)
            {
                var txt = new TextBlock()
                {
                    Text = textStr[rnd.Next(0, textStr.Length)].ToString(),
                    FontSize = FontSize,
                    FontFamily = FontFamily,
                    Foreground = Foreground,
                    Opacity = 0
                };
                var animation = baseAnimation.Clone();
                animation.BeginTime = TimeSpan.FromMilliseconds(i * 100 + waitTIme);
                txt.Tag = animation;
                column.Children.Add(txt);
                textCollection.Add(txt);
            }
            playground.Children.Add(column);
            foreach (var item in textCollection)
            {
                var animation = item.Tag as DoubleAnimation;
                item.BeginAnimation(System.Windows.UIElement.OpacityProperty, animation);
            }
        }

        public static void RainOneText(this string text, StackPanel playground)
        {
            var column = new StackPanel();
            column.VerticalAlignment = VerticalAlignment.Stretch;
            column.HorizontalAlignment = HorizontalAlignment.Left;
            var txt = new TextBlock() { Text = text, FontSize = 16, FontFamily = FontFamily, Foreground = Foreground, Opacity = 0 };
            var baseAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(2)
            };
            column.Children.Add(txt);
            playground.Children.Add(column);
            txt.BeginAnimation(System.Windows.UIElement.OpacityProperty, baseAnimation);
        }
    }
}
