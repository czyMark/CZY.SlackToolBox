using System;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class LongTool
    {
        /// <summary>
        /// jsGetTime转为DateTime
        /// </summary>
        /// <param name="jsGetTime">js中Date.getTime()</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long jsGetTime)
        {
            DateTime dtStart = new DateTime(1970, 1, 1).ToLocalTime();
            long lTime = long.Parse(jsGetTime + "0000");  //说明下，时间格式为13位后面补加4个"0"，如果时间格式为10位则后面补加7个"0",至于为什么我也不太清楚，也是仿照人家写的代码转换的
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow); 

            return dtResult;
        }
        public static string ToPercent(this long data)
        {
            return data.ToString() + "%";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToTime(this long data)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(data));
             
            string str = "";
            if (ts.Days > 0)
            {
                str = ts.Hours.ToString() + "天" + ts.Hours.ToString() + "时" + ts.Minutes.ToString() + "分 " + ts.Seconds + "秒";
            }
            if (ts.Days == 0 && ts.Hours > 0)
            {
                str = ts.Hours.ToString() + "时" + ts.Minutes.ToString() + "分 " + ts.Seconds + "秒";
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString() + "分" + ts.Seconds + "秒";
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = ts.Seconds + "秒";
            }
            return str;
        }
    }
}
