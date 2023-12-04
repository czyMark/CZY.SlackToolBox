using CZY.SlackToolBox.FrameTemplate.YXKJ.Core;
using System.Threading;
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate.Functional
{
    /// <summary>
    /// InitSysWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InitSysWindow : Window
    {
        public InitSysWindow()
        {
            InitializeComponent();
            this.Loaded += InitSysWindow_Loaded; 
        }

        private void StartDataDecrypt()
        {

            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("检查系统版本...")));
            ////在缓存窗体中检查是否要更新程序



            //检测运行环境是否完整
            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("检测系统运行环境...")));

            //检查基础目录是否完善
            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("目录检查完成")));

            //检查是否安装驱动

            //检查依赖程序运行环境

            //检查依赖程序是否已达到运行的准备

            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("检测系统运行环境完成")));




            #region 初始化配置
            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("初始化配置...")));

            Thread.Sleep(1000);
            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("核心资源初始化...")));

            Thread.Sleep(1000);
            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("初始化本地缓存...")));
            UserCache.Init();

            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("初始化配置完成")));
            #endregion

            Thread.Sleep(1000);

            #region 检测网络状态
            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("检测网络状态...")));

            Thread.Sleep(1000);

            this.Dispatcher.Invoke(new System.Action(() => InitTipMssage("检测网络状态完成")));
            #endregion
            Thread.Sleep(3000);
            this.Dispatcher.Invoke(new System.Action(() => this.Close()));
            

        }

        public bool Rolling = true;

        private string[] tipmsg = new string[3];
        int index = 0;
        public void InitTipMssage(string tipStr)
        {
            if (!Rolling)
            {
                this.InitTip.Text = tipStr;
                return;
            }

            if (index >= tipmsg.Length)
            {
                //排序
                for (int i = 0; i < tipmsg.Length - 1; i++)
                {
                    tipmsg[i] = tipmsg[i + 1];
                }
            }
            index = tipmsg.Length - 1;
            tipmsg[index] = tipStr;
            string temp = string.Empty;
            for (int i = 0; i < tipmsg.Length; i++)
            {
                temp = temp + tipmsg[i] + "\r\n";
            }
            this.InitTip.Text = temp;
            index++;
        }
        private void InitSysWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Thread th1 = new Thread(StartDataDecrypt);
            th1.Start();
        }
    }
}
