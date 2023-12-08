using System;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// 异步运行
    /// </summary>
    public static class DelegateAsync
    {
        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="firstFunc">首先执行的方法</param>
        /// <param name="next">接下来执行的方法</param>
        public static Task DoneRunAsync(this Action firstFunc)
        {
            Task firstTask = new Task(() =>
            {
                firstFunc();
            });
            firstTask.Start();
            return firstTask;
        }

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="firstFunc">首先执行的方法</param>
        /// <param name="next">接下来执行的方法</param>
        public static Task DoneRunAsync(this Func<object> firstFunc )
        {
            Task<object> task = new Task<object>(() =>
            {
                return firstFunc();
            });
            task.Start();
            return task;
        }  
    }
     
}
