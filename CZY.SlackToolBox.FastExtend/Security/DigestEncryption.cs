using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend.Security
{
    public static class DigestEncryption
    {

        #region MD5
        /// <summary>
        /// 将字符串MD5加密
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        public static string ToMD5String(this string text)
        {
            text = text.Trim();
            using (MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider())
            {
                byte[] hashBytes = md5Provider.ComputeHash(Encoding.UTF8.GetBytes(text));
                var builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString().ToUpper();
            }
        }


        /// <summary>
        /// 将字符串MD5加密，为了避免被通用库撞库，在加密信息前后增加字符
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="before">字符串前面增加的字符</param>
        /// <param name="after">字符串后面增加的字符</param>
        /// <returns></returns>
        public static string ToMD5String(this string text, string before, string after)
        {
            text = before + text.Trim() + after;
            using (MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider())
            {
                byte[] hashBytes = md5Provider.ComputeHash(Encoding.UTF8.GetBytes(text));
                var builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString().ToUpper();
            }
        }



        /// <summary>
        /// 转换为MD5加密后的字符串（16位）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5String16(this string str)
        {
            return str.ToMD5String().Substring(8, 16);
        }

        #endregion


        #region SHA1

        /// <summary>
        /// 计算SHA1摘要
        /// 注：默认使用UTF8编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] ToSHA1Bytes(this string str)
        {
            return str.ToSHA1Bytes(Encoding.UTF8);
        }

        /// <summary>
        /// 计算SHA1摘要
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static byte[] ToSHA1Bytes(this string str, Encoding encoding)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] inputBytes = encoding.GetBytes(str);
            byte[] outputBytes = sha1.ComputeHash(inputBytes);

            return outputBytes;
        }

        /// <summary>
        /// 转为SHA1哈希加密字符串
        /// 注：默认使用UTF8编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToSHA1String(this string str)
        {
            return str.ToSHA1String(Encoding.UTF8);
        }

        /// <summary>
        /// 转为SHA1哈希
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ToSHA1String(this string str, Encoding encoding)
        {
            byte[] sha1Bytes = str.ToSHA1Bytes(encoding);
            string resStr = BitConverter.ToString(sha1Bytes);
            return resStr.Replace("-", "").ToLower();
        }
        #endregion
    }
}
