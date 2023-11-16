using System.Windows.Controls;

namespace CZY.SlackToolBox.FrameTemplate.YXKJ.View
{
    /// <summary>
    /// PersonalCenter.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalCenter : UserControl
    {
        public enum PersonalFunction { PersonalCenter,EditPwd,ExitLogin }
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
                    case "修改密码": selectedFuntion(PersonalFunction.EditPwd); break;
                    case "退出登录": selectedFuntion(PersonalFunction.ExitLogin); break;
                }
            }
        }
    }
}
