using System;

namespace  CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// 验证某个操作是否频繁操作
    /// </summary>
    public static class RepeatInputTool
	{
		//最后一次操作时间
		private static DateTime _lastTime = DateTime.MinValue;
		/// <summary>
		/// 验证距离上次执行 是否炒过间隔
		/// </summary>
		/// <param name="intervalTime">间隔时间 毫秒</param>
		/// <returns></returns>
		public static bool CanExecute(this int intervalTime)
		{
			var now = DateTime.Now;
			if (now.Subtract(_lastTime) < TimeSpan.FromMilliseconds(intervalTime))
				return false;
			_lastTime = now;
			return true;
		}
	}
}
