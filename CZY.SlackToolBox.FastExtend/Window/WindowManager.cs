using System;
using System.Collections.Generic;

namespace CZY.SlackToolBox.FastExtend.Window
{
    public static class WindowManager
    {
        class WindowInfo
        {
            public Type WindowType { get; set; }
            public System.Windows.Window Owner { get; set; }
        }
        static Dictionary<string, WindowInfo> _regWindowContainer = new Dictionary<string, WindowInfo>();

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        public static void Register<T>(string name, System.Windows.Window owner = null)
        {
            if (!_regWindowContainer.ContainsKey(name))
            {
                _regWindowContainer.Add(name, new WindowInfo { WindowType = typeof(T), Owner = owner });
            }
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="dataContext"></param>
        /// <returns></returns>
        public static bool ShowDialog<T>(string name, T dataContext)
        {
            if (_regWindowContainer.ContainsKey(name))
            {
                Type type = _regWindowContainer[name].WindowType;

                //反射创建窗体对象
                var window = (System.Windows.Window)Activator.CreateInstance(type);
                window.Owner = _regWindowContainer[name].Owner;
                window.DataContext = dataContext;
                return window.ShowDialog() == true;
            }

            return false;
        } 
    }
}
