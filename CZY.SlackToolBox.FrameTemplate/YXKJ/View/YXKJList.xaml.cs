using CZY.SlackToolBox.AnimationBank.Other;
using CZY.SlackToolBox.FrameTemplate.YXKJ.Core;
using CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.View
{
    /// <summary>
    /// 列表界面 的交互逻辑
    /// </summary>
    public partial class YXKJList : FrameControl
    {
        public YXKJList()
        {
            InitializeComponent();
            this.DataContext = new YXKJListViewModel();

        }
         
    }
}
