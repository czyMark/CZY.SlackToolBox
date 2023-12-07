using CZY.SlackToolBox.FrameTemplate.YXKJ.Core;
using CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel;
using System.Windows.Media.Animation;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.View
{
    /// <summary>
    /// 列表界面 的交互逻辑
    /// </summary>
    public partial class DataListContent : FrameControl
    {
        public DataListContent()
        {
            InitializeComponent();
            this.DataContext = new DataListContentViewModel();
        }

        private void SearchPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //验证SearchMask是否显示
            if (SearchMask.Visibility == Visibility)
            {
                //寻找显示的动画资源
                Storyboard sb = (Storyboard)FindResource("HidenSearchPanel");
                //执行动画
                sb.Begin();
            }
            else
            { 
                //寻找显示的动画资源
                Storyboard sb = (Storyboard)FindResource("ExpanderSearchPanel");
                //执行动画
                sb.Begin();
            }
        }
    }
}
