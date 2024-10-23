using IWshRuntimeLibrary;
using LaunchMoreApp.Helpers;
using LaunchMoreApp.Models;
using LaunchMoreApp.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace LaunchMoreApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //编辑右键菜单  快捷操作
            ContextMenu contextMenu = new ContextMenu();
            #region 初始化右键菜单数据
            {

                MenuItem showValue = new MenuItem() { Header = "打开软件", Foreground = new SolidColorBrush(Colors.Black) };
                showValue.Name = "AddSoft";
                showValue.Click += MenuItem_Click;
                contextMenu.Items.Add(showValue);
            }
            {

                MenuItem showValue = new MenuItem() { Header = "支持列表", Foreground = new SolidColorBrush(Colors.Black) };
                showValue.Name = "AllSoft";
                showValue.Click += MenuItem_Click;
                contextMenu.Items.Add(showValue);
            } 
            #endregion
            this.ContextMenu = contextMenu;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item != null)
            {
                switch (item.Name)
                {
                    case "AddSoft":
                        AddSoft();
                        break;
                    case "AllSoft":
                        {
                            AllSoftWin win= new AllSoftWin();
                            win.ShowDialog();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public void AddSoft()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;//返回文件的完整路径   
                string[] img = { ".lnk", ".exe", ".doc", ".docx", ".rar" };
                if (System.IO.File.Exists(path))
                {
                    //string filename = System.IO.Path.GetFileName(path);
                    string ext = System.IO.Path.GetExtension(path);
                    if (Array.IndexOf(img, ext) != -1)
                    {
                        if (ext.Equals(".lnk"))
                        {
                            string initialSource = @"C:\Users\AY_Format\Desktop\QuickHider快捷方式.lnk"; //需要读取的快捷方式路径
                            WshShell shell = new WshShell();
                            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(initialSource);    //获取快捷方式对象
                            path = shortcut.TargetPath;
                        }
                        openFile(path);
                    }
                }
            }
        }
        private void win_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))      //判断该文件是否可以转换到文件放置格式
            {
                e.Effects = DragDropEffects.Link;       //放置效果为链接放置
            }
            else
            {
                e.Effects = DragDropEffects.None;      //不接受该数据,无法放置，后续事件也无法触发
            }
        }

        private void win_Drop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            string[] img = { ".lnk", ".exe", ".doc", ".docx", ".rar" };
            if (System.IO.File.Exists(path))
            {
                //string filename = System.IO.Path.GetFileName(path);
                string ext = System.IO.Path.GetExtension(path);
                if (Array.IndexOf(img, ext) != -1)
                {
                    if (ext.Equals(".lnk"))
                    {
                        WshShell shell = new WshShell();
                        IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(path);    //获取快捷方式对象
                        path = shortcut.TargetPath;
                    }
                    openFile(path);
                }
            }
        }

        private void openFile(string path = "")
        {
            HandleModle.ClearMemory();
            string args = "";
            string dir = System.IO.Path.GetDirectoryName(path);//路径
            string ext = System.IO.Path.GetExtension(path);//后缀
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            ResultInfo res = Data.getSoftInfo(name);
            if (res.code == 1)
            {
                foreach (MapInfo mi in res.data)
                {
                    //互斥体
                    if (mi.type == 1)
                    {
                        Process[] localByName = Process.GetProcessesByName(mi.process);
                        foreach (Process pro in localByName)
                        {
                            foreach (string value in mi.values)
                            {
                                checkProcessAndClose(pro, value);
                            }
                        }
                    }
                    //起始名称
                    if (mi.type == 2)
                    {

                        if (!System.IO.File.Exists(dir + "\\" + mi.paths + "-mut" + ext))
                        {
                            System.IO.File.Copy(dir + "\\" + mi.paths + ext, dir + "\\" + mi.paths + "-mut" + ext);
                        }
                        args = mi.paths_args;
                        Process[] localByName = Process.GetProcessesByName(mi.process + "-mut");
                        foreach (Process pro in localByName)
                        {
                            foreach (string value in mi.values)
                            {
                                checkProcessAndClose(pro, value);
                            }
                        }
                        path = dir + "\\" + mi.paths + "-mut" + ext;
                    }
                }
            }
            Process myprocess = new Process();
            myprocess.StartInfo = new ProcessStartInfo(path, args);
            myprocess.StartInfo.WorkingDirectory = dir;
            myprocess.Start();
            HandleModle.ClearMemory();
        }


        private void checkProcessAndClose(Process pro, string name)
        {
            ushort handle = RefreshHandles2(pro, name);
            if (handle != 0)
            {
                HandleModle.CloseProcessHandle(pro.Id, handle);
            }
        }

        private ushort RefreshHandles2(Process pro, string check = "")
        {
            List<Win32API.SYSTEM_HANDLE_INFORMATION> lws = HandleModle.GetHandles(pro);
            foreach (Win32API.SYSTEM_HANDLE_INFORMATION lw in lws)
            {
                string str_handle_name = HandleModle.GetFilePath(lw, pro);

                if ("" == str_handle_name)
                {
                    continue;
                }
                if (str_handle_name == null)
                {
                    continue;
                }
                if (str_handle_name.Contains(check))
                {
                    Console.WriteLine(lw.ProcessID);
                    Console.WriteLine(lw.ObjectTypeNumber);
                    Console.WriteLine(lw.Flags);
                    Console.WriteLine(lw.Handle);
                    Console.WriteLine(str_handle_name);
                    Console.WriteLine("===========================");
                    return lw.Handle;
                }
            }
            return 0;
        }
    }
}
