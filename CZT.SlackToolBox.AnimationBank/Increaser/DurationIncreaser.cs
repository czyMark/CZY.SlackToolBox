using System;
using System.Windows;

namespace CZY.SlackToolBox.AnimationBank.Increaser
{
    public class DurationIncreaser : DependencyObject
    {
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(Duration), typeof(DurationIncreaser),
                new PropertyMetadata(new Duration(TimeSpan.Zero)));

        private Duration _current = new Duration(TimeSpan.Zero);

        public  Duration Next
        {
            get
            {
                var result = Start + _current;
                _current += Step;
                return result;
            }
        }

        public Duration Step
        {
            get => (Duration)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }
        public Duration Start
        {
            get => (Duration)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }
    }
}
