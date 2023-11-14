using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CZY.SlackToolBox.LuckyControl.CoreConvert
{
    //[ValueConversion(typeof(String), typeof(ImageSource))]
    public class ResourceKeyToImageConvert : IValueConverter
    {
        //当值从绑定源传播给绑定目标时，调用方法Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ((System.Windows.Media.ImageBrush)Application.Current.Resources.MergedDictionaries[3]["SystemMenuImageHover"]).ImageSource;
            }
            //返回图像
            return ((System.Windows.Media.ImageBrush)Application.Current.Resources.MergedDictionaries[3][value]).ImageSource;
        }
        //当值从绑定目标传播给绑定源时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //触发数据绑定
            return DependencyProperty.UnsetValue;
        }
    }
}
