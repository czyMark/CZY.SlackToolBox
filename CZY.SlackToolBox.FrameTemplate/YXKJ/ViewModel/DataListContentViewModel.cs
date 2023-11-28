using CZY.SlackToolBox.FastExtend;
using CZY.SlackToolBox.FrameTemplate.YXKJ.Core;
using CZY.SlackToolBox.FrameTemplate.YXKJ.View;
using CZY.SlackToolBox.LuckyControl;
using CZY.SlackToolBox.LuckyControl.MessageNotify;
using CZY.SlackToolBox.LuckyControl.MultiData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel
{
    public class DataListContentViewModel : INotifyPropertyChanged
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
            DataEditContent dx = new DataEditContent();
            //要新建的实体
            DataEditContentViewModel dataEdit = new DataEditContentViewModel(obj);
        
            dx.DataContext = dataEdit;

            DataEditContent dataEditContent = dx;

            //弹窗修改
            ContentBoxWin win = ContentBoxWin.GetContentBoxWin(dataEditContent, "添加信息", ContentBoxWin.ContentBoxWinState.YesNo);
            bool? state = win.ShowDialog();
            if (state == true)
            {
                //dataEdit.DataV 为编辑后的信息

                //调用修改数据的请求  

                MainWindowManager.SetMessageTip("信息添加完成");
                RefreshData();
            }
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
            //    MainWindowManager.SetMessageTip(  "无法找到要修改的数据，请刷新数据...", LuckyControl.ElementPanel.TipPanel.TipPanelState.Warn);
            //    return;
            //}  
            DataEditContent dx=new DataEditContent();
            DataEditContentViewModel dataEdit=new DataEditContentViewModel(obj); 
            dataEdit.BaseData = new
            {
                Sex = new List<Sex>() {
                new Sex() { Id=0, Text="女" },
                new Sex() { Id=1, Text="男" },
                new Sex() { Id=2, Text="未知" },
            }
            };
            dx.DataContext = dataEdit;
            DataEditContent dataEditContent = dx;

            //
            //滑动修改框修改

            //设置弹窗宽高
            ContentBoxWin.WinWidth = 720;
            ContentBoxWin.WinHeight = 600;
            //弹窗修改
            ContentBoxWin win = ContentBoxWin.GetContentBoxWin(dataEditContent, "修改信息", ContentBoxWin.ContentBoxWinState.YesNo);
            bool? state = win.ShowDialog();
            if (state == true)
            {
                //dataEdit.DataV 为编辑后的信息

                //调用修改数据的请求  

                MainWindowManager.SetMessageTip("信息修改成功");
                RefreshData();
            }
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

        public DataListContentViewModel()
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
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new TempData
                    {
                        Id=i,
                        Question=i.ToString(),
                        Answer=i.ToString(),
                        Remark = "测试数据",
                    });
                }
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
                //}
            });
            if (list.Count > 0)
                GridPagingService.FreashData(list);
            else { 
            
                //SysTipWindow.Show("系统提示", "没有该数据");
            }
            LoadingVisibility = Visibility.Collapsed;
        }
        #endregion
    }
    [Serializable]
    public class Sex
    {
        public int Id { get; set; }
        public string Text { get; set; } 
    }
    [Serializable]
    public class TempData
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public string Remark { get; set; } 
    }
}
