using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class StringTool
	{

		/// <summary>
		/// 通过指定的地址保存图片
		/// </summary>
		/// <param name="picPath">图片地址</param>
		/// <param name="img">图片数组</param>
		/// <returns></returns>
		public static int SaveBitmapImage(this string picPath, byte[] img)
		{
			int result = 1;
			try
			{
				MemoryStream ms = new MemoryStream(img);
				System.Drawing.Image bm = System.Drawing.Image.FromStream(ms, true);
				//Bitmap bmp = new Bitmap(ms);
				bm.Save(picPath);
				ms.Close();
			}
			catch
			{
				result = 0;
			}

			return result;
		}

		/// <summary>
		/// 时间戳反转为时间
		/// </summary>
		/// <param name="TimeStamp">时间戳</param>
		/// <param name="AccurateToMilliseconds">是否精确到毫秒</param>
		/// <returns>返回一个日期时间</returns>
		public static DateTime StampToTime(this string TimeStamp, bool AccurateToMilliseconds = false)
		{
			long timeStamp = long.Parse(TimeStamp);
			System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
			if (AccurateToMilliseconds)
			{
				return startTime.AddTicks(timeStamp * 10000);
			}
			else
			{
				return startTime.AddTicks(timeStamp * 10000000);
			}
		}

		/// <summary>
		/// 时间字符串转成时间
		/// </summary>
		/// <param name="TimeStr">时间字符串</param>
		/// <param name="AccurateToMilliseconds">是否精确到毫秒</param>
		/// <returns>返回一个日期时间</returns>
		public static DateTime StrToTime(this string TimeStr, bool AccurateToMilliseconds = false)
		{
			//截取字符串。
			string temp= TimeStr.Substring(0, 4) + "/" + TimeStr.Substring(4, 2) + "/" + TimeStr.Substring(6, 2) + " " + TimeStr.Substring(8, 2) + ":" + TimeStr.Substring(10, 2) + ":" + TimeStr.Substring(12, 2);
			if (AccurateToMilliseconds)
			{
				temp += " " + TimeStr.Substring(14, TimeStr.Length - 1);
			}
			return DateTime.Parse(temp);
		}

		/// <summary>
		/// 时间字符串转成时间字符串
		/// </summary>
		/// <param name="TimeStr">时间字符串</param>
		/// <param name="AccurateToMilliseconds">是否精确到毫秒</param>
		/// <returns>返回一个日期时间</returns>
		public static string StrToTimeStr(this string TimeStr, bool AccurateToMilliseconds = false)
		{
			//截取字符串。
			string temp = TimeStr.Substring(0, 4) + "/" + TimeStr.Substring(4, 2) + "/" + TimeStr.Substring(6, 2) + " " + TimeStr.Substring(8, 2) + ":" + TimeStr.Substring(10, 2) + ":" + TimeStr.Substring(12, 2);
			if (AccurateToMilliseconds)
			{
				temp += " " + TimeStr.Substring(14, TimeStr.Length - 1);
			}
			return temp;
		}

		/// <summary>
		/// 转int类型
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int ToInt(this string str)
		{
			int i = 0;
			int.TryParse(str, out i);
			if (string.IsNullOrEmpty(str))
				i = -1;
			return i;
		}
		/// <summary>
		/// 转double类型
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static double ToDouble(this string str)
		{
			double i = 0;
			double.TryParse(str, out i);
			if (string.IsNullOrEmpty(str))
				i = -1;
			return i;
		}


		/// <summary>
		/// 转long类型
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static long ToLong(this string str)
		{
			long i = 0;
			long.TryParse(str, out i);
			if (string.IsNullOrEmpty(str))
				i = -1;
			return i;
		}


		/// <summary>
		/// 二进制字符串转为Int
		/// </summary>
		/// <param name="str">二进制字符串</param>
		/// <returns></returns>
		public static int BinToInt(this string str)
		{
			return Convert.ToInt32(str, 2);
		}

		/// <summary>
		/// 将16进制字符串转为Int
		/// </summary>
		/// <param name="str">数值</param>
		/// <returns></returns>
		public static int HexToInt(this string str)
		{
			int num = Int32.Parse(str, NumberStyles.HexNumber);
			return num;
		}

		/// <summary>
		/// 将16进制字符串转为Byte数组
		/// </summary>
		/// <param name="str">16进制字符串(2个16进制字符表示一个Byte)</param>
		/// <returns></returns>
		public static byte[] ToHexBytes(this string str)
		{
			List<byte> resBytes = new List<byte>();
			for (int i = 0; i < str.Length; i = i + 2)
			{
				string numStr = $@"{str[i]}{str[i + 1]}";
				resBytes.Add((byte)numStr.HexToInt());
			}

			return resBytes.ToArray();
		}


		/// <summary>
		/// 将ASCII码形式的字符串转为对应字节数组
		/// 注：一个字节一个ASCII码字符
		/// </summary>
		/// <param name="str">字符串</param>
		/// <returns></returns>
		public static byte[] ToASCIIBytes(this string str)
		{
			return str.ToList().Select(x => (byte)x).ToArray();
		}

		/// <summary>
		/// base64字符串转为字节数组
		/// </summary>
		/// <param name="base64Str">base64字符串</param>
		/// <returns></returns>
		public static byte[] FromBase64ToBytes(this string str)
		{
			return Convert.FromBase64String(str);
		}
		/// <summary>
		/// string转byte[]
		/// </summary>
		/// <param name="str">字符串</param>
		/// <returns></returns>
		public static byte[] ToBytes(this string str)
		{
			return Encoding.Default.GetBytes(str);
		}

		/// <summary>
		/// string转byte[]
		/// </summary>
		/// <param name="str">字符串</param>
		/// <param name="theEncoding">需要的编码</param>
		/// <returns></returns>
		public static byte[] ToBytes(this string str, Encoding theEncoding)
		{
			return theEncoding.GetBytes(str);
		}


		/// <summary>
		/// 删除字符串中的@符号
		/// </summary>
		/// <param name="jsonStr">json字符串</param>
		/// <returns></returns>
		public static string RemoveAtChar(this string str)
		{
			Regex reg = new Regex("\"@([^ \"]*)\"\\s*:\\s*\"(([^ \"]+\\s*)*)\"");
			string strPatten = "\"$1\":\"$2\"";
			return reg.Replace(str, strPatten);
		}

		/// <summary>
		/// json数据转实体类,仅仅应用于单个实体类，速度非常快
		/// </summary>
		/// <typeparam name="T">泛型参数</typeparam>
		/// <param name="json">json字符串</param>
		/// <returns></returns>
		public static T JsonToOnlyOneEntity<T>(this string json)
		{
			if (json == null || json == "")
				return default(T);

			System.Type type = typeof(T);
			object obj = Activator.CreateInstance(type, null);

			foreach (var item in type.GetProperties())
			{
				PropertyInfo info = obj.GetType().GetProperty(item.Name);
				string pattern;
				pattern = "\"" + item.Name + "\":\"(.*?)\"";
				foreach (Match match in Regex.Matches(json, pattern))
				{
					switch (item.PropertyType.ToString())
					{
						case "System.String": info.SetValue(obj, match.Groups[1].ToString(), null); break;
						case "System.Int32": info.SetValue(obj, match.Groups[1].ToString().ToInt(), null); ; break;
						case "System.Int64": info.SetValue(obj, Convert.ToInt64(match.Groups[1].ToString()), null); ; break;
						case "System.DateTime": info.SetValue(obj, Convert.ToDateTime(match.Groups[1].ToString()), null); ; break;
					}
				}
			}
			return (T)obj;
		}


		/// <summary>
		/// 类型T必须具备
		/// (1)无参构造方法 
		/// (2)方法签名为 bool TryParse(string,T)的方法
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		[SecuritySafeCritical]
		public static bool Is<T>(this string value) where T : IConvertible
		{
			var type = typeof(T);
			System.Type[] types = { typeof(string), type.MakeByRefType() };
			var Method = type.GetMethod("TryParse", types);
			if (Method == null)
			{
				return false;
			}
			try
			{
				T Convertible = Activator.CreateInstance<T>();
				return (bool)Method.Invoke(Convertible, new object[] { value, Convertible });
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// 转为首字母大写
		/// </summary>
		/// <param name="str">字符串</param>
		/// <returns></returns>
		public static string ToFirstUpperStr(this string str)
		{
			return str.Substring(0, 1).ToUpper() + str.Substring(1);
		}

		/// <summary>
		/// 转为首字母小写
		/// </summary>
		/// <param name="str">字符串</param>
		/// <returns></returns>
		public static string ToFirstLowerStr(this string str)
		{
			return str.Substring(0, 1).ToLower() + str.Substring(1);
		}

		/// <summary>
		/// 转为网络终结点IPEndPoint
		/// </summary>=
		/// <param name="str">字符串</param>
		/// <returns></returns>
		public static IPEndPoint ToIPEndPoint(this string str)
		{
			IPEndPoint iPEndPoint = null;
			try
			{
				string[] strArray = str.Split(':').ToArray();
				string addr = strArray[0];
				int port = Convert.ToInt32(strArray[1]);
				iPEndPoint = new IPEndPoint(IPAddress.Parse(addr), port);
			}
			catch
			{
				iPEndPoint = null;
			}

			return iPEndPoint;
		}




		/// <summary>
		/// 将字符串转换为Color
		/// </summary>
		/// <param name="color">带#号的16进制颜色</param>
		/// <returns></returns>
		public static Color HexToColor(this string color)
		{
			int red, green, blue, alpha;
			char[] rgb;
			color = color.TrimStart('#');
			color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
			switch (color.Length)
			{
				case 3:
					rgb = color.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
					green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
					blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
					return Color.FromRgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

				case 4:
					rgb = color.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
					green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
					blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
					alpha = Convert.ToInt32(rgb[3].ToString() + rgb[3].ToString(), 16);
					return Color.FromArgb(Convert.ToByte(alpha), Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

				case 6:
					rgb = color.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
					green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
					blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
					return Color.FromRgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

				case 8:
					rgb = color.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
					green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
					blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
					alpha = Convert.ToInt32(rgb[6].ToString() + rgb[7].ToString(), 16);
					return Color.FromArgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue), Convert.ToByte(alpha));
				default:
					return Colors.Black;

			}
		}


		/// <summary>
		/// 将#号的16进制颜色变成透明颜色
		/// </summary>
		/// <param name="color">带#号的16进制颜色</param>
		/// <param name="alpha">透明数值0-255</param>
		/// <returns></returns>
		public static Color ColorAlpha(this string color, Byte alpha)
		{
			int red, green, blue;
			char[] rgb;
			color = color.TrimStart('#');
			color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
			switch (color.Length)
			{
				case 3:
					rgb = color.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
					green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
					blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
					return Color.FromRgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

				case 4:
					rgb = color.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
					green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
					blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
					return Color.FromArgb(alpha, Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

				case 6:
					rgb = color.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
					green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
					blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
					return Color.FromRgb(Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));

				case 8:
					rgb = color.ToCharArray();
					red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
					green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
					blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
					return Color.FromArgb(alpha, Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));
				default:
					return Colors.Black;

			}
		}







        /// <summary>
        /// 从base64字符串读入图片
        /// </summary>
        /// <param name="base64">base64字符串</param>
        /// <returns></returns>
        public static System.Drawing.Image FromBase64ToImage(this string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
            return img;
        }
    }
}
