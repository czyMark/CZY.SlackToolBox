using System;
using System.Windows;

namespace CZY.SlackToolBox.AnimationBank.Increaser
{
    public class TimeSpanIncreaser : DependencyObject
    {
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(TimeSpan), typeof(TimeSpanIncreaser),
                new PropertyMetadata(default(TimeSpan)));

        private TimeSpan _current;

        public TimeSpan Next
        {
            get
            {
                var result = Start + _current;
                _current += Step;
                return result;
            }
        }

        public TimeSpan Step
        {
            get => (TimeSpan)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }
        public TimeSpan Start
        {
            get => (TimeSpan)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }
    }
}
