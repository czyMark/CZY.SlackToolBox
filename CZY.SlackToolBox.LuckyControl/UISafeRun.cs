using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CZY.SlackToolBox.LuckyControl
{
    /// <summary>
    /// 作用于保障 任何时候修改界面都能安全运行 界面不阻塞
    /// </summary>
    public static class UISafe
    {
        /// <summary>
        /// 指定要更新的UI界面
        /// </summary>
        public static Dispatcher MainDispatcher { get; set; }
        public static void Run(this Action action)
        {
            if (MainDispatcher == null)
            {
                action();
                return;
            }

            if (!MainDispatcher.CheckAccess())
            {
                MainDispatcher.BeginInvoke(action);
                return;
            }
            action();
        }
    }
}
