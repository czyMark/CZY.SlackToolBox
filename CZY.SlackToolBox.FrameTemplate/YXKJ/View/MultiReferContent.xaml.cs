using CZY.SlackToolBox.FastExtend;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.View
{
    /// <summary>
    /// MultiReferContent.xaml 的交互逻辑
    /// </summary>
    public partial class MultiReferContent : UserControl
    {
        public MultiReferContent()
        {
            InitializeComponent();

        }
        private void CellLoaded(object sender, EventArgs e)
        {
            var t = (LuckyControl.MultiData.GridPagingModel)ReferDataGrid.DataContext;
            if (t == null)
                return;
            List<object>  obj=t.SelectedItem as List<object>;
            FrameworkElement element = (System.Windows.FrameworkElement)sender;
            if (obj.Contains(element.DataContext))
            {
                ViewTreeTool.SetVisualChildCheckBox((UIElement)sender, true);
            }
        }

        public object GetSelected()
        {
            var t = (LuckyControl.MultiData.GridPagingModel)ReferDataGrid.DataContext;
            return t.SelectedItem;

        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            if (ck == null)
            {
                return;
            }
            ReferDataGrid.UpdateLayout();

            List<object> obj = new List<object>();
            for (int i = 0; i < this.ReferDataGrid.Items.Count; i++)
            {
                DataGridRow dr = (DataGridRow)ReferDataGrid.ItemContainerGenerator.ContainerFromItem(ReferDataGrid.Items[i]);
                if (dr == null)
                {
                    ReferDataGrid.UpdateLayout();
                    ReferDataGrid.ScrollIntoView(ReferDataGrid.Items[i]);
                    dr = (DataGridRow)ReferDataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                }
                ViewTreeTool.SetVisualChildCheckBox(dr, (bool)ck.IsChecked);
                obj.Add(this.ReferDataGrid.Items[i]);
            }
            var t = (LuckyControl.MultiData.GridPagingModel)ReferDataGrid.DataContext;
            if (t == null)
                return;
            if ((bool)ck.IsChecked)
            {
                t.SelectedItem = obj;
            }
            else
            {
                t.SelectedItem = null;
            }
        }
         
    }
}
