using System;
using System.Collections.Generic;
using System.Linq;

namespace CZY.SlackToolBox.FastExtend
{


    public static class RandomTool
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

        #region 随机获取集合中的值

        /// <summary>
        /// 获取集合中的随机值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="source">值的集合</param>
        /// <returns></returns>
        public static T GetRandomValue<T>(this IEnumerable<T> source)
        {
            return source.ToList()[GetSimpNum(0, source.Count())];
        }

        /// <summary>
        /// 获取不重复的随机值的集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public static List<T> GetRandomValue<T>(this IEnumerable<T> source, int count)
        {
            if (source.Count() < count)
                throw new Exception("选择数量不能大于集合数量!");

            var _source = new List<T>(source).Distinct().ToList();
            List<T> resList = new List<T>();
            for (int i = 0; i < count; i++)
            {
                resList.Add(GetNewValue());
            }
            return resList;
            T GetNewValue()
            {
                while (true)
                {
                    var theValue = GetRandomValue(_source);
                    //todo:需要验证值是否一样
                    if (!resList.Any(x => x.GetHashCode() == theValue.GetHashCode()))
                    {
                        _source.Remove(theValue);

                        return theValue;
                    }
                }
            }
        }

        #endregion


        public static double NextDouble()=> random.NextDouble();
        public static int Next()=> random.Next();
        public static int Next(int max)=> random.Next(max);
        public static int Next(int min,int max)=> random.Next(min,max);



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
        public static int GetSimpNum(int min, int max)
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
        public static string GetNum(int count, int decimalCount, RandomNumFormat randomNumFormat = RandomNumFormat.Point)
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

        /// 使用默认的字符集合生成 生成指定位数的随机字符数字和字母
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
             
            //采用一个简单的算法以保证生成随机数的不同
            for (int i = 1; i < VcodeNum + 1; i++)
            { 
                int t = random.Next(VcArray.Length);//获取随机数
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

        /// <summary>
        /// 随机返回中文字符
        /// </summary>
        /// <returns></returns>
        public static char GetChinese()
        {
            // Unicode编码范围为0x4E00到0x9FA5之间（包括这两个值）
            int startRange = 0x4e00;
            int endRange = 0x9fa5;

            int unicodeValue = GetSimpNum(startRange, endRange + 1);

            return Convert.ToChar(unicodeValue);
        }

        /// <summary>
        /// 随机返回指定数量的中文
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetChinese(int count, bool repeat = false)
        {
            // Unicode编码范围为0x4E00到0x9FA5之间（包括这两个值）
            int startRange = 0x4e00;
            int endRange = 0x9fa5;

            string str = string.Empty;
            if (repeat)
            {
                for (int i = 0; i < count; i++)
                {
                    int unicodeValue = GetSimpNum(startRange, endRange + 1);
                    str += Convert.ToChar(unicodeValue);
                }
            }
            else
            { 
                for (int i = 0; i < count; i++)
                {
                    int unicodeValue = GetSimpNum(startRange, endRange + 1);
                    char c= Convert.ToChar(unicodeValue);
                    if (str.Contains(c))
                    {
                        count--;
                    } else
                    {
                        str += Convert.ToChar(unicodeValue);
                    } 
                }
            }
            return str;
        }
    }
}
