﻿using System.Windows.Controls;

namespace CZY.SlackToolBox.FrameTemplate.AirportCS.View
{
    /// <summary>
    /// PersonalCenter.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalCenter : UserControl
    {
        public enum PersonalFunction { PersonalCenter, LockScreen,  EditPwd,ExitLogin, ExitSys }
        public delegate void SelectedFuntion(PersonalFunction personalFunction);
        public event SelectedFuntion selectedFuntion;
        public PersonalCenter()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (selectedFuntion != null)
            {
                Button button = sender as Button;
                switch (button.Content.ToString())
                {
                    case "个人中心": selectedFuntion(PersonalFunction.PersonalCenter); break;
                    case "挂机锁屏": selectedFuntion(PersonalFunction.LockScreen); break;
                    case "修改密码": selectedFuntion(PersonalFunction.EditPwd); break;
                    case "退出登录": selectedFuntion(PersonalFunction.ExitLogin); break;
                    case "关闭系统": selectedFuntion(PersonalFunction.ExitSys); break;
                }
            }
        }
    }
}
