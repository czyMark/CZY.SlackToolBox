using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.LuckyControl.NotifyWindow
{
    /// <summary>
    /// NotifyPanel.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyPanel : UserControl
    {
        System.Timers.Timer timerClean;
        object CleanLock = new object();
        public NotifyPanel()
        {
            InitializeComponent();
            timerClean = new System.Timers.Timer();
            timerClean.Interval = 3000;
            timerClean.Elapsed += TimerClean_Elapsed;
            timerClean.Start();
        }

        private void TimerClean_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //清除透明度为0的panel
            lock (CleanLock)
            {
                this.Dispatcher.Invoke(new Action(() =>
                             {
                                 for (int i = 0; i < mainNotify.Children.Count; i++)
                                 {
                                     if (mainNotify.Children[i].Opacity == 0)
                                     { 
                                         mainNotify.Children.RemoveAt(i);
                                         i = i - 1;
                                     }
                                 }
                             }));
            }
        }

        public void AddTipPanel(UserControl tipPanel)
        {
            DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
            LinearDoubleKeyFrame linearDoubleKeyFrameTime1 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameTime2 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame linearDoubleKeyFrameTime3 = new LinearDoubleKeyFrame();

            linearDoubleKeyFrameTime1.KeyTime = new TimeSpan(0, 0, 0, 0);
            linearDoubleKeyFrameTime1.Value = 1;

            linearDoubleKeyFrameTime2.KeyTime = new TimeSpan(0,0,0,3);
            linearDoubleKeyFrameTime2.Value = 0.9;


            linearDoubleKeyFrameTime3.KeyTime = new TimeSpan(0, 0, 0, 3, 800);
            linearDoubleKeyFrameTime3.Value = 0;

            doubleAnimationUsingKeyFrames.KeyFrames.Add(linearDoubleKeyFrameTime1);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(linearDoubleKeyFrameTime2);
            doubleAnimationUsingKeyFrames.KeyFrames.Add(linearDoubleKeyFrameTime3);

            tipPanel.Padding = new System.Windows.Thickness(3);
            mainNotify.Children.Add(tipPanel);

            doubleAnimationUsingKeyFrames.Duration = new TimeSpan(0, 0, 0, 3, 800);
            tipPanel.BeginAnimation(UserControl.OpacityProperty, doubleAnimationUsingKeyFrames);
        }
         
    }
}
