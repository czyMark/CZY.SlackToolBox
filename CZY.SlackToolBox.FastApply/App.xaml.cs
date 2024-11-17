using CZY.SlackToolBox.FastExtend;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CZY.SlackToolBox.FastApply
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
                //此代码解决锁屏后，程序假死的情况（尤其趋势图不出现新的点，感觉控件假死）
                RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;

                System.Windows.FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;


                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;



                CZY.SlackToolBox.FastExtend.LanguageTool.Instance.ChangeLanguageResourceHandle += ChangeLanguageResource;
                CZY.SlackToolBox.FastExtend.LanguageTool.Instance.FindResourceHandle += SICPFindResource;
                CZY.SlackToolBox.FastExtend.LanguageTool.Instance.InitLanguageResourceCache(GetLanguageResourceCache());

            }
            catch (Exception ex)
            {

                Application.Current.Shutdown();
            }
        }

        private object SICPFindResource(object key)
        {
            if (key == null) return null;
            return Current.TryFindResource(key);
        }
        /// <summary>
        /// 程序集解析失败后调用   可直接从本地查找 补充
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = System.IO.Path.Combine(path, "Framework", assemblyName.Name + ".dll");
            if (System.IO.File.Exists(filePath))
                return Assembly.LoadFrom(filePath);
            else
                return null;
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
            Thread thread = new Thread(() =>
            {
                 
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            this.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            new MainWindow().Show();
        }


        #region 语言服务



        private void ChangeLanguageResource(string languageDictionaryUriPath)
        {
            ResourceDictionary langRd = Application.LoadComponent(new Uri(languageDictionaryUriPath, UriKind.Relative)) as ResourceDictionary;
            if (base.Resources.MergedDictionaries.Count > 0)
            {
                var old = base.Resources.MergedDictionaries.FirstOrDefault(rItem => rItem.Contains("LanuageKey_LanguageResourceKey"));
                if (old != null)
                    base.Resources.MergedDictionaries.Remove(old);
            }
            base.Resources.MergedDictionaries.Add(langRd);
             
        }

        private IDictionary<string, string> GetLanguageResourceCache()
        {
            IDictionary<string, string> languageResourceCache = new Dictionary<string, string>();
            var languageResource = base.Resources.MergedDictionaries.FirstOrDefault(rItem => rItem.Contains("LanuageKey_LanguageResourceKey"));
            if (languageResource == null) return languageResourceCache;
            foreach (var key in languageResource.Keys)
            {
                var languageKey = key.ToString();
                var languageValue = languageResource[key].ToString();
                if (!languageResourceCache.ContainsKey(languageValue)
                    && languageKey.Contains("LanuageKey_Code_"))
                    languageResourceCache.Add(languageValue, languageKey);
            }
            return languageResourceCache;
        }

        #endregion
    }
}
