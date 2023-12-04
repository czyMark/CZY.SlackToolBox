using System;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// 委托帮助类
    /// </summary>
    public static class DelegateHelper
    {
        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="firstFunc">首先执行的方法</param>
        /// <param name="next">接下来执行的方法</param>
        public static void DoneRunAsync(this Action firstFunc, Action next)
        {
            Task firstTask = new Task(() =>
            {
                firstFunc();
            });

            firstTask.Start();
            firstTask.ContinueWith(x => next());
        }

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="firstFunc">首先执行的方法</param>
        /// <param name="next">接下来执行的方法</param>
        public static void DoneRunAsync(this Func<object> firstFunc, Action<object> next)
        {
            Task<object> firstTask = new Task<object>(() =>
            {
                return firstFunc();
            });

            firstTask.Start();
            firstTask.ContinueWith(x => next(x.Result));
        }
         

    }
}
