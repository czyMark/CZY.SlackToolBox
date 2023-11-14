using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend.Security
{
    public static class SymmetricEncryption
    {
        #region DES
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string ToDESEncrypt(string encryptString, string encryptKey, string iv)
        {
            try
            {
                //将字符转换为UTF - 8编码的字节序列
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                //用指定的密钥和初始化向量创建CBC模式的DES加密标准
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                dCSP.Mode = CipherMode.CBC;
                dCSP.Padding = PaddingMode.PKCS7;
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);//写入内存流
                cStream.FlushFinalBlock();//将缓冲区中的数据写入内存流，并清除缓冲区
                return Convert.ToBase64String(mStream.ToArray()); //将内存流转写入字节数组并转换为string字符
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string ToDESDecrypt(string decryptString, string decryptKey, string iv)
        {
            try
            {
                //将字符转换为UTF - 8编码的字节序列
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                //用指定的密钥和初始化向量使用CBC模式的DES解密标准解密
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                dCSP.Mode = CipherMode.CBC;
                dCSP.Padding = PaddingMode.PKCS7;
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }


        } 
        #endregion
         
        #region AES
        //AES加密  传入,要加密的串和, 解密key
        public static string ToAESEncrypt(string input, string key = "coreccsxtechewtgnnrtrttgkljhopid", string iv = "1034567812315678")
        {
            var rgbKey = Encoding.UTF8.GetBytes(key);
            var rgbIV = Encoding.UTF8.GetBytes(iv); //偏移量,最小为16
            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(rgbKey, rgbIV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor,
                            CryptoStreamMode.Write))

                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        var decryptedContent = msEncrypt.ToArray();

                        return Convert.ToBase64String(decryptedContent);
                    }
                }
            }
        }
        public static string ToAESDecrypt(string cipherText, string key = "coreccsxtechewtgnnrtrttgkljhopid", string iv = "1034567812315678")
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var rgbKey = Encoding.UTF8.GetBytes(key);
            var rgbIV = Encoding.UTF8.GetBytes(iv); //偏移量,最小为16

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(rgbKey, rgbIV))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(fullCipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }


        #region AES加密

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始向量</param>
        /// <param name="padding">填充模式</param>
        /// <param name="mode">加密模式</param>
        /// <returns></returns>
        public static string ToAES128Encrypt(this string source, string key = "Xa5eN9crY8S6e7S2", string iv = "P2t3l5OC6k7K8e9Y", PaddingMode padding = PaddingMode.PKCS7, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] textBytes = Encoding.UTF8.GetBytes(source);
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

                byte[] useKeyBytes = new byte[16];
                byte[] useIvBytes = new byte[16];

                if (keyBytes.Length > useKeyBytes.Length)
                    Array.Copy(keyBytes, useKeyBytes, useKeyBytes.Length);
                else
                    Array.Copy(keyBytes, useKeyBytes, keyBytes.Length);

                if (ivBytes.Length > useIvBytes.Length)
                    Array.Copy(ivBytes, useIvBytes, useIvBytes.Length);
                else
                    Array.Copy(ivBytes, useIvBytes, ivBytes.Length);

                Aes aes = System.Security.Cryptography.Aes.Create();
                aes.KeySize = 256;//秘钥的大小，以位为单位,128,256等
                aes.BlockSize = 128;//支持的块大小
                aes.Padding = padding;//填充模式
                aes.Mode = mode;
                aes.Key = useKeyBytes;
                aes.IV = useIvBytes;//初始化向量，如果没有设置默认的16个0

                ICryptoTransform cryptoTransform = aes.CreateEncryptor();
                byte[] resultBytes = cryptoTransform.TransformFinalBlock(textBytes, 0, textBytes.Length);

                return resultBytes.To0XString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region AES解密

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始向量</param>
        /// <param name="padding">填充模式</param>
        /// <param name="mode">加密模式</param>
        /// <returns></returns>
        public static string ToAES128Decrypt(this string source, string key = "Xa5eN9crY8S6e7S2", string iv = "P2t3l5OC6k7K8e9Y", PaddingMode padding = PaddingMode.PKCS7, CipherMode mode = CipherMode.CBC)
        {
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] textBytes = source.ToHexBytes();
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

                byte[] useKeyBytes = new byte[16];
                byte[] useIvBytes = new byte[16];

                if (keyBytes.Length > useKeyBytes.Length)
                    Array.Copy(keyBytes, useKeyBytes, useKeyBytes.Length);
                else
                    Array.Copy(keyBytes, useKeyBytes, keyBytes.Length);

                if (ivBytes.Length > useIvBytes.Length)
                    Array.Copy(ivBytes, useIvBytes, useIvBytes.Length);
                else
                    Array.Copy(ivBytes, useIvBytes, ivBytes.Length);

                Aes aes = System.Security.Cryptography.Aes.Create();
                aes.KeySize = 256;//秘钥的大小，以位为单位,128,256等
                aes.BlockSize = 128;//支持的块大小
                aes.Padding = padding;//填充模式
                aes.Mode = mode;
                aes.Key = useKeyBytes;
                aes.IV = useIvBytes;//初始化向量，如果没有设置默认的16个0

                ICryptoTransform decryptoTransform = aes.CreateDecryptor();
                byte[] resultBytes = decryptoTransform.TransformFinalBlock(textBytes, 0, textBytes.Length);

                return Encoding.UTF8.GetString(resultBytes);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
        #endregion
         
        #region Base64

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <param name="encoding">加密采用的编码方式</param>
        /// <returns></returns>
        public static string ToBase64Encode(this string source, Encoding encoding)
        {
            string encode = string.Empty;
            byte[] bytes = encoding.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        /// <summary>
        /// Base64加密
        /// 注:默认采用UTF8编码
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string ToBase64Encode(this string source)
        {
            return ToBase64Encode(source, Encoding.UTF8);
        }


        /// <summary>
        /// Base64解密
        /// 注:默认使用UTF8编码
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string ToBase64Decode(this string result)
        {
            return ToBase64Decode(result, Encoding.UTF8);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <param name="encoding">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <returns>解密后的字符串</returns>
        public static string ToBase64Decode(this string result, Encoding encoding)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encoding.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
        #endregion
    }
}
