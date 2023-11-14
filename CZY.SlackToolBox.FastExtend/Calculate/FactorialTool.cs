namespace CZY.SlackToolBox.FastExtend
{
    public static class FactorialTool
	{
		#region 运算方法
		/// <summary>
		/// 计算组合数
		/// </summary>
		/// <param name="total"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string Combination(int total, int num)
		{
			string result = "";
			string dividend = Factorial(num).Multiply(Factorial(total - num));
			string divisor = Factorial(total);
			result = divisor.Divide(dividend);
			return result;
		}
		/// <summary>
		/// 计算num的阶乘
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static string Factorial(this int num)
		{
			string result = "";
			result = num > 0 ? num.ToString().Multiply(Factorial(num - 1)) : "1";
			return result;
		} 
		#endregion
	}
}
