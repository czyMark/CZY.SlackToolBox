﻿using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CZY.SlackToolBox.FastExtend.Security
{
    public static class DissymmetryEncryption
    {

		#region 私钥加密，公钥解密

		private static byte[] Transform(AsymmetricKeyParameter key, byte[] contentData, string algorithm, bool forEncryption)
		{
			var c = CipherUtilities.GetCipher(algorithm);
			c.Init(forEncryption, new ParametersWithRandom(key));
			return c.DoFinal(contentData);
		}
		/// <summary>
		/// RSA私钥加密
		/// </summary>
		/// <param name="privateKey">Java格式的RSA私钥 base64格式</param>
		/// <param name="contentData">待加密的数据；调用方法Encoding.GetEncoding("UTF-8").GetBytes(contentData)</param>
		/// <param name="algorithm">加密算法</param>
		/// <returns>RSA私钥加密之后的密文 Base64格式</returns>
		public static string EncryptByPrivateKey(this string value, string privateKey, Encoding encoding = null, string algorithm = "RSA/ECB/PKCS1Padding")
		{
			return Convert.ToBase64String(value.EncryptByPrivateKey(Convert.FromBase64String(privateKey), encoding, algorithm));
		}


		/// <summary>
		/// RSA私钥加密
		/// </summary>
		/// <param name="privateKey">Java格式的RSA私钥</param>
		/// <param name="contentData">待加密的数据；调用方法Encoding.GetEncoding("UTF-8").GetBytes(contentData)</param>
		/// <param name="algorithm">加密算法</param>
		/// <returns>RSA私钥加密之后的密文</returns>
		public static byte[] EncryptByPrivateKey(this string value, byte[] privateKey, Encoding encoding = null, string algorithm = "RSA/ECB/PKCS1Padding")
		{
			if (encoding == null)
				encoding = Encoding.Default;
			byte[] contentData = encoding.GetBytes(value);
			RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(privateKey);
			return Transform(privateKeyParam, contentData, algorithm, true);
		}

		/// <summary>
		/// RSA公钥解密
		/// </summary>
		/// <param name="value">待解密数据 base64格式</param>
		/// <param name="publicKey">Java格式的RSA公钥  base64格式</param>
		/// <param name="encoding">解密出来的数据编码格式，默认UTF-8</param>
		/// <param name="algorithm">加密算法</param>
		/// <returns>RSA私钥解密之后的明文</returns>
		public static string DecryptByPublicKey(this string value, string publicKey, Encoding encoding = null, string algorithm = "RSA/ECB/PKCS1Padding")
		{
			if (encoding == null)
				encoding = Encoding.Default;
			return encoding.GetString(value.DecryptByPublicKey(Convert.FromBase64String(publicKey), algorithm));
		}

		/// <summary>
		/// RSA公钥解密
		/// </summary>
		/// <param name="value">待解密数据</param>
		/// <param name="publicKey">Java格式的RSA公钥</param>
		/// <param name="algorithm">加密算法</param>
		/// <returns>RSA私钥解密之后的明文</returns>
		public static byte[] DecryptByPublicKey(this string value, byte[] publicKey, string algorithm = "RSA/ECB/PKCS1Padding")
		{
			byte[] contentData = Convert.FromBase64String(value);
			RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKey);
			return Transform(publicKeyParam, contentData, algorithm, false);
		}
		#endregion

		#region 公钥加密，私钥解密

		/// <summary>
		/// RSA公钥加密
		/// </summary>
		/// <param name="value">需要加密的明文字符串</param>
		/// <param name="xmlPublicKey">加密公钥；为空则默认系统公钥</param>
		/// <param name="encoding">编码格式；默认：UTF-8</param>
		/// <returns>RSA公钥加密的密文</returns>
		public static string EncryptByRSAPublic(this string value, string xmlPublicKey = "AAAAAAAAAA", string encoding = "UTF-8")
		{
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
			{
				byte[] cipherbytes;
				rsa.FromXmlString(xmlPublicKey);
				cipherbytes = rsa.Encrypt(Encoding.GetEncoding(encoding).GetBytes(value), false);
				return Convert.ToBase64String(cipherbytes);
			}
		}

		/// <summary>
		/// RSA私钥解密
		/// </summary>
		/// <param name="value">需要加密的明文字符串</param>
		/// <param name="xmlPrivateKey">解密私钥；为空则默认系统公钥</param>
		/// <param name="encoding">编码格式；默认：UTF-8</param>
		/// <returns>RSA私钥解密的明文</returns>
		public static string DecryptByRSAPrivate(this string value, string xmlPrivateKey = "AAAAAAAAAA", string encoding = "UTF-8")
		{
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
			{
				byte[] cipherbytes;
				rsa.FromXmlString(xmlPrivateKey);
				cipherbytes = rsa.Decrypt(Convert.FromBase64String(value), false);
				return Encoding.GetEncoding(encoding).GetString(cipherbytes);
			}
		}
		#endregion

		#region RSA加密解密
		private static CspParameters Param;
		/// <summary>
		/// 进行 RSA 加密
		/// </summary>
		/// <param name="value">源字符串</param>
		/// <returns>加密后字符串</returns>
		public static string EncryptByRsa(this string value)
		{
			Param = new CspParameters();
			//密匙容器的名称，保持加密解密一致才能解密成功
			Param.KeyContainerName = "Navis";
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(Param))
			{
				//将要加密的字符串转换成字节数组
				byte[] plaindata = Encoding.Default.GetBytes(value);
				//通过字节数组进行加密
				byte[] encryptdata = rsa.Encrypt(plaindata, false);
				//将加密后的字节数组转换成字符串
				return Convert.ToBase64String(encryptdata);
			}
		}

		/// <summary>
		/// 通过RSA 加密方式进行解密
		/// </summary>
		/// <param name="value">加密字符串</param>
		/// <returns>解密后字符串</returns>
		public static string DecryptByRsa(this string value)
		{
			Param = new CspParameters();
			Param.KeyContainerName = "Navis";
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(Param))
			{
				byte[] encryptdata = Convert.FromBase64String(value);
				byte[] decryptdata = rsa.Decrypt(encryptdata, false);
				return Encoding.Default.GetString(decryptdata);
			}
		}
		#endregion

		#region RSA分段加密：待加密的字符串拆开，每段长度都小于等于限制长度，然后分段加密

		/// <summary>
		/// RSA分段加密
		/// </summary>
		/// <param name="value">需要进行RSA加密的长字符串</param>
		/// <param name="xmlPublicKey">RSA C#公钥</param>
		/// <returns>返回RSA加密后的密文</returns>
		public static String EncryptBySubRSA(this string value, string xmlPublicKey)
		{
			RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
			provider.FromXmlString(xmlPublicKey);
			Byte[] bytes = Encoding.Default.GetBytes(value);
			int MaxBlockSize = provider.KeySize / 8 - 11;  //加密块最大长度限制

			if (bytes.Length <= MaxBlockSize)
				return Convert.ToBase64String(provider.Encrypt(bytes, false));

			using (MemoryStream PlaiStream = new MemoryStream(bytes))
			using (MemoryStream CrypStream = new MemoryStream())
			{
				Byte[] Buffer = new Byte[MaxBlockSize];
				int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

				while (BlockSize > 0)
				{
					Byte[] ToEncrypt = new Byte[BlockSize];
					Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

					Byte[] Cryptograph = provider.Encrypt(ToEncrypt, false);
					CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

					BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
				}

				return Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);
			}

		}

		/// <summary>
		/// RSA分段解密，应对长字符串
		/// </summary>
		/// <param name="xmlPrivateKey">RSA C#私钥</param>
		/// <param name="value">需要解密的长字符串</param>
		/// <returns>返回RSA分段解密的明文</returns>
		public static String DecryptBySubRSA(this string value, string xmlPrivateKey)
		{
			RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
			provider.FromXmlString(xmlPrivateKey);
			Byte[] bytes = Convert.FromBase64String(value);
			int MaxBlockSize = provider.KeySize / 8;  //解密块最大长度限制

			if (bytes.Length <= MaxBlockSize)
				return Encoding.Default.GetString(provider.Decrypt(bytes, false));

			using (MemoryStream CrypStream = new MemoryStream(bytes))
			using (MemoryStream PlaiStream = new MemoryStream())
			{
				Byte[] Buffer = new Byte[MaxBlockSize];
				int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

				while (BlockSize > 0)
				{
					Byte[] ToDecrypt = new Byte[BlockSize];
					Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

					Byte[] Plaintext = provider.Decrypt(ToDecrypt, false);
					PlaiStream.Write(Plaintext, 0, Plaintext.Length);

					BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
				}

				return Encoding.Default.GetString(PlaiStream.ToArray());
			}
		}
		#endregion
	}
}
