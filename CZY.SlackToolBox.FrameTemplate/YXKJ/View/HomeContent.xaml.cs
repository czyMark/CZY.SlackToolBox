using CZY.SlackToolBox.FrameTemplate.YXKJ.Core;
using CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.View
{
    /// <summary>
    /// 列表界面 的交互逻辑
    /// </summary>
    public partial class HomeContent : FrameControl
    {
        public HomeContent()
        {
            InitializeComponent();
            this.DataContext = new HomeContentViewModel();
        }
         
    }
}
