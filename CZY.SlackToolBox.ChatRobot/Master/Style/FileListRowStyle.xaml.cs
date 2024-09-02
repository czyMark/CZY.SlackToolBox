using System.Windows.Controls;

namespace CZY.SlackToolBox.ChatRobot.Master.Style
{
    /// <summary>
    /// FileListRowStyle.xaml 的交互逻辑
    /// </summary>
    public partial class FileListRowStyle : UserControl
    {
        public class FileListModel
        {
            public string Icon { get; set; }
            public string Title { get; set; }
            public string Detail { get; set; }
            public string Time { get; set; }
        }
        public FileListRowStyle(FileListModel DataV)
        {
            InitializeComponent();
            //this.Icon = DataV.Icon;
            this.Title.Text = DataV.Title; 
            this.Detail.Text = DataV.Detail;
            this.Time.Text = DataV.Time;

        }
    }
}
