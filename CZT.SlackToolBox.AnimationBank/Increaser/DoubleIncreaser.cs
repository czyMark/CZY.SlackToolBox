using System.Windows;

namespace CZY.SlackToolBox.AnimationBank.Increaser
{
    public class DoubleIncreaser : DependencyObject
    {
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(double), typeof(DoubleIncreaser),
                new PropertyMetadata(default(double)));

        private double _current;

        public double Next
        {
            get
            {
                var result = Start + _current;
                _current += Step;
                return result;
            }
        }

        public double Step
        {
            get => (double)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }
        public double Start
        {
            get => (double)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }
    }
}
