using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CZY.SlackToolBox.LuckyControl.NimbleMenu
{
    /// <summary>
    /// ExpanderMenu.xaml 的交互逻辑
    /// </summary>
    public partial class ExpanderMenu : UserControl
    {
        public delegate void SelectedIndex(string ID, string NavPath, string NameSpaceName, string ContentName);
        public event SelectedIndex SelectedIndexChanged;

        public class ExpanderBar
        {
            public string ID { get; set; }
            public string Text { get; set; }
            public double Height { get; set; }
            public string IconPath { get; set; }
            public string IconName { get; set; }
            public BitmapImage Icon { get; set; }
            public Visibility Arrow { get; set; }
            public string NavPath { get; set; }
            public string NameSpaceName { get; set; }
            public string ContentName { get; set; }
            public Brush SelectedColor { get; set; } = null;
            public List<ExpanderSub> ExpanderSub { get; set; }
        }
        public class ExpanderSub
        {
            /// <summary>
            /// 编号
            /// </summary>
            public string ID { get; set; }
            /// <summary>
            /// 显示文本
            /// </summary>
            public string Text { get; set; }
            /// <summary>
            /// 高度
            /// </summary>
            public double Height { get; set; }

            /// <summary>
            /// 导航路径
            /// </summary>
            public string NavPath { get; set; }
            public string NameSpaceName { get; set; }
            public Brush SelectedColor { get; set; } = null;
            public string ContentName { get; set; }
        }
        public ExpanderMenu()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MenuItemProperty =
    DependencyProperty.Register(nameof(MenuItem), typeof(ExpanderBar), typeof(ExpanderMenu), new UIPropertyMetadata(OnMenuItemChanged));
        public ExpanderBar MenuItem
        {
            get => (ExpanderBar)GetValue(MenuItemProperty);
            set => SetValue(MenuItemProperty, value);
        }

        private static void OnMenuItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExpanderMenu control = (ExpanderMenu)d;
            control.DataContext = (ExpanderBar)e.NewValue;
        }


        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (MenuItem == null)
                return;
            //如果是第一级别返回对应的界面注册路径
            if (MenuItem.ExpanderSub == null || MenuItem.ExpanderSub.Count == 0)//没有子集的菜单，返回对应的选中路径
            {
                ExpanderBar expander = (ExpanderBar)((System.Windows.FrameworkElement)sender).DataContext;
                //PagePath
                if (SelectedIndexChanged != null)
                {
                    SelectedIndexChanged(expander.ID, expander.NavPath, expander.NameSpaceName, expander.ContentName);
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //第二级别通知界面切换
            ExpanderSub expander = (ExpanderSub)((System.Windows.FrameworkElement)sender).DataContext;
            //PagePath
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(expander.ID, expander.NavPath, expander.NameSpaceName, expander.ContentName);
            }
        }
    }
}
