using LaunchMoreApp.Models;
using LaunchMoreApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LaunchMoreApp
{
    /// <summary>
    /// AllSoftWin.xaml 的交互逻辑
    /// </summary>
    public partial class AllSoftWin : Window
    {
        public AllSoftWin()
        {
            InitializeComponent();

            ResultAll res = Data.getAllSoft();
            if (res.code == 1)
            {
                AllSoft.ItemsSource= res.data;
            }
        }

        private void AllSoft_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header=e.Row.GetIndex()+1;
        }
    }
}
