using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class InfoHandle
	{
		/// <summary>
		/// 是否为弱密码
		/// 注:密码必须包含数字、小写字母、大写字母和其他符号中的两种并且长度大于8
		/// </summary>
		/// <param name="pwd">密码</param>
		/// <returns></returns>
		public static bool IsWeakPwd(this string pwd)
		{
			if (string.IsNullOrEmpty(pwd))
				throw new Exception("pwd不能为空");

			string pattern = "(^[0-9]+$)|(^[a-z]+$)|(^[A-Z]+$)|(^.{0,8}$)";
			if (Regex.IsMatch(pwd, pattern))
				return true;
			else
				return false;
		}
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
