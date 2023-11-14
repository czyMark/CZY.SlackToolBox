using System;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class LongTool
    {
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
