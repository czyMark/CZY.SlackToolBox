using CZY.SlackToolBox.FrameTemplate.AirportCS.Core;
using CZY.SlackToolBox.FrameTemplate.AirportCS.ViewModel;
using System.Windows.Controls;

namespace CZY.SlackToolBox.FrameTemplate.AirportCS.View
{
    /// <summary>
    /// 列表界面 的交互逻辑
    /// </summary>
    public partial class HomeContent :UserControl
    {
        public HomeContent()
        {
            InitializeComponent();
            this.DataContext = new HomeContentViewModel();
        }
         
    }
}
