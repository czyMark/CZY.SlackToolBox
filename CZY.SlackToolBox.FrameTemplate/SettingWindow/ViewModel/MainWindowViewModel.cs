using CZY.SlackToolBox.FastExtend;
using CZY.SlackToolBox.FrameTemplate.YXKJ.View;
using CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel;
using CZY.SlackToolBox.LuckyControl;
using CZY.SlackToolBox.LuckyControl.MessageNotify;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using static CZY.SlackToolBox.LuckyControl.NimbleMenu.ExpanderMenu;

namespace CZY.SlackToolBox.FrameTemplate.SettingWindow.ViewModel
{
    public class FunMenuItem
    {
        public string IconName { get; set; }
        public string TextName { get; set; }
        public string Path { get; set; }
        public string FunName { get; set; }
    }
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// wpf 接口继承
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        #region 属性

        /// <summary>
        /// 功能菜单
        /// </summary>
        private List<FunMenuItem> funList;
        public List<FunMenuItem> FunList
        {
            get
            {
                return funList;
            }
            set
            {
                funList = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FunList"));
                }
            }
        }




        /// <summary>
        /// 版本号
        /// </summary>
        private string versionNumber;
        public string VersionNumber
        {
            get
            {
                return versionNumber;
            }
            set
            {
                versionNumber = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("VersionNumber"));
                }
            }
        }
        #endregion

         


        public MainWindowViewModel()
        {

            VersionNumber = "V1.0";
            FunList = new List<FunMenuItem>(){
                new FunMenuItem() {   IconName="Setting", Path="",FunName="Setting", TextName="基础设置" },
                new FunMenuItem() {   IconName= "Help", Path="",FunName="Setting", TextName= "帮助说明" },
                new FunMenuItem() {   IconName= "Network", Path="",FunName="Setting", TextName= "网络检查" }
            };
        }
    }
}
