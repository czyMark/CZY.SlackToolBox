using CZY.SlackToolBox.FastExtend.StringFile;
using Expression.Blend.SampleData.SampleDataSource;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CZY.SlackToolBox.FastApply.View.FastDevelop
{
    /// <summary>
    /// FunTool.xaml 的交互逻辑
    /// </summary>
    public partial class FunTool : UserControl
    {
        public static FunTool Instance = new FunTool();
        public FunTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 文件选择框
        /// </summary>
        /// <returns></returns>
        public string SelecteFile(string filter = null, string defaultDir = null)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (!string.IsNullOrEmpty(defaultDir))
                dialog.InitialDirectory = defaultDir;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                dialog.Filter = filter;
            }
            dialog.RestoreDirectory = true;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                return dialog.FileName;
            }
            return string.Empty;
        }
        public string SaveFileSet(string defaultFileName = null, string filter = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                dialog.Filter = filter;
            }
            if (!string.IsNullOrWhiteSpace(defaultFileName))
            {
                dialog.FileName = defaultFileName;
            }
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return string.Empty;
        }
        private void Button_Click_Export(object sender, RoutedEventArgs e)
        {
            #region 初始化表头
            DataTable dt = new DataTable();
            dt.Columns.Add("名称", typeof(string));
            dt.Columns.Add("IP地址", typeof(string));
            dt.Columns.Add("端口", typeof(string));
            dt.Columns.Add("描述", typeof(string));
            #endregion

            #region 组织数据
            for (int i = 0; i < 9; i++)
            {
                dt.Rows.Add();
                //根据panelID查询报警板信息
                dt.Rows[i]["名称"] = $"网卡{i}";
                dt.Rows[i]["IP地址"] = $"192.168.5.0{i}";
                dt.Rows[i]["端口"] = $"50{i}";
                dt.Rows[i]["描述"] = $"测试网卡{i}";
            }
            #endregion

            #region 新建页
            List<DataTable> datas = new List<DataTable>();
            datas.Add(dt);
            #endregion

            string savePath = SaveFileSet($"ExportTest.xlsx", "Execl files (*.xlsx)|*.xlsx");
            if (!string.IsNullOrEmpty(savePath))
                savePath.ExportExcel(datas);
        }
        private void Button_Click_Import(object sender, RoutedEventArgs e)
        {
            string savePath = SelecteFile("Execl files (*.xlsx)|*.xlsx");
            if (!string.IsNullOrEmpty(savePath))
            {
                List<DataTable> dataTables = savePath.ImportExcel();
                string mesg = "";
                foreach (DataTable dt in dataTables)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        mesg+= dt.Rows[i][0].ToString();
                    }
                }
                MessageBox.Show(mesg);
            }
        }
    }
}
