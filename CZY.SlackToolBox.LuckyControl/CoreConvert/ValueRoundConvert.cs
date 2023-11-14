using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CZY.SlackToolBox.LuckyControl.CoreConvert
{
    //[ValueConversion(typeof(String), typeof(ImageSource))]
    public class ValueRoundConvert : IValueConverter
    {
        //当值从绑定源传播给绑定目标时，调用方法Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return 0.0;
            }
            double d;
            if (double.TryParse(value.ToString(), out d))
            {
                return Math.Round(d, 1);
            }
            return 0.0;
            //返回图像
        }
        //当值从绑定目标传播给绑定源时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //触发数据绑定
            return DependencyProperty.UnsetValue;
        }
    }
}
