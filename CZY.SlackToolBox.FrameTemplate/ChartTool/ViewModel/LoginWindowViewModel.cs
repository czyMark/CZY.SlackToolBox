using CZY.SlackToolBox.FrameTemplate.ChartTool.Core;
using CZY.SlackToolBox.FrameTemplate.ChartTool.View;
using CZY.SlackToolBox.LuckyControl;
using System.ComponentModel;
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate.ChartTool.ViewModel
{
    public class LoginWindowViewModel : INotifyPropertyChanged
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
        /// 用户名
        /// </summary>
        private string account;
        public string Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LoadingText"));
                }
            }
        }



        /// <summary>
        /// 密码
        /// </summary>

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }


        #endregion


        #region 事件

        /// <summary>
        /// 登录命令
        /// </summary>
        public RelayCommand LoginCommand { get; private set; }

        //登录
        void ExecuteLoginCommand(object obj)
        {
            LoadingVisibility=Visibility.Visible;

            //程序登录成功后关闭当前程序
            var win = obj as Window;
            if (win != null)
            { 
                win.Close();

                UserCache.AccountName = "超级管理员";
                UserCache.AccountID = "1";

                MainWindow main = new MainWindow();
                main.Show();
            }
        }
         

        #endregion

        public LoginWindowViewModel()
        {
            loadingVisibility = Visibility.Collapsed;
            LoginCommand = new RelayCommand(ExecuteLoginCommand); 
        }
    }
}
