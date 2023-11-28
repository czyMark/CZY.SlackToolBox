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
        /// 要编辑的数据
        /// </summary>
        private object dataV;
        public object DataV
        {
            get
            {
                return dataV;
            }
            set
            {
                dataV = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DataV"));
                }
            }
        }
        /// <summary>
         /// 用来渲染界面的基础数据 例如：性别 xx类型
         /// </summary>
        private object baseData;
        public object BaseData
        {
            get
            {
                return baseData;
            }
            set
            {
                baseData = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("BaseData"));
                }
            }
        }
        #endregion

        #region 事件

        /// <summary>
        /// 打开XX引用窗口
        /// </summary>
        public RelayCommand XXReferCommand { get; private set; }

        //打开XX引用窗口
        void XXReferCommandCommand(object obj)
        {
            //打开引用窗口
            ReferContent dx = new ReferContent();

            //设置弹窗宽高
            ContentBoxWin.WinWidth = 600;
            ContentBoxWin.WinHeight = 500;
            //弹窗修改
            ContentBoxWin win = ContentBoxWin.GetContentBoxWin(dx, "引用", ContentBoxWin.ContentBoxWinState.Yes);
            bool? state = win.ShowDialog();
            if (state == true)
            {
                //依据选中项修改DataV中的xx属性
                //dx.SelectItem
            }



        } 
        #endregion
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="editData">要编辑的数据</param>
        public DataEditContentViewModel(object editData)
        {
            ////避免修改时直接修改界面导致操作上的bug，使用深拷贝复制对象。在操作
            DataV = editData.DeepCopyByBinary();

            XXReferCommand = new RelayCommand(XXReferCommandCommand);

        }
    }
}
