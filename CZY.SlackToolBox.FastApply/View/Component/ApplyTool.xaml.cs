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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CZY.SlackToolBox.FastApply.View.Component
{
    /// <summary>
    /// ApplyTool.xaml 的交互逻辑
    /// </summary>
    public partial class ApplyTool : UserControl
    {
        public static ApplyTool Instance = new ApplyTool();
        public ApplyTool()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            if (btn == null) { return; }
            switch (btn.Content)
            {
                case "截图":
                    {
                        CaptureApply.MainWindow win = new CaptureApply.MainWindow();
                        win.Show();
                    }
                    break;
                case "软件多开":
                    {
                        LaunchMoreApp.MainWindow win = new LaunchMoreApp.MainWindow();
                        win.Show();
                    }
                    break;
            }
        }
    }
}
