﻿using CZY.SlackToolBox.FrameTemplate.YXKJ.Core;
using CZY.SlackToolBox.LuckyControl;
using System.ComponentModel;
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.ViewModel
{
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// wpf 接口继承
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        #region 属性

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
            LoginCommand = new RelayCommand(ExecuteLoginCommand); 
        }
    }
}
