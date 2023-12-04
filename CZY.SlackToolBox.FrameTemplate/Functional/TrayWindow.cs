using System;
using System.Windows.Forms;
using ContextMenu = System.Windows.Forms.ContextMenu;
using MenuItem = System.Windows.Forms.MenuItem;

namespace CZY.SlackToolBox.FrameTemplate.Functional
{
    public partial class TrayWindow 
    {
        private NotifyIcon notifyIcon = null;
  

        public TrayWindow()
        { 

            //设置托盘的各个属性
            //当缺少系统图标的时候无法显示
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "XX系统后台运行";
            notifyIcon.Text = "XX系统在后台运行，如需要唤醒请从系统托盘处操作";
            notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + @"\product.ico");
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(5000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            //设置菜单项
            MenuItem setting1 = new MenuItem("setting1");
            MenuItem setting2 = new MenuItem("setting2");
            MenuItem setting = new MenuItem("setting", new MenuItem[] { setting1, setting2 });

            //帮助选项
            MenuItem help = new MenuItem("help");

            //关于选项
            MenuItem about = new MenuItem("about");


            //退出菜单项
            MenuItem exit = new MenuItem("exit");
            exit.Click += new EventHandler(exit_Click);

            //关联托盘控件
            MenuItem[] childen = new MenuItem[] { setting, help, about, exit };
            notifyIcon.ContextMenu = new ContextMenu(childen);
             

             
        }

        public void ShowNotofy(string balloonTipText,string text,int timeout=1000)
        {
            notifyIcon.BalloonTipText = balloonTipText;
            notifyIcon.Text = text;
            notifyIcon.ShowBalloonTip(timeout);
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //如果鼠标左键单击
            if (e.Button == MouseButtons.Left)
            {
               //进入对应的窗口
            }
        }

        /// <summary>
        /// 退出选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
