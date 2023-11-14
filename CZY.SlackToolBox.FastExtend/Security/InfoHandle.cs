using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class InfoHandle
	{

		/// <summary>
		/// 隐藏关键信息
		/// </summary>
		/// <param name="str"></param>
		/// <param name="mask"></param>
		/// <param name="maskLength"></param>
		/// <returns></returns>
		public static string MaskInfo(this string str, char mask = '*', int maskLength = 4)
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				return str;
			}
			str = str.Trim();
			string masks = mask.ToString().PadLeft(maskLength, mask);


			//特殊加密  -- 邮箱加密
			if (Regex.IsMatch(str, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
			{
				int suffixlen = str.Length - str.LastIndexOf('@');
				string suffix = str.Substring(str.Length - suffixlen, suffixlen);
				string num = str.Substring(0, str.Length - suffixlen);
				str = $"{num.MaskInfo(mask, maskLength)}{suffix}";
				return str;
			}


			if (str.Length >= 11)
			{
				str = Regex.Replace(str, "(.{3}).*(.{4})", $"$1{masks}$2");
			}
			else if (masks.Length == 10)
			{

				str = Regex.Replace(str, "(.{3}).*(.{3})", $"$1{masks}$2");
			}
			else if (masks.Length == 9)
			{

				str = Regex.Replace(str, "(.{2}).*(.{3})", $"$1{masks}$2");
			}
			else if (masks.Length == 8)
			{

				str = Regex.Replace(str, "(.{2}).*(.{2})", $"$1{masks}$2");
			}
			else if (masks.Length == 7)
			{

				str = Regex.Replace(str, "(.{1}).*(.{2})", $"$1{masks}$2");
			}
			else if (masks.Length == 6)
			{
				str = Regex.Replace(str, "(.{1}).*(.{1})", $"$1{masks}$2");
			}
			else
			{
				str = Regex.Replace(str, "(.{1}).*", $"$1{masks}");
			}
			return str;
		}


    }
}
