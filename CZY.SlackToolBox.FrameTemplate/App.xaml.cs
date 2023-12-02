using CZY.SlackToolBox.FastExtend;
using CZY.SlackToolBox.FrameTemplate.YXKJ;
using CZY.SlackToolBox.FrameTemplate.YXKJ.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
                Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException; ;
                

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
            //启动缓存窗体。完成系统配置或外联设备参数初始化
            UserCache.Init();

            //在缓存窗体中检查是否要更新程序


            //登录窗体
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

        }
    }
}
