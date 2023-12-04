using System.Collections.Generic;

namespace CZY.SlackToolBox.FastExtend
{
    public static class ChartTool
	{

		/// <summary>
		/// 转换成string
		/// </summary>
		/// <param name="cNum"></param>
		/// <returns></returns>
		public static string ConvertString(this List<char> cNum)
		{
			if (cNum.Count == 0)
				return "0";

			while (cNum[0] == '0' && cNum.Count > 1)
				cNum.RemoveAt(0);
			return string.Join("", cNum);
		}
	}
}
