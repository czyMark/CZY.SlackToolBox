using System;
using System.Collections.Generic;

namespace CZY.SlackToolBox.FastExtend.Window
{
    /// <summary>
    /// windwos 窗体缓存管理
    /// </summary>
    public static class WindowManager
    {
        class WindowInfo
        {
            public Type WindowType { get; set; }
            public System.Windows.Window Owner { get; set; }
        }
        static Dictionary<string, WindowInfo> _regWindowContainer = new Dictionary<string, WindowInfo>();

        /// <summary>
        /// 注册窗口类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        public static void RegisterWindow<T>(this string name, System.Windows.Window owner = null)
        {
            if (!_regWindowContainer.ContainsKey(name))
            {
                _regWindowContainer.Add(name, new WindowInfo { WindowType = typeof(T), Owner = owner });
            }
        }


        /// <summary>
        /// 获取窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        public static System.Windows.Window GetWindow<T>(this string name, System.Windows.Window owner = null)
        {
            if (_regWindowContainer.ContainsKey(name))
            {
                Type type = _regWindowContainer[name].WindowType;
                var window = (System.Windows.Window)Activator.CreateInstance(type);
                window.Owner = _regWindowContainer[name].Owner;
                return window;
            }
            return null;
        }

        /// <summary>
        /// 获取显示窗口对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="dataContext"></param>
        /// <returns></returns>
        public static bool ShowDialog<T>(this string name, T dataContext)
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
