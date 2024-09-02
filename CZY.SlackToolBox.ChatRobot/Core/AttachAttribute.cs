using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CZY.SlackToolBox.ChatRobot.Core
{
    public class AttachAttribute : Control
    {
        public static DependencyProperty FocusBorderBrushProperty =
            DependencyProperty.Register("FocusBorderBrush", typeof(Brush), typeof(RichTextBox),
       null); //属性默认值

        public Brush FocusBorderBrush
        {
            get { return (Brush)GetValue(FocusBorderBrushProperty); }
            set { SetValue(FocusBorderBrushProperty, value); }
        }


        public static DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.Register("MouseOverBorderBrush", typeof(Brush), typeof(RichTextBox),
       null); //属性默认值

        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }
         
          

        public static readonly DependencyProperty DropDownIconProperty =
           DependencyProperty.RegisterAttached("DropDownIcon", typeof(string), typeof(AttachAttribute), 
               new PropertyMetadata("", new PropertyChangedCallback(OnDropDownIconChanged)));

        private static void OnDropDownIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public static string GetDropDownIcon(DependencyObject obj)
        {
            return (string)obj.GetValue(DropDownIconProperty);
        }

        public static void SetDropDownIcon(DependencyObject obj, string value)
        {
            obj.SetValue(DropDownIconProperty, value);
        }

    }
}
