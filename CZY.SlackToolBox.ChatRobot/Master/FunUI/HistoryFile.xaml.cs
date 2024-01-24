using System.Windows.Controls;
using CZY.SlackToolBox.ChatRobot.Master.Style;

namespace CZY.SlackToolBox.ChatRobot.Master.FunUI
{
    /// <summary>
    /// HistoryFile.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryFile : UserControl
    {
        public HistoryFile()
        {
            InitializeComponent();

            Recommend.Children.Add(new FileListRowStyle(new FileListRowStyle.FileListModel()
            {
                Title = "于一科技产品的七麦采集",
                Time = "2024年01月09日20:37:02",
                Detail = "用户/桌面/2024-01-09于一科技产品的数据采集"
            }));


            Recommend.Children.Add(new FileListRowStyle(new FileListRowStyle.FileListModel()
            {
                Title = "小宝宝的衣服京东购买最近最低价对比采集",
                Time = "2024年01月09日20:37:02",
                Detail = "用户/桌面/小宝宝的衣服京东购买最近最低价对比采集"
            }));


            Recommend.Children.Add(new FileListRowStyle(new FileListRowStyle.FileListModel()
            {
                Title = "AJ 8淘宝最近最低售价对比采集",
                Time = "2024年01月09日20:37:02",
                Detail = "用户/桌面/AJ 8淘宝最近最低售价对比"
            }));


            //测试添加数据。
            for (int i = 0; i < 50; i++)
            {
                Recommend.Children.Add(new FileListRowStyle(new FileListRowStyle.FileListModel()
                {
                    Title = "AJ 8淘宝最近最低售价对比采集",
                    Time = "2024年01月09日20:37:02",
                    Detail = $"用户/桌面/AJ 8淘宝最近最低售价对比{i}"
                }));
            }
        }
         
    }
}
