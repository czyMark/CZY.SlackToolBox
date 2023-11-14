using System.Drawing;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class RectangleTool
    {

        /// <summary>
        /// 将矩形框在指定高度画布上做垂直翻转
        /// </summary>
        /// <param name="rectangle">矩形框</param>
        /// <param name="height">指定画布高度</param>
        /// <returns>翻转后的新矩形框</returns>
        public static Rectangle VerticalFlip(this Rectangle rectangle, int height) => new Rectangle(rectangle.X, height - rectangle.Y - rectangle.Height, rectangle.Width, rectangle.Height);

        /// <summary>
        /// 将矩形框在指定高度画布上做水平翻转
        /// </summary>
        /// <param name="rectangle">矩形框</param>
        /// <param name="width">指定画布宽度</param>
        /// <returns>翻转后的新矩形框</returns>
        public static Rectangle HorizontalFlip(this Rectangle rectangle, int width) => new Rectangle(width - rectangle.X - rectangle.Width, rectangle.Y, rectangle.Width, rectangle.Height);

        /// <summary>
        /// 将矩形框在指定宽度、高度的画布上做翻转
        /// </summary>
        /// <param name="rectangle">矩形框</param>
        /// <param name="width">指定画布宽度</param>
        /// <param name="height">指定画布高度</param>
        /// <returns>翻转后的新矩形框</returns>
        public static Rectangle Flip(this Rectangle rectangle, int width, int height) => new Rectangle(width - rectangle.X - rectangle.Width, height - rectangle.Y - rectangle.Height, rectangle.Width, rectangle.Height);

        /// <summary>
        /// 对矩形框横向和纵向做偏移
        /// </summary>
        /// <param name="rectangle">矩形框</param>
        /// <param name="x">横向偏移量</param>
        /// <param name="y">纵向偏移量</param>
        /// <returns>偏移后的新的矩形框</returns>
        public static Rectangle OffsetEx(this Rectangle rectangle, int x, int y)
        {
            rectangle.Offset(x, y);
            return rectangle;
        }

    }
}
