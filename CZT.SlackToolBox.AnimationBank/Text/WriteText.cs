using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.AnimationBank
{
    public static class WriteText
    {
        /// <summary>
        /// 模拟打字的效果-如果要垂直显示调整TextBlock相关属性
        /// </summary>
        /// <param name="textToAnimate">所有要打的文字</param>
        /// <param name="txt">用于承载的容器</param>
        /// <param name="timeSpan">打字一共的耗时</param>
        public static void PlayWriteTextblock(this string text, TextBlock txt, TimeSpan timeSpan)
        {
            Storyboard story = new Storyboard();
            story.FillBehavior = FillBehavior.HoldEnd;
            story.RepeatBehavior = RepeatBehavior.Forever;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            stringAnimationUsingKeyFrames.Duration = new Duration(timeSpan);

            //关键帧动画 一帧输出之前帧输出的文字  (缺少1帧的bug,所以文字要多一个)
            string tmp = string.Empty;
            foreach (char c in text)
            {
                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                tmp += c;
                discreteStringKeyFrame.Value = tmp;
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }

            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, txt.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(TextBlock.TextProperty));
            story.Children.Add(stringAnimationUsingKeyFrames);
            story.Begin(txt);
        }
    }
}
