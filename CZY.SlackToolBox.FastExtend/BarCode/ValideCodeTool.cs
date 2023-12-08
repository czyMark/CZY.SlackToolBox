using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CZY.SlackToolBox.FastExtend
{

    /// <summary>
    /// 验证码生成
    /// </summary>
    public static  class ValideCodeTool
	{
        /// <summary>
        /// 将字符串转内容绘制验证码类型的图片 
        /// </summary>
        /// <param name="code">生成图片的验证码</param>
        /// <returns></returns>
        public static Bitmap ToValideImage(this string code)
        {
            Bitmap Img = null;
            Graphics g = null;
            System.Random random = new System.Random();
            //验证码颜色集合  
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //验证码字体集合  
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

            //定义图像的大小，生成图像的实例  
            Img = new Bitmap((int)code.Length * 18, 32);
            g = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象    
            g.Clear(Color.White);//背景设为白色  

            //在随机位置画背景点  
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(Img.Width);
                int y = random.Next(Img.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }
            //验证码绘制在g中  
            for (int i = 0; i < code.Length; i++)
            {
                int cindex = random.Next(7);//随机颜色索引值  
                int findex = random.Next(5);//随机字体索引值  
                Font f = new Font(fonts[findex], 15, FontStyle.Bold);//字体  
                Brush b = new SolidBrush(c[cindex]);//颜色  
                int ii = 4;
                if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                {
                    ii = 2;
                }
                g.DrawString(code.Substring(i, 1), f, b, 5 + (i * 15), ii);//绘制一个验证字符  
            }
            //MemoryStream ms = new MemoryStream();//生成内存流对象  
            //Img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中  
            //ms.Seek(0, SeekOrigin.Begin);//指针回归

            //回收资源  
            g.Dispose(); 
            return Img;
        }


        /// <summary>
        /// 将字符串转内容生成验证码类型的图片缓存
        /// </summary>
        /// <param name="code">生成图片的验证码缓存</param>
        /// <returns></returns>
        public static MemoryStream ToValideImageMemory(this string code)
        {
            Bitmap Img = null;
            Graphics g = null;
            System.Random random = new System.Random();
            //验证码颜色集合  
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //验证码字体集合  
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

            //定义图像的大小，生成图像的实例  
            Img = new Bitmap((int)code.Length * 18, 32);
            g = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象    
            g.Clear(Color.White);//背景设为白色  

            //在随机位置画背景点  
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(Img.Width);
                int y = random.Next(Img.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }
            //验证码绘制在g中  
            for (int i = 0; i < code.Length; i++)
            {
                int cindex = random.Next(7);//随机颜色索引值  
                int findex = random.Next(5);//随机字体索引值  
                Font f = new Font(fonts[findex], 15, FontStyle.Bold);//字体  
                Brush b = new SolidBrush(c[cindex]);//颜色  
                int ii = 4;
                if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                {
                    ii = 2;
                }
                g.DrawString(code.Substring(i, 1), f, b, 5 + (i * 15), ii);//绘制一个验证字符  
            }
			MemoryStream ms = new MemoryStream();//生成内存流对象  
			Img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中  
			ms.Seek(0, SeekOrigin.Begin);//指针回归

			//回收资源  
			g.Dispose();
            Img.Dispose();
            return ms;
        }
    }
}
