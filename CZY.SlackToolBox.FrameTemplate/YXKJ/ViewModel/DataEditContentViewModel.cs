using CZY.SlackToolBox.FrameTemplate.YXKJ.Core;
using CZY.SlackToolBox.FrameTemplate.YXKJ.View;
using CZY.SlackToolBox.LuckyControl;
using CZY.SlackToolBox.LuckyControl.MessageNotify;
using CZY.SlackToolBox.LuckyControl.MultiData;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CZY.SlackToolBox.FastExtend;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel
{
    public class DataEditContentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// wpf 接口继承
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        #region 属性

        /// <summary>
        /// 测试属性
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



        public DataEditContentViewModel(object obj)
        {
            //避免修改时直接修改界面导致操作上的bug，使用深拷贝复制对象。在操作
            stu s =(stu) obj.DeepCopyByBinary();
            
           //将obj转换成model
           //将model值赋值给给属性
           VersionNumber = "V1.0";
        }
    }
    public class stu { 
    
    }
}
