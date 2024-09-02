using CZY.SlackToolBox.LuckyControl.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CZY.SlackToolBox.LuckyControl.MultiData
{
    /// <summary>
    /// MultiComboBox.xaml 的交互逻辑
    /// </summary>
    public partial class MultiComboBox : UserControl
    {

        /// <summary>
        /// 选中项列表
        /// </summary>
        public ObservableCollection<MultiCbxBaseData> ChekedItems = new ObservableCollection<MultiCbxBaseData>();

        /// <summary>
        /// ListBox竖向列表
        /// </summary>
        private ListBox _ListBoxV;

        /// <summary>
        /// ListBox横向列表
        /// </summary>
        private ListBox _ListBoxH;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
         "ItemsSource",
         typeof(IEnumerable),
         typeof(MultiComboBox), new PropertyMetadata(ItemsSourcePropertyChanged));
        private static void ItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                MultiComboBox up = d as MultiComboBox;
                IEnumerable datav = e.NewValue as IEnumerable;
                foreach (var item in datav)
                {
                    MultiCbxBaseData bdc = item as MultiCbxBaseData;
                    if (bdc.IsCheck)
                    {
                        up._ListBoxV.SelectedItems.Add(bdc);
                    }
                }
            }
        }


        public MultiComboBox()
        {
            InitializeComponent();
            _ListBoxV = PART_ListBox;
            _ListBoxH = PART_ListBoxChk;
            _ListBoxH.ItemsSource = ChekedItems;
            _ListBoxV.SelectionChanged += _ListBoxV_SelectionChanged;
            _ListBoxH.SelectionChanged += _ListBoxH_SelectionChanged;

        }

        private void _ListBoxH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                MultiCbxBaseData datachk = item as MultiCbxBaseData;

                for (int i = 0; i < _ListBoxV.SelectedItems.Count; i++)
                {
                    MultiCbxBaseData datachklist = _ListBoxV.SelectedItems[i] as MultiCbxBaseData;
                    if (datachklist.ID == datachk.ID)
                    {
                        _ListBoxV.SelectedItems.Remove(_ListBoxV.SelectedItems[i]);
                    }
                }
            }
        }

        void _ListBoxV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                MultiCbxBaseData datachk = item as MultiCbxBaseData;
                datachk.IsCheck = true;
                if (ChekedItems.IndexOf(datachk) < 0)
                {
                    ChekedItems.Add(datachk);
                }
            }

            foreach (var item in e.RemovedItems)
            {
                MultiCbxBaseData datachk = item as MultiCbxBaseData;
                datachk.IsCheck = false;
                ChekedItems.Remove(datachk);
            }
        }

        public class MultiCbxBaseData
        {
            private int _id;
            /// <summary>
            /// 关联主键
            /// </summary>
            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            private string _viewName;
            /// <summary>
            /// 显示名称
            /// </summary>
            public string ViewName
            {
                get { return _viewName; }
                set
                {
                    _viewName = value;
                }
            }

            private bool _isCheck;
            /// <summary>
            /// 是否选中
            /// </summary>
            public bool IsCheck
            {
                get { return _isCheck; }
                set { _isCheck = value; }
            }
        }
    }
}
