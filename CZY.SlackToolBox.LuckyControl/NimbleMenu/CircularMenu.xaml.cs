using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CZY.SlackToolBox.LuckyControl.NimbleMenu
{
    /// <summary>
    /// CircularMenu.xaml 的交互逻辑
    /// </summary>
    public partial class CircularMenu : UserControl
    {

        public delegate void selectedItemChangeDelegate(MenuModel item);

        public event selectedItemChangeDelegate SelectedItemChange;



        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(MenuModel), typeof(CircularMenu));
        public MenuModel SelectedItem
        {
            get => (MenuModel)GetValue(SelectedItemProperty);
            private set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty DataSorceProperty =
            DependencyProperty.Register(nameof(DataSorce), typeof(List<MenuModel>), typeof(CircularMenu), new UIPropertyMetadata(OnDataSorceChanged));
        public List<MenuModel> DataSorce
        {
            get => (List<MenuModel>)GetValue(DataSorceProperty);
            set => SetValue(DataSorceProperty, value);
        }

        private static void OnDataSorceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularMenu control = (CircularMenu)d;
            control.MainMenu.ItemsSource = control.DataSorce;
        }



        public static readonly DependencyProperty ExtendMenuProperty =
            DependencyProperty.Register(nameof(ExtendMenu), typeof(bool), typeof(CircularMenu), new UIPropertyMetadata(OnExtendMenuChanged));
        public bool ExtendMenu
        {
            get => (bool)GetValue(ExtendMenuProperty);
            set => SetValue(ExtendMenuProperty, value);
        }

        private static void OnExtendMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircularMenu control = (CircularMenu)d;
            control.CoreBtn.IsChecked = control.ExtendMenu;
        }


        public CircularMenu()
        {
            InitializeComponent();
        }
         

        private void CircularMenuItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CircularMenuItem circularMenuItem= sender as CircularMenuItem;
            if (circularMenuItem != null)
            {
                //当前项为选中项
                SelectedItem = (MenuModel)circularMenuItem.DataContext;
                if (SelectedItemChange != null)
                {
                    SelectedItemChange(SelectedItem);
                }
            }
        }
    }
}
