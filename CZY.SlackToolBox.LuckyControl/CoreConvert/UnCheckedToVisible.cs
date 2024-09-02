using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CZY.SlackToolBox.LuckyControl.CoreConvert
{
    public class UnCheckedToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = (bool)value;
            if (t == true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
