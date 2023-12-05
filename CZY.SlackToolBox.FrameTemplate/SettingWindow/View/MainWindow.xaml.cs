using CZY.SlackToolBox.FrameTemplate.SettingWindow.ViewModel;
using System.Reflection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CZY.SlackToolBox.FrameTemplate.SettingWindow.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext=new MainWindowViewModel();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView list= sender as ListView;
            FunMenuItem fun = list.SelectedItem as FunMenuItem;

            Assembly assembly = Assembly.Load("CZY.SlackToolBox.FrameTemplate");//Path
            Type type = assembly.GetType("CZY.SlackToolBox.FrameTemplate.SettingWindow.View.SettingContent");//FunName
            MainContentControl.Content = Activator.CreateInstance(type);
        }
    }
}
