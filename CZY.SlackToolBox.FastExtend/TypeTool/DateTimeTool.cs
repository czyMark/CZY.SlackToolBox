using System;

namespace  CZY.SlackToolBox.FastExtend
{

    public static class DateTimeTool
	{
		 
		/// <summary>
		/// 将DateTime 转换成 SystemTime
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static SystemTime ToSystemTime(this DateTime dateTime)
		{
			return new SystemTime()
			{
				year = (ushort)dateTime.Year,
				month = (ushort)dateTime.Month,
				day = (ushort)dateTime.Day,
				hour = (ushort)dateTime.Hour,
				minute = (ushort)dateTime.Minute,
				second = (ushort)dateTime.Second,
				milliseconds = (ushort)dateTime.Millisecond
			};
		}

		/// <summary>
		/// 将系统SystemTime结构体转换成DateTime
		/// </summary>
		/// <param name="systemTime"></param>
		/// <returns></returns>
		public static DateTime ToDateTime(this SystemTime systemTime)
		{
			return new DateTime(systemTime.year, systemTime.month, systemTime.day, systemTime.hour, systemTime.minute, systemTime.second, systemTime.milliseconds);
		}







		#region 计算时间差
		 
		/// <summary>
		/// 计算时间差 差值单位毫秒
		/// </summary>
		/// <param name="DateTime1">要操作的时间</param>
		/// <param name="DateTime2">要减去的时间</param>
		/// <returns></returns>
		public static double DateDiffMilliseconds(this DateTime DateTime1, DateTime DateTime2)
		{
			TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
			TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
			TimeSpan ts = ts1.Subtract(ts2).Duration();
			return ts.TotalMilliseconds;
		}

		/// <summary>
		/// 计算时间差 差值秒
		/// </summary>
		/// <param name="DateTime1"></param>
		/// <param name="DateTime2"></param>
		/// <returns></returns>
		public static double DateDiffSeconds(this DateTime DateTime1, DateTime DateTime2)
		{
			TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
			TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
			TimeSpan ts = ts1.Subtract(ts2).Duration();
			return ts.TotalSeconds;
		}

		/// <summary>
		/// 计算时间差 差值分钟
		/// </summary>
		/// <param name="DateTime1"></param>
		/// <param name="DateTime2"></param>
		/// <returns></returns>
		public static double DateDiffMinutes(this DateTime DateTime1, DateTime DateTime2)
		{
			TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
			TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
			TimeSpan ts = ts1.Subtract(ts2).Duration();
			return ts.Minutes;
		}
		/// <summary>
		/// 计算时间差 差值小时
		/// </summary>
		/// <param name="DateTime1"></param>
		/// <param name="DateTime2"></param>
		/// <returns></returns>
		public static double DateDiffHours(this DateTime DateTime1, DateTime DateTime2)
		{
			TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
			TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
			TimeSpan ts = ts1.Subtract(ts2).Duration();
			return ts.TotalHours;
		}
		/// <summary>
		/// 计算时间差 差值天
		/// </summary>
		/// <param name="DateTime1"></param>
		/// <param name="DateTime2"></param>
		/// <returns></returns>
		public static double DateDiffDays(this DateTime DateTime1, DateTime DateTime2)
		{
			TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
			TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
			TimeSpan ts = ts1.Subtract(ts2).Duration();
			return ts.TotalDays;
		} 
		#endregion
	}
}
