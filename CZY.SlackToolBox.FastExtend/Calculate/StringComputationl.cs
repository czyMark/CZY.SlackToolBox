using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZY.SlackToolBox.FastExtend
{
	public static class StringComputationl
	{
		#region 运算方法

		/// <summary>
		/// 两个string数字相除
		/// </summary>
		/// <param name="divisor"></param>
		/// <param name="dividend"></param>
		/// <returns></returns>
		public static string Divide(this string divisor, string dividend)
		{
			string result = "";
			List<char> cDivisor = divisor.ToCharArray().ToList<char>();
			List<char> cDividend = dividend.ToCharArray().ToList<char>();
			List<char> temp = new List<char>();
			List<char> cResult = new List<char>();
			int indexResult = -1;
			int indexDivisor = 0;

			// 除法的竖式计算
			while (indexDivisor != cDivisor.Count)
			{
				// 从前往后分离除数大于被除数的部分，结果存入temp
				try
				{
					for (; CompareTwoStringNum(temp.ConvertString(), cDividend.ConvertString()) < 0; indexDivisor++)
					{
						temp.Add(cDivisor[indexDivisor]);
						cResult.Add('0');
						indexResult++;
					}
				}
				catch
				{
					;
				}
				// 得到一位结果，存入cResult
				for (int i = 1; CompareTwoStringNum(temp.ConvertString(), Multiply(dividend, i.ToString())) >= 0; i++)
				{
					cResult[indexResult] = Convert.ToChar(i.ToString());
				}

				temp = Sub(temp.ConvertString(), Multiply(cDividend.ConvertString(), cResult[indexResult].ToString())).ToList<char>();
			}

			// 去除多余的0
			while (cResult[0] == '0' && cResult.Count > 1)
				cResult.RemoveAt(0);

			result = string.Join("", cResult);
			return result;
		}
		/// <summary>
		/// 多个string数字相乘
		/// </summary>
		/// <param name="nums"></param>
		/// <param name="isRecursion"></param>
		/// <returns></returns>
		public static string Multiply(List<string> nums, bool isRecursion = false)
		{
			string result = "";
			if (isRecursion)
				nums.RemoveAt(0);

			if (nums.Count > 0)
				result = Multiply(nums[0], Multiply(nums, true));
			else
				return "1";
			return result;
		}
		/// <summary>
		/// 两个string数字相乘
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		public static string Multiply(this string num1, string num2)
		{
			string result = "";
			List<string> temp = new List<string>();
			char[] Char1 = num1.ToCharArray();
			char[] Char2 = num2.ToCharArray();
			int index1 = Char1.Length - 1, index2 = Char2.Length - 1;
			int flag = 0;

			// 得到第一级相乘后的字符串数组
			for (int i = index2; i >= 0; i--)
			{
				List<char> c = new List<char>();
				for (int z = i; z < Char2.Length - 1; z++)
				{
					c.Add('0');
				}
				// 进行第一级相乘（倒序存放）
				for (int j = index1; j >= 0; j--)
				{
					int n = Convert.ToInt32(Char1[j].ToString()) * Convert.ToInt32(Char2[i].ToString()) + flag;
					flag = n > 9 ? n / 10 : 0;
					c.Add(n > 9 ? Convert.ToChar((n % 10).ToString()) : Convert.ToChar(n.ToString()));
				}
				// 组合成第一级相乘的字符串
				StringBuilder sb = new StringBuilder();
				if (flag > 0)
					sb.Append(flag.ToString());     // 最高位进位
				for (int k = c.Count - 1; k >= 0; k--)
				{
					sb.Append(c[k]);
				}
				temp.Add(sb.ToString());
				flag = 0;
			}

			// 字符串相加
			result = Add(temp);

			return result;
		}
		/// <summary>
		/// 计算组合数
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string Add(List<string> num)
		{
			string result = "";
			List<char[]> nChar = new List<char[]>();

			foreach (string s in num)
			{
				nChar.Add(s.ToCharArray());
			}

			int flag = 0;   // 进位
			int index = 1;  // 从后向前的索引
			bool continus;
			List<string> addResult = new List<string>();
			StringBuilder sb = new StringBuilder();
			do
			{
				int addNum = 0;
				continus = false;

				// 从后向前单列相加
				for (int i = 0; i < num.Count; i++)
				{
					int lastIndex = nChar[i].Length - index;
					addNum += lastIndex < 0 ? 0 : Convert.ToInt32(nChar[i][lastIndex].ToString());
					continus |= (lastIndex > 0);
				}
				index++;

				addResult.Add(((addNum + flag) % 10).ToString());
				flag = (addNum + flag) / 10;
				if (flag > 0 && !continus)
					addResult.Add(flag.ToString());
			} while (continus);
			for (int i = addResult.Count - 1; i >= 0; i--)
			{
				sb.Append(addResult[i]);
			}
			result = sb.ToString();

			List<char> cResult = result.ToCharArray().ToList<char>();
			// 去除多余的0
			while (cResult[0] == '0' && cResult.Count > 1)
				cResult.RemoveAt(0);
			result = string.Join("", cResult);

			return result;
		}
		/// <summary>
		/// 两个string数字相减
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		public static string Sub(this string num1, string num2)
		{
			string result = "";
			int flag = 0;
			if (num1.Length < num2.Length)
				return null;
			num2 = num2.PadLeft(num1.Length, '0');
			char[] cNum1 = num1.ToCharArray();
			char[] cNum2 = num2.ToCharArray();
			if (Convert.ToInt32(cNum1[0].ToString()) < Convert.ToInt32(cNum2[0].ToString()))
				return null;

			List<char> temp = new List<char>();
			for (int i = cNum1.Length - 1; i >= 0; i--)
			{
				if ((Convert.ToInt32(cNum1[i].ToString()) - flag) >= Convert.ToInt32(cNum2[i].ToString()))
				{
					temp.Add(Convert.ToChar((Convert.ToInt32(cNum1[i].ToString()) - flag - Convert.ToInt32(cNum2[i].ToString())).ToString()));
					flag = 0;
				}
				else
				{
					temp.Add(Convert.ToChar((Convert.ToInt32(cNum1[i].ToString()) - flag + 10 - Convert.ToInt32(cNum2[i].ToString())).ToString()));
					flag = 1;
				}
			}
			// 去除多余的0
			while (temp[temp.Count - 1] == '0' && temp.Count > 1)
				temp.RemoveAt(temp.Count - 1);

			StringBuilder sb = new StringBuilder();
			for (int i = temp.Count - 1; i >= 0; i--)
			{
				sb.Append(temp[i]);
			}
			result = sb.ToString();
			return result;
		}
		/// <summary>
		///  两个string数字比较大小，前面的大返回1
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		public static int CompareTwoStringNum(this string num1, string num2)
		{
			int result = 0;
			List<char> cNum1 = num1.ToCharArray().ToList<char>();
			List<char> cNum2 = num2.ToCharArray().ToList<char>();
			// 去除多余的0
			while (cNum1[0] == '0' && cNum1.Count > 1)
				cNum1.RemoveAt(0);
			while (cNum2[0] == '0' && cNum2.Count > 1)
				cNum2.RemoveAt(0);

			if (cNum1.Count > cNum2.Count)
				result = 1;
			else if (cNum1.Count < cNum2.Count)
				result = -1;
			else
			{
				for (int i = 0; i < cNum2.Count; i++)
				{
					if (cNum1[i] != cNum2[i])
					{
						result = cNum1[i] > cNum2[i] ? 1 : -1;
						break;
					}
				}
			}
			return result;
		}


		/// <summary>
		/// 公式数值计算
		/// </summary>
		/// <param name="formula">输入公式</param>
		/// <returns>返回计算得出的结果 如果是错误返回-Infinity</returns>
		public static string ToFormulaValueDataTable(this string formula)
		{
			object result = new System.Data.DataTable().Compute(formula, "");
			return result.ToString();
		}
		/// <summary>
		/// 将中序表达式转化成后序表达式
		/// </summary>
		/// <param name="formula"></param>
		/// <returns></returns>
		public static string ToFormulaValue(this string formula)
		{
			string result = formula.CalculateParenthesesExpression();
			return result.ToString();
		}
		/// <summary>
		/// 解公式
		/// </summary>
		/// <param name="formula"></param>
		/// <returns></returns>
		public static string ToFormula(this string formula)
		{
			string result = formula.CalculateParenthesesExpression();
			return result.ToString();
		}



		#endregion
		
	}
}
