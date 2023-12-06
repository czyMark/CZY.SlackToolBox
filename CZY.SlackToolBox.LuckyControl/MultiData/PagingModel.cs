using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CZY.SlackToolBox.LuckyControl.MultiData
{
    /// <summary>
    /// 分页控件模型
    /// </summary>
    public class PagingModel : INotifyPropertyChanged
    {
        private int _SelectedIndex;

        public PagingModel()
        {
            FirstPage = new RelayCommand(FirstPageFun);
            PrevPage = new RelayCommand(PrevPageFun);
            NextPage = new RelayCommand(NextPageFun);
            LastPage = new RelayCommand(LastPageFun);
            CheckCommand = new RelayCommand(CheckFun);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Current { get; set; }

        public RelayCommand FirstPage { get; }

        public List<object> Items { get; set; }

        public RelayCommand LastPage { get; }

        public RelayCommand NextPage { get; }

        public RelayCommand PrevPage { get; }

        private List<object> dataList;

        public SortedDictionary<int, List<object>> pagedList;

        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (_SelectedIndex == value)
                    return;
                _SelectedIndex = value;
                if (_SelectedIndex < 0)
                    return;
                Current = _SelectedIndex + 1;
                RefreshPage(Current);
            }
        }


        //选中的数据
        public object SelectedItem { get; set; }
        
        /// <summary>
        /// 多选复选框checked
        /// </summary>
        public RelayCommand CheckCommand { get; private set; }

        //多选复选框checked
        void CheckFun(object obj)
        { 
            //如果带有复选框 就是多选。 默认是null
            List<object> list = SelectedItem as List<object>;
            if (list == null)
            {
                list = new List<object>();
                list.Add(obj);
                SelectedItem = list;
                return;
            }
            //如果列表里面有就移除
            bool add = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(obj))
                {
                    add = false;
                    list.RemoveAt(i);
                    i--;
                }
            }
            //选中就添加
            if (add)
            {
                list.Add(obj);
            }
            SelectedItem = list;
        }


        /// <summary>
        /// 选中页选择
        /// </summary>
        public List<string> SelectList { get; set; }

        public int Total { get; set; }

        public void FreshData(List<object> list)
        {
            dataList = list;
            pagedList = new SortedDictionary<int, List<object>>();
            for (int i = 0; i < list.Count; i += CurrentSplit)
            {
                pagedList[i / CurrentSplit] = list.Skip(i).Take(CurrentSplit).ToList();
            }
            Current = 1;
            Total = pagedList.Count;
            RefreshPage(Current);
            SelectList = pagedList.ToList().ConvertAll(o => $"第{o.Key + 1}页");
            RaisProperyChanged("SelectList");
        }

        private void FirstPageFun(object obj)
        {
            if (pagedList == null || !pagedList.Any())
                return;
            if (Current <= 1)
                return;
            Current = 1;
            RefreshPage(Current);
        }

        private void LastPageFun(object obj)
        {
            if (pagedList == null || !pagedList.Any())
                return;
            if (Current >= Total)
                return;
            Current = Total;
            RefreshPage(Current);
        }

        private void NextPageFun(object obj)
        {
            if (pagedList == null || !pagedList.Any())
                return;
            if (Current >= Total)
                return;
            Current++;
            RefreshPage(Current);
        }

        private void PrevPageFun(object obj)
        {
            if (pagedList == null || !pagedList.Any())
                return;
            if (Current <= 1)
                return;
            Current--;
            RefreshPage(Current);
        }

        void RaisProperyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        void RefreshPage(int index)
        {
            Items = pagedList[index - 1];
            RaisProperyChanged("Items");
            RaisProperyChanged("Current");
            RaisProperyChanged("Total");
        }
        public List<int> AvailablePageSize { get; set; } = new List<int>() { 5, 10, 20, 50, 100, 200, 500, 1000 };

        private int _CurrentSplit = 100;

        public int CurrentSplit
        {
            get { return _CurrentSplit; }
            set
            {
                if (_CurrentSplit == value)
                    return;
                _CurrentSplit = value;
                if (dataList != null)
                    FreshData(dataList);
            }
        }

    }
     
}
