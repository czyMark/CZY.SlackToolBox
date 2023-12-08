using QRCoder;
using System.Drawing;

namespace CZY.SlackToolBox.FastExtend.BarCode
{
    public static class QRCodeTool
    {
        #region 生成二维码

        /// <summary>
        /// 生成二维码，默认边长为250px
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <returns></returns>
        public static Image ToQRCode(this string content)
        {
            return content.ToQRCode(250, Color.White, Color.Black);
        }

        /// <summary>
        /// 生成二维码,自定义边长
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="imgSize">二维码边长px</param>
        /// <returns></returns>
        public static Image ToQRCode(this string content, int imgSize)
        {
            return content.ToQRCode(imgSize, Color.White, Color.Black);
        }

        /// <summary>
        /// 生成二维码
        /// 注：自定义边长以及颜色
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="imgSize">二维码边长px</param>
        /// <param name="background">二维码底色</param>
        /// <param name="foreground">二维码前景色</param>
        /// <returns></returns>
        public static Image ToQRCode(this string content, int imgSize, Color background, Color foreground)
        {
            return content.ToAddLogoQRCode(imgSize, background, foreground, null);
        }

        /// <summary>
        /// 生成二维码并添加Logo
        /// 注：默认生成边长为250px的二维码
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="logo">logo图片</param>
        /// <returns></returns>
        public static Image ToAddLogoQRCode(this string content, Bitmap logo)
        {
            return ToAddLogoQRCode(content, 250, Color.White, Color.Black, logo);
        }

        /// <summary>
        /// 生成二维码并添加Logo
        /// 注：自定义边长
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="imgSize">二维码边长px</param>
        /// <param name="logo">logo图片</param>
        /// <returns></returns>
        public static Image ToAddLogoQRCode(this string content, int imgSize, Bitmap logo)
        {
            return ToAddLogoQRCode(content, imgSize, Color.White, Color.Black, logo);
        }

        /// <summary>
        /// 生成二维码并添加Logo
        /// 注：自定义边长以及颜色
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="imgSize">二维码边长px</param>
        /// <param name="background">二维码底色</param>
        /// <param name="foreground">二维码前景色</param>
        /// <param name="logo">logo图片</param>
        /// <returns></returns>
        public static Image ToAddLogoQRCode(this string content, int imgSize, Color background, Color foreground, Bitmap logo)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var ppm = imgSize / qrCodeData.ModuleMatrix.Count;
            Bitmap qrCodeImage = qrCode.GetGraphic(ppm, foreground, background, logo);

            return qrCodeImage;
        }

        #endregion
    }
}
