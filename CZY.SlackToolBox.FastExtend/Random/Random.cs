using System;

namespace CZY.SlackToolBox.FastExtend
{


    public static class Random
	{
        public enum RandomNumFormat
        {
            /// <summary>
            /// .
            /// </summary>
            Point,
            /// <summary>
            /// ,
            /// </summary>
            Comma,
            /// <summary>
            /// /
            /// </summary>
            Slash
        }

        private static System.Random random = new System.Random();
		private static double oldNum;

        /// <summary>
        /// 生成指定位数的随机数
        /// </summary>
        /// <param name="count">随机数的位数</param>
        /// <returns>返回字符串的数字</returns>
        public static string GetNum(int count)
		{ 
            double temp = random.NextDouble();


			if (temp == oldNum)
			{
                return GetNum(count);
			}
			else
			{
				oldNum = temp;
			}

            if (count > 15)
            {
                return temp.ToString().Substring(2, 15) + GetNum(count - 15);
            }
            else
            {
                return temp.ToString().Substring(2, count);
            } 
		}



        /// <summary>
        /// 生成某个区间的整数
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns>返回整数</returns>
        public static int GetSimpNum(int min,int max)
        {
            int temp = random.Next(min, max);

            if (temp == oldNum)
            {
                return GetSimpNum(min, max);
            }
            else
            {
                oldNum = temp;
            }
            return temp;
        }

        /// <summary>
        /// 生成指定位数的随机带小数点的小数
        /// </summary>
        /// <param name="count">随机数的位数</param>
        /// <returns>返回字符串的数字</returns>
        public static string GetNum(int count,int decimalCount, RandomNumFormat randomNumFormat= RandomNumFormat.Point)
        {
			switch (randomNumFormat)
			{
				case RandomNumFormat.Point:
                    return $"{GetNum(count)}.{decimalCount}";
				case RandomNumFormat.Comma:
                    return $"{GetNum(count)},{decimalCount}";
				case RandomNumFormat.Slash:
                    return $"{GetNum(count)}/{decimalCount}";
				default:
                    return $"{GetNum(count)}.{decimalCount}";
			} 
        }
        /// <summary>
        /// 使用默认的字符集合生成 生成指定位数的随机字符
        /// </summary>
        /// <param name="VcodeNum">随机数的位数</param>
        /// <returns>返回一个随机数字符串</returns>
        public static string GetVcodeNum(int VcodeNum)
        {
            //验证码可以显示的字符集合
            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组
            string code = "";//产生的随机数
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数

            System.Random rand = new System.Random();
            //采用一个简单的算法以保证生成随机数的不同
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new System.Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类
                }
                int t = rand.Next(VcArray.Length);//获取随机数
                if (temp != -1 && temp == t)
                {
                    return GetVcodeNum(VcodeNum);//如果获取的随机数重复，则递归调用
                }
                temp = t;//把本次产生的随机数记录起来
                code += VcArray[t];//随机数的位数加一
            }
            return code;
        }

        /// <summary>
        /// 使用指定的字符集合 生成指定位数的随机字符
        /// </summary>
        /// <param name="VcodeNum">随机数的位数</param>
        /// <param name="Vchar">指定字符集合</param>
        /// <returns>返回一个随机数字符串</returns>
        public static string GetVcodeNum(int VcodeNum, Char[] VcArray)
        {
          
            string code = "";//产生的随机数
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数

            System.Random rand = new System.Random();
            //采用一个简单的算法以保证生成随机数的不同
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new System.Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类
                }
                int t = rand.Next(VcArray.Length);//获取随机数
                if (temp != -1 && temp == t)
                {
                    return GetVcodeNum(VcodeNum, VcArray);//如果获取的随机数重复，则递归调用
                }
                temp = t;//把本次产生的随机数记录起来
                code += VcArray[t];//随机数的位数加一
            }
            return code;
        }

        /// <summary>
        /// 使用指定的字符串集合 生成指定位数的随机字符
        /// </summary>
        /// <param name="VcodeNum">随机数的位数</param>
        /// <param name="Vchar">指定字符串集合</param>
        /// <returns>返回一个随机数字符串</returns>
        public static string GetVcodeNum(int VcodeNum, string[] VcArray)
        {

            string code = "";//产生的随机数
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数

            System.Random rand = new System.Random();
            //采用一个简单的算法以保证生成随机数的不同
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new System.Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类
                }
                int t = rand.Next(VcArray.Length);//获取随机数
                if (temp != -1 && temp == t)
                {
                    return GetVcodeNum(VcodeNum, VcArray);//如果获取的随机数重复，则递归调用
                }
                temp = t;//把本次产生的随机数记录起来
                code += VcArray[t];//随机数的位数加一
            }
            return code;
        }
    }
}
