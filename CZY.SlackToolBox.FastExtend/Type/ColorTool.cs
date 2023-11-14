using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CZY.SlackToolBox.FastExtend.Type
{
    public static class ColorTool
    {

        /// <summary>
        /// 将Color转换为字符串
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHex(this Color color)
        {
            //return "#" + String.Format("{0:X}", Color.FromArgb(_color.R, _color.G, _color.B).ToArgb()).Substring(2);
            return Color.FromArgb(color.R, color.G, color.B, color.A).ToString();
        }


    }
}
