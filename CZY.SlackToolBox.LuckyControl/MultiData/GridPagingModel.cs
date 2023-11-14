using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CZY.SlackToolBox.LuckyControl.MultiData
{
    /// <summary>
    /// 分页控件
    /// </summary>
    public class GridPagingModel : INotifyPropertyChanged
    {
        private int _SelectedIndex;

        public GridPagingModel()
        {
            FirstPage = new RelayCommand(CMD_FirstPage);
            PrevPage = new RelayCommand(CMD_PrevPage);
            NextPage = new RelayCommand(CMD_NextPage);
            LastPage = new RelayCommand(CMD_LastPage);
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
                RefreashPage(Current);
            }
        }

        public List<string> SelectList { get; set; }

        public int Total { get; set; }

        public void FreashData(List<object> list)
        {
            dataList = list;
            pagedList = new SortedDictionary<int, List<object>>();
            for (int i = 0; i < list.Count; i += CurrentSplit)
            {
                pagedList[i / CurrentSplit] = list.Skip(i).Take(CurrentSplit).ToList();
            }
            Current = 1;
            Total = pagedList.Count;
            RefreashPage(Current);
            SelectList = pagedList.ToList().ConvertAll(o => $"第{o.Key + 1}页");
            RaisProperyChanged("SelectList");
        }

        private void CMD_FirstPage(object obj)
        {
            if (pagedList == null || !pagedList.Any())
                return;
            if (Current <= 1)
                return;
            Current = 1;
            RefreashPage(Current);
        }

        private void CMD_LastPage(object obj)
        {
            if (pagedList == null || !pagedList.Any())
                return;
            if (Current >= Total)
                return;
            Current = Total;
            RefreashPage(Current);
        }

        private void CMD_NextPage(object obj)
        {
            if (pagedList == null || !pagedList.Any())
                return;
            if (Current >= Total)
                return;
            Current++;
            RefreashPage(Current);
        }

        private void CMD_PrevPage(object obj)
        {
            if (pagedList == null || !pagedList.Any())
                return;
            if (Current <= 1)
                return;
            Current--;
            RefreashPage(Current);
        }

        void RaisProperyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        void RefreashPage(int index)
        {
            Items = pagedList[index - 1];
            RaisProperyChanged("Items");
            RaisProperyChanged("Current");
            RaisProperyChanged("Total");
        }
        public List<int> SplitCount { get; set; } = new List<int>() { 5, 10, 20, 50, 100, 200, 500, 1000 };
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
                    FreashData(dataList);
            }
        }

    }
     
}
