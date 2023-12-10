using CZY.SlackToolBox.LuckyControl;
using CZY.SlackToolBox.LuckyControl.MultiData;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel
{
    public class ReferContentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// wpf 接口继承
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        #region 属性

        /// <summary>
        /// 加载等待条是否显示
        /// </summary>
        private Visibility loadingVisibility;
        public Visibility LoadingVisibility
        {
            get
            {
                return loadingVisibility;
            }
            set
            {
                loadingVisibility = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LoadingVisibility"));
                }
            }
        }

        /// <summary>
        /// 加载等待条显示文字
        /// </summary>
        private string loadingText;
        public string LoadingText
        {
            get
            {
                return loadingText;
            }
            set
            {
                loadingText = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LoadingText"));
                }
            }
        }



        /// <summary>
        /// 搜索输入的名称
        /// </summary>

        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SearchText"));
                }
            }
        }
         

        /// <summary>
        /// 选中的数据
        /// </summary>
         
        public object SelectedItem
        {
            get
            {
                return GridPagingService.SelectedItem;
            }
            set
            {
                GridPagingService.SelectedItem = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("GridPagingService.SelectedItem"));
                }
            }
        }
        #endregion


        #region 事件

        /// <summary>
        /// 刷新数据
        /// </summary>
        public RelayCommand RefreshDataCommand { get; private set; }

        //刷新用户数据
        void ExecuteRefreshDataCommand(object obj)
        {
            RefreshData();
        }




        #endregion

        private List<object> ReferData;
        public ReferContentViewModel(List<object> referData)
        {
            RefreshDataCommand = new RelayCommand(ExecuteRefreshDataCommand);
            ReferData = referData;
            RefreshData();
        }

        #region 表格操作

        public PagingModel GridPagingService { get; set; } = new PagingModel();
        public async void RefreshData()
        {
            LoadingText = "数据加载中....";
            LoadingVisibility = Visibility.Visible;

            await Task.Run(() =>
            {
                //数据的特殊处理
            });
            if (ReferData.Count > 0)
                GridPagingService.FreshData(ReferData);

            LoadingVisibility = Visibility.Collapsed;
        }
        #endregion
    } 
}
