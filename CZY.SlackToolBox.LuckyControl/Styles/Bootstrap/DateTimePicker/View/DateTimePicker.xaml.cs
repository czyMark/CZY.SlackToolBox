using System;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace CZY.SlackToolBox.LuckyControl.Styles.Bootstrap
{

    [ToolboxBitmap(typeof(DateTimePicker), "DateTimePicker.bmp")]  
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>    
    public partial class DateTimePicker : UserControl
    {
        #region 属性

        public DateTime? DateTime { get; set; }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(DateTimePicker), new PropertyMetadata("请选择时间"));


        #endregion
        public DateTimePicker()
        {
            InitializeComponent();
            this.DataContext= this;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public DateTimePicker(string txt)
            : this()
        {
           // this.textBox1.Text = txt;
        
        }

        #region 事件

        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, RoutedEventArgs e)
        {    
            if (popChioce.IsOpen == true)
            {
                popChioce.IsOpen = false;
            }

            TDateTimeView dtView = new TDateTimeView(textBlock1.Text);// TDateTimeView  构造函数传入日期时间
            dtView.DateTimeOK += (dateTimeStr) => //TDateTimeView 日期时间确定事件
            {

                textBlock1.Text = dateTimeStr;
                PlaceholderTxt.Visibility = Visibility.Hidden;
                DateTime = Convert.ToDateTime(dateTimeStr);
                popChioce.IsOpen = false;//TDateTimeView 所在pop  关闭

            };

            popChioce.Child = dtView;
            popChioce.IsOpen = true;
        } 
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DateTime == null)
            {
                PlaceholderTxt.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock1.Text = DateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");//"yyyyMMddHHmmss"
            }
            //DateTime dt = DateTime.Now;
            //textBlock1.Text = dt.ToString("yyyy/MM/dd HH:mm:ss");//"yyyyMMddHHmmss"
            //DateTime = dt;            
        }

        #endregion


        private void textBlock1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            iconButton1_Click(null,null);
        }
    }
}
