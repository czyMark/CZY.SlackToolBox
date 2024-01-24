using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CZY.SlackToolBox.ChatRobot.ControlConvert
{
    public class CheckedToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = (bool)value;
            if (t == true)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
