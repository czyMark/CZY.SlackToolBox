using System.Windows.Media;

namespace CZY.SlackToolBox.FastExtend
{
    public static class ColorTool
    {

        /// <summary>
        /// 将Color转换为字符串
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHex(this Color color)
        {
            //return "#" + String.Format("{0:X}", Color.FromArgb(_color.R, _color.G, _color.B).ToArgb()).Substring(2);
            return Color.FromArgb(color.R, color.G, color.B, color.A).ToString();
        }


    }
}
