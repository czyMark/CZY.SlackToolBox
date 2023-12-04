using System;
using System.ComponentModel;
using System.Net;
using System.Text;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class ObjectTool
	{ 
		public static string  ToString(this object obj)
		{
			if (obj == null)
				return string.Empty;
			string str = obj.ToString();
			return str;
		}

        /// <summary>
        /// 变成网页地址后的参数
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToHttpParameter(this object source)
        {
            var buff = new StringBuilder(string.Empty);
            if (source == null)
                throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                object value = property.GetValue(source);
                if (value != null)
                {
                    buff.Append(WebUtility.UrlEncode(property.Name) + "=" + WebUtility.UrlEncode(value + "") + "&");
                }
            }
            return buff.ToString().Trim('&');
        }
    }
}
