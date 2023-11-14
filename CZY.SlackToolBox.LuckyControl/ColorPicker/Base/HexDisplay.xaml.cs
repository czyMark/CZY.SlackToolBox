using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CZY.SlackToolBox.LuckyControl.ColorPicker
{
    /// <summary>
    /// Interaction logic for HexDisplay.xaml
    /// </summary>
    public partial class HexDisplay : UserControl
    {

        public enum EAlphaByteVisibility
        {
            visible,
            hidden, 
            auto //show if Alpha byte not ff
        }
        public static Type ClassType
        {
            get { return typeof(HexDisplay); }
        }
        public event EventHandler<EventArgs<Color>> ColorChanged;

        #region Color

        public static DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), ClassType,
             new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnColorChanged));

        [ Category("ColorPicker")]
        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (Color)e.NewValue;
            var rd = (HexDisplay)d;
            rd.OnColorChanged(c);
        }

        private void OnColorChanged(Color c)
        {
            string colorText = "";

            if (IsNumberSignIncludedInText)
            {
                colorText="#";
            }
           switch (AlphaByteVisibility )
           {
               case EAlphaByteVisibility.visible:
                    colorText += c.ToString().Substring(1);
                   break;
               case EAlphaByteVisibility.hidden :
                    colorText += c.ToString().Substring(3);
                   break;
               case EAlphaByteVisibility.auto :
                   break;
           }


            txtHex.Text = colorText;
            if (ColorChanged != null)
            {
                ColorChanged(this, new EventArgs<Color>(c));
            }
        }

        #endregion

        #region Text

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), ClassType, new PropertyMetadata("", new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var display = (HexDisplay) d;
            var oldtext = (string) e.OldValue;
            var newText = (String) e.NewValue; 
        }

        #endregion

        #region IsNumberSignIncludedInText

        public static DependencyProperty IsNumberSignIncludedInTextProperty = DependencyProperty.Register("IsNumberSignIncludedInText", typeof(bool), ClassType, 
            new PropertyMetadata(false, OnIsNumberSignIncludedInTextChanged));

         [Category("ColorPicker")]
        public bool IsNumberSignIncludedInText
        {
            get
            {
                return (bool)GetValue(IsNumberSignIncludedInTextProperty);
            }
            set
            {
                SetValue(IsNumberSignIncludedInTextProperty, value);
            }
        }

        private static void OnIsNumberSignIncludedInTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region AlphaByteVisibility

        public static DependencyProperty AlphaByteVisibilityProperty = DependencyProperty.Register("AlphaByteVisibility", typeof(EAlphaByteVisibility), ClassType,
            new PropertyMetadata(EAlphaByteVisibility.hidden, OnAlphaByteVisibilityChanged));
         [Category("ColorPicker")]
        public EAlphaByteVisibility AlphaByteVisibility
        {
            get
            {
                return (EAlphaByteVisibility)GetValue(AlphaByteVisibilityProperty);
            }
            set
            {
                SetValue(AlphaByteVisibilityProperty, value);
            }
        }

        private static void OnAlphaByteVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion



        public HexDisplay()
        {
            InitializeComponent();
        }

        private void txtHex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //将文本设置
                this.Color = HexToColor("#" + txtHex.Text);
            }
        }

        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="color">带#号的16进制颜色</param>
        /// <returns></returns>
        public  Color HexToColor( string color)
        {
            int red, green, blue, alpha;
            char[] rgb;
            color = color.TrimStart('#');
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            switch (color.Length)
            {
                case 3:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
                    green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
                    blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
                    return Color.FromRgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

                case 4:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
                    green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
                    blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
                    alpha = Convert.ToInt32(rgb[3].ToString() + rgb[3].ToString(), 16);
                    return Color.FromArgb(Convert.ToByte(alpha), Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

                case 6:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
                    return Color.FromRgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

                case 8:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
                    alpha = Convert.ToInt32(rgb[6].ToString() + rgb[7].ToString(), 16);
                    return Color.FromArgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue), Convert.ToByte(alpha));
                default:
                    return Colors.Black;

            }
        }
    }
}
