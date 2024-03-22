using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace CZY.SlackToolBox.LuckyControl.Input.UploadFile
{
    /// <summary>
    /// UploadPic.xaml 的交互逻辑
    /// </summary>
    public partial class UploadPic : UserControl
    {
        public UploadPic()
        {
            InitializeComponent();
            this.DataContext = Capture;
        }
        public ObservableCollection<string> Capture = new ObservableCollection<string>();
        private void UpLoadCaptrue_Click(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog logoSelected = new OpenFileDialog();
            logoSelected.Filter = "图片|*.jpg;*.png;*.bmp;*.gif";
            if (logoSelected.ShowDialog() == true)
            {
                Capture.Add(logoSelected.FileName);
            }
        }

        /// <summary>
        /// 移除截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReMoveCaptrue_Click(object sender, MouseButtonEventArgs e)
        {
            Capture.Remove(((Image)sender).DataContext.ToString());
        }
    }
}
