using CZY.SlackToolBox.FastExtend;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using static CZY.SlackToolBox.LuckyControl.NimbleMenu.ExpanderMenu;

namespace CZY.SlackToolBox.FrameTemplate.AirportCS.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// wpf 接口继承
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        #region 属性

        /// <summary>
        /// 用户名
        /// </summary>
        private List<ExpanderBar> menuList;
        public List<ExpanderBar> MenuList
        {
            get
            {
                return menuList;
            }
            set
            {
                menuList = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MenuList"));
                }
            }
        }
        #endregion
         

        public MainWindowViewModel()
        {
             
            MenuList = new List<ExpanderBar>();
            int j = 0;
            for (int i = 0; i < 5; i++)
            {
                var expanderBar = new ExpanderBar()
                {
                    ID = "1",
                    Arrow = Visibility.Visible,
                    Text = "测试页面" + i,
                    IconName = "Setting",
                    IconPath = "/CZY.SlackToolBox.LuckyControl;component/IconResource/IconFont/",
                    Height = 40,
                    SelectedColor = new SolidColorBrush(Colors.Transparent),
                    NavPath = "" + j++,
                    ExpanderSub = new List<ExpanderSub>()
                 {
                  new ExpanderSub(){
                  ID = "2",
                   Text ="首页0"+(i),
                    Height =40,
                    SelectedColor=new SolidColorBrush(("#FF007BFF").HexToColor()),
                    NavPath =""+j++
                  },
                    new ExpanderSub(){
                  ID = "2",
                   Text ="首页0"+(i+1),
                    Height =40,
                    SelectedColor=new SolidColorBrush(("#FF007BFF").HexToColor()),
                    NavPath =""+j++
                  }
                 }
                };
                MenuList.Add(expanderBar);
            }
        }
    }
}
