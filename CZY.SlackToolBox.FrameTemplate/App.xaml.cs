using CZY.SlackToolBox.FastExtend;
using CZY.SlackToolBox.FrameTemplate.Functional;
using CZY.SlackToolBox.FrameTemplate.YXKJ.View;
using System;
using System.Windows;

namespace CZY.SlackToolBox.FrameTemplate
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //防止二次启动程序
            if (ProcessTool.VerifyRunning())
            {
                Application.Current.Shutdown();
            }

            try
            {

                System.Windows.FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;


            }
            catch (Exception ex)
            {

                Application.Current.Shutdown();
            }
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //记录严重错误  并抛出

            e.Handled = true;//不在往下通知。     
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    //记录严重错误
                }
            }
            catch (Exception ex)
            {
                //记录严重错误
            }
            finally
            {
                //ignore
            }
            //记录严重错误  
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;

            //string config = "{ \"Url\": \"https://ark.cn-beijing.volces.com/api/v3/chat/completions\", \"ApiKey\": \"3ef4ce8d-96fa-4a49-b2dc-d78f7f186d0e\", \"EndpointId\": \"ep-20240823164642-5kqqv\" }";

            //AIClient aIClient = new AIClient(config);
            //string str = aIClient.ChatAI("你好");
            //while (true)
            //{
            //    string t = "";
            //}
            //CZY.SlackToolBox.FrameTemplate.ChartTool.View.LoginWindow win = new ChartTool.View.LoginWindow();
            //win.Show();


            //系统设置窗口模板
            CZY.SlackToolBox.FrameTemplate.SettingWindow.Core.UserCache.Init();
            CZY.SlackToolBox.FrameTemplate.SettingWindow.View.MainWindow win = new SettingWindow.View.MainWindow();
            win.Show();


            //机场管理系统模板
            //CZY.SlackToolBox.FrameTemplate.AirportCS.Core.UserCache.Init();
            //CZY.SlackToolBox.FrameTemplate.AirportCS.View.LoginWindow win = new AirportCS.View.LoginWindow();
            //win.Show();

            //TrayWindow Tray = new TrayWindow();


            //启动缓存窗体。完成系统配置或外联设备参数初始化
            //InitSysWindow win = new InitSysWindow();
            //win.ShowDialog();

            //登录窗体
            //LoginWindow loginWindow = new LoginWindow();
            //loginWindow.Show();


            //Tray.ShowNotofy("程序退出", "正常退出");



        }
    }
}
