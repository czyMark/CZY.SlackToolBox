using System.Windows.Media;

namespace  CZY.SlackToolBox.FastExtend
{
	public class PageAdaptiveTool
	{
		/// <summary>
		/// 获取控件自适应缩放比
		/// </summary>
		/// <param name="RunWidth">程序运行动态获取的宽</param>
		/// <param name="RunHeight">程序运行动态获取的高</param>
		/// <param name="DevWidth">开发时自适应缩放宽</param>
		/// <param name="DevHeight">开发时自适应缩放高</param>
		/// <returns></returns>
		public static ScaleTransform GetScale(double RunWidth = 1920, double RunHeight = 1080, double Width = 1920, double Height = 1080)
		{
			double Pix = RunWidth;
			double Piy = RunHeight;
			double ScaleX = Pix / Width;
			double ScaleY = Piy / Height;

			double ScaleXY = 0;
			if (ScaleX > ScaleY)
			{
				ScaleXY = ScaleY;
			}
			else
			{
				ScaleXY = ScaleX;
			}

			ScaleTransform st = new ScaleTransform();
			st.ScaleX = ScaleXY;
			st.ScaleY = ScaleXY;
			return st; 
		}
	}
}
