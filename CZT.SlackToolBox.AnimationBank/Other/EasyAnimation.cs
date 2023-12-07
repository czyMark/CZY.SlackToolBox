using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace CZY.SlackToolBox.AnimationBank.Other
{
    public static class EasyAnimation
    {
        public enum AnimationDirection { Left, Up }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="element"></param>
        public static void ShowMe(this FrameworkElement element)
        {
            //先透明化
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = new System.TimeSpan(0, 0, 0, 0, 300);
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;

            //显示
            ObjectAnimationUsingKeyFrames animationUsingKeyFrames = new ObjectAnimationUsingKeyFrames();
            DiscreteObjectKeyFrame discreteObjectKey = new DiscreteObjectKeyFrame();
            discreteObjectKey.KeyTime = new System.TimeSpan(0, 0, 0, 0, 0);
            discreteObjectKey.Value = Visibility.Visible;
            animationUsingKeyFrames.KeyFrames.Add(discreteObjectKey);

            //动画绑定
            element.BeginAnimation(FrameworkElement.OpacityProperty, doubleAnimation);
            element.BeginAnimation(FrameworkElement.VisibilityProperty, animationUsingKeyFrames);
        }
        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="element"></param>
        public static void HideMe(this FrameworkElement element)
        {

            //先显示化
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = new System.TimeSpan(0, 0, 0, 0, 300);
            doubleAnimation.From = 1;
            doubleAnimation.To = 0;

            //隐藏
            ObjectAnimationUsingKeyFrames animationUsingKeyFrames = new ObjectAnimationUsingKeyFrames();
            DiscreteObjectKeyFrame discreteObjectKey = new DiscreteObjectKeyFrame();
            discreteObjectKey.KeyTime = new System.TimeSpan(0, 0, 0, 0, 310);
            discreteObjectKey.Value = Visibility.Collapsed;
            animationUsingKeyFrames.KeyFrames.Add(discreteObjectKey);

            //动画绑定
            element.BeginAnimation(FrameworkElement.OpacityProperty, doubleAnimation);
            element.BeginAnimation(FrameworkElement.VisibilityProperty, animationUsingKeyFrames);

        }

        /// <summary>
        /// 透明闪烁
        /// </summary>
        /// <param name="element"></param>
        public static void FlickerMe(this FrameworkElement element)
        {
            DoubleAnimation flickerAnimation = new DoubleAnimation()
            {
                From = 0.5,
                To = 1,
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new TimeSpan(0, 0, 0, 1, 0),
            };
            element.BeginAnimation(FrameworkElement.OpacityProperty, flickerAnimation);
        }

        /// <summary>
        /// 发光闪烁
        /// </summary>
        /// <param name="element"></param>
        /// <param name="effectColor"></param>
        public static void EffectMe(this FrameworkElement element, Color effectColor, double shadowDepth = 0)
        {
            var effect = new DropShadowEffect() { Color = effectColor, Direction = 0, ShadowDepth = shadowDepth, BlurRadius = 0 };
            element.Effect = effect;

            DoubleAnimation effectAnimation = new DoubleAnimation()
            {
                From = 8,
                To = 1,
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new TimeSpan(0, 0, 0, 0, 800),
            };
            effect.BeginAnimation(DropShadowEffect.BlurRadiusProperty, effectAnimation);

        }

        /// <summary>
        /// 飞入
        /// </summary>
        /// <param name="element"></param>
        public static void FlyInto(this FrameworkElement element, AnimationDirection Direction)
        { 
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = new TimeSpan(0, 0, 0, 500);
            if (Direction == AnimationDirection.Left)
                doubleAnimation.To = element.Width;
            else
                doubleAnimation.To = element.Height;
            if (Direction == AnimationDirection.Left)
                element.BeginAnimation(FrameworkElement.WidthProperty, doubleAnimation);
            else
                element.BeginAnimation(FrameworkElement.HeightProperty, doubleAnimation);

        }
      
        /// <summary>
        /// 飞出
        /// </summary>
        /// <param name="element"></param>
        public static void FlyOff(this FrameworkElement element, AnimationDirection Direction)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = new TimeSpan(0, 0, 0, 500);
            doubleAnimation.To = 0;

            if (Direction == AnimationDirection.Left)
                element.BeginAnimation(FrameworkElement.WidthProperty, doubleAnimation);
            else
                element.BeginAnimation(FrameworkElement.HeightProperty, doubleAnimation);
        }

        /// <summary>
        /// 沿着路径移动
        /// </summary>
        /// <param name="element"></param>
        /// <param name="path"></param>
        public static void PathMove(this FrameworkElement element, PathGeometry path)
        {
            {
                DoubleAnimationUsingPath doubleAnimationUsingPath = new DoubleAnimationUsingPath();
                doubleAnimationUsingPath.Duration = new TimeSpan(0, 0, 0, 2);
                doubleAnimationUsingPath.PathGeometry = path;
                element.RenderTransform.BeginAnimation(TranslateTransform.XProperty, doubleAnimationUsingPath);
            }
            {
                DoubleAnimationUsingPath doubleAnimationUsingPath = new DoubleAnimationUsingPath();
                doubleAnimationUsingPath.Duration = new TimeSpan(0, 0, 0, 2);
                doubleAnimationUsingPath.PathGeometry = path;
                element.RenderTransform.BeginAnimation(TranslateTransform.YProperty, doubleAnimationUsingPath);
            }

        }

    }
}
