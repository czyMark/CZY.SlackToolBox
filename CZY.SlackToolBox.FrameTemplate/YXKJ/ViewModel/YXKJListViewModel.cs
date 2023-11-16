using CZY.SlackToolBox.LuckyControl;
using CZY.SlackToolBox.LuckyControl.MultiData;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel
{
    public class YXKJListViewModel : INotifyPropertyChanged
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

        private string questionText;
        public string QuestionText
        {
            get
            {
                return questionText;
            }
            set
            {
                questionText = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("QuestionText"));
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
        /// <summary>
        /// 添加数据
        /// </summary>
        public RelayCommand AddDataCommand { get; private set; }

        //添加数据
        void ExecuteAddDataCommand(object obj)
        {
            //EditWindow window = new EditWindow(new BaseTopics());
            //bool? state = window.ShowDialog();
            //if (state == true)
            //{
            //    SysTipWindow.Show("系统提示", "数据保存成功");
            //    RefreshData();
            //}
        }


        /// <summary>
        /// 修改数据
        /// </summary>
        public RelayCommand DataEditCommand { get; private set; }

        //修改数据
        void ExecuteDataEditCommand(object obj)
        {
            //BaseTopics loadData = (BaseTopics)obj;
            //if (loadData == null)
            //{
            //    SysTipWindow.Show("系统提示", "无法找到要修改的数据，请刷新数据...");
            //    return;
            //}
            //EditWindow window = new EditWindow(loadData);
            //bool? state = window.ShowDialog();
            //if (state == true)
            //{
            //    SysTipWindow.Show("系统提示", "数据保存成功");
            //    RefreshData();
            //}
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        public RelayCommand DataDelCommand { get; private set; }

        //删除数据
        void ExecuteDataDelCommand(object obj)
        {
            //BaseTopics loadData = (BaseTopics)obj;
            //if (loadData == null)
            //{
            //    SysTipWindow.Show("系统提示", "无法找到要删除的数据，请刷新数据...");
            //    return;
            //}
            //MySQLDatabase mySQL = MySQLDatabase.GetInstance();

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete from  base_Topics ");
            //strSql.Append(" where id=@dataid");
            //MySqlParameter[] parameters = {
            //        new MySqlParameter("@dataid", loadData.Id)
            //};
            //int i = mySQL.ExecuteNonQuery(strSql.ToString(), parameters);

            //if (i > 0)
            //{
            //    SysTipWindow.Show("系统提示", "删除成功");
            //    RefreshData();
            //}
            //else
            //{

            //    SysTipWindow.Show("系统提示", "无法找到要删除的数据，请刷新数据...");
            //    return;
            //}
        }




        #endregion

        public YXKJListViewModel()
        {
            RefreshDataCommand = new RelayCommand(ExecuteRefreshDataCommand);
            AddDataCommand = new RelayCommand(ExecuteAddDataCommand);
            DataEditCommand = new RelayCommand(ExecuteDataEditCommand);
            DataDelCommand = new RelayCommand(ExecuteDataDelCommand); 
            RefreshData();
        }

        #region 表格操作

        public GridPagingModel GridPagingService { get; set; } = new GridPagingModel();

        public async void RefreshData()
        {
            LoadingText = "数据加载中....";
            LoadingVisibility = Visibility.Visible;
            var list = new List<object>();
            //await Task.Run(() =>
            //{
            //    //从数据库中获取所有用户数据

            //    MySQLDatabase mySQL = MySQLDatabase.GetInstance();
            //    StringBuilder strSql = new StringBuilder();
            //    strSql.Append("select id,question,answer,remark from base_Topics");
            //    strSql.Append(" where question like @question  ");
            //    MySqlParameter[] parameters = {
            //        new MySqlParameter("@question", "%"+QuestionText+"%")
            //};
            //    DataSet data = mySQL.ExecuteDataSet(strSql.ToString(), parameters);

            //    if (data.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow item in data.Tables[0].Rows)
            //        {
            //            list.Add(new BaseTopics(
            //                item.ItemArray[0].ToString(),
            //                item.ItemArray[1].ToString(),
            //                item.ItemArray[2].ToString(),
            //                item.ItemArray[3].ToString()
            //                ));
            //        }
            //    }
            //});
            //if (list.Count > 0)
            //    GridPagingService.FreashData(list);
            //else { SysTipWindow.Show("系统提示", "没有该数据"); }
            LoadingVisibility = Visibility.Collapsed;
        }
        #endregion
    }
}
