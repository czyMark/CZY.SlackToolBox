using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Resources;

namespace CZY.SlackToolBox.LuckyControl.IconResource
{
    public class IconFont : Label
    {
        Dictionary<string, Viewbox> CacheIcon = new Dictionary<string, Viewbox>();


        public string IconName
        {
            get { return (string)GetValue(IconNameProperty); }
            set { SetValue(IconNameProperty, value); }
        }

        public static readonly DependencyProperty IconNameProperty = DependencyProperty.Register(
         "IconName",
         typeof(string),
         typeof(IconFont), new PropertyMetadata(IconNameChanged));
        private static void IconNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as IconFont).ReferenControls();
        }


        public string IconPath
        {
            get { return (string)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }

        public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(
         "IconPath",
         typeof(string),
         typeof(IconFont), new PropertyMetadata(IconPathChanged));
        private static void IconPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //(d as IconFont).ReferenControls();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ReferenControls();
        }
        public IconFont()
        {
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.HorizontalContentAlignment = HorizontalAlignment.Center;
            this.Padding = new Thickness(0);
            this.Margin = new Thickness(0);
            IconPath = "/CZY.SlackToolBox.LuckyControl;component/IconResource/Icon/";

        }
         

        public void ReferenControls()
        { 
            var key = $"{IconPath}{IconName}.xaml";
            if (CacheIcon.ContainsKey(key))
            {
                this.Content = CacheIcon[key];
            }
            else
            {
                StreamResourceInfo info = Application.GetResourceStream(new Uri(key, UriKind.Relative));
                using (var stream = info.Stream)
                {
                    Viewbox page = (Viewbox)XamlReader.Load(info.Stream);
                    this.Content = page;
                    CacheIcon.Add(key, page);
                }
            }
        }
    }
}
