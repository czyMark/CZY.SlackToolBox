using CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.View
{
    /// <summary>
    /// DataEditContent.xaml 的交互逻辑
    /// </summary>
    public partial class DataEditContent : UserControl
    {
        public DataEditContent(DataEditContentViewModel dataEditContentViewModel)
        {
            InitializeComponent();   
            this.DataContext = dataEditContentViewModel;
        }
    }
}
