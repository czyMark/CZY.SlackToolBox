using CZY.SlackToolBox.FrameTemplate.YXKJ.View;
using CZY.SlackToolBox.LuckyControl;
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.Core
{
    public class UserCache
    {
        public static string AccountID { get; set; }
        public static string AccountName { get; set; }




        public static RelayCommand Shutdown { get; private set; }

        //退出系统
        private static void ExecuteShutdownCommand(object obj)
        {
            Application.Current.Shutdown();
        }




        public static RelayCommand ExitLogin { get; private set; }

        //退出系统
        private static void ExecuteExitLogin(object obj)
        {
            Application.Current.MainWindow.Close();
            LoginWindow login = new LoginWindow();
            login.Show();
        }


        public static void Init()
        {
            Shutdown = new RelayCommand(ExecuteShutdownCommand);
            ExitLogin = new RelayCommand(ExecuteExitLogin);
        }
    }
}
