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


		/// <summary>
		/// 获取Js格式的timestamp
		/// </summary>
		/// <param name="dateTime">日期</param>
		/// <returns></returns>
		public static long ToJsTimestamp(this DateTime dateTime)
		{
			var startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
			long result = (dateTime.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位
			return result;
		}


		/// <summary>
		/// 获取js中的getTime()
		/// </summary>
		/// <param name="dt">日期</param>
		/// <returns></returns>
		public static Int64 JsGetTime(this DateTime dt)
		{
			Int64 retval = 0;
			var st = new DateTime(1970, 1, 1);
			TimeSpan t = (dt.ToUniversalTime() - st);
			retval = (Int64)(t.TotalMilliseconds + 0.5);
			return retval;
		}

		/// <summary>
		/// 返回默认时间1970-01-01
		/// </summary>
		/// <param name="dt">时间日期</param>
		/// <returns></returns>
		public static DateTime Default(this DateTime dt)
		{
			return DateTime.Parse("1970-01-01");
		}

		///// <summary>
		///// 转为标准时间（北京时间，解决Linux时区问题）
		///// </summary>
		///// <param name="dt">当前时间</param>
		///// <returns></returns>
		//public static DateTime ToCstTime(this DateTime dt)
		//{
		//	Instant now = SystemClock.Instance.GetCurrentInstant();
		//	var shanghaiZone = DateTimeZoneProviders.Tzdb["Asia/Shanghai"];
		//	return now.InZone(shanghaiZone).ToDateTimeUnspecified();
		//}



		/// <summary>
		/// 转为本地时间
		/// </summary>
		/// <param name="time">时间</param>
		/// <returns></returns>
		public static DateTime ToLocalTime(this DateTime time)
		{
			return TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local);
		}

		/// <summary>
		/// 转为转换为Unix时间戳格式(精确到秒)
		/// </summary>
		/// <param name="time">时间</param>
		/// <returns></returns>
		public static int ToUnixTimeStamp(this DateTime time)
		{
			DateTime startTime = new DateTime(1970, 1, 1).ToLocalTime();
			return (int)(time - startTime).TotalSeconds;
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
