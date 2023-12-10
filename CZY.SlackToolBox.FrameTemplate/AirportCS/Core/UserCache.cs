using CZY.SlackToolBox.FrameTemplate.AirportCS.View;
using CZY.SlackToolBox.LuckyControl; 
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate.AirportCS.Core
{
    public static class UserCache
    {
        public static string AccountID { get; set; }
        public static string AccountName { get; set; }
        public static string LockScreenPwd { get; set; }

        #region 通用命令
        public static RelayCommand Shutdown { get; private set; }

        //退出系统
        private static void ExecuteShutdownCommand(object obj)
        {
            Application.Current.Shutdown();
        }




        public static RelayCommand ExitLogin { get; private set; }

        //退出登录
        private static void ExecuteExitLogin(object obj)
        {
            Application.Current.MainWindow.Close();
            LoginWindow login = new LoginWindow();
            login.Show();
        }
        #endregion

        #region 页面传递信息
        /// <summary>
        /// 用于页面传递url
        /// </summary>
        /// <param name="URL"></param>
        public delegate void ContentChangeDelegate(string nameSpaceName, string contentName);

        public static event ContentChangeDelegate ContentChangeEvent;
        /// <summary>
        /// 内容页所在的命名空间
        /// </summary>
        public static string NameSpaceName { get; set; }
        /// <summary>
        /// 界面传递的参数
        /// </summary>
        public static object ContentArgument { get; set; }
        /// <summary>
        /// 内容页名称，在修改后触发 ContentChange
        /// </summary>
        private static string contentName;
        public static string ContentName
        {
            get
            {
                return contentName;
            }
            set
            {
                contentName = value;
                if (ContentChangeEvent != null)
                {
                    ContentChangeEvent(NameSpaceName, value);
                }
            }
        }
        #endregion

         
        public static void Init()
        { 
            if (Shutdown == null)
                Shutdown = new RelayCommand(ExecuteShutdownCommand);
            if (ExitLogin == null)
                ExitLogin = new RelayCommand(ExecuteExitLogin);
        }
    }
}
