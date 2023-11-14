using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace  CZY.SlackToolBox.FastExtend
{
    /// <summary>
    /// 深拷贝
    /// </summary>
    public static class DeepCopyTool
	{
		/// <summary>
		/// 二进制序列化拷贝
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static T DeepCopyByBinary<T>(this T obj)
		{
			object retval;
			using (MemoryStream ms = new MemoryStream())
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(ms, obj);
				ms.Seek(0, SeekOrigin.Begin);
				retval = bf.Deserialize(ms);
				ms.Close();
			}
			return (T)retval;
		}

		/// <summary>
		/// 使用XML方式拷贝
		/// </summary> 
		public static T DeepCopyByXML<T>(this T obj)
		{
			object retval;
			using (MemoryStream ms = new MemoryStream())
			{
				XmlSerializer xml = new XmlSerializer(typeof(T));
				xml.Serialize(ms, obj);
				ms.Seek(0, SeekOrigin.Begin);
				retval = xml.Deserialize(ms);
				ms.Close();
			}
			return (T)retval;
		}

		/// <summary>
		/// 使用内存的方式拷贝
		/// </summary> 
		public static T DeepCopyByMemory<T>(this T obj)
		{
			object retval;
			using (MemoryStream ms = new MemoryStream())
			{
				DataContractSerializer ser = new DataContractSerializer(typeof(T));
				ser.WriteObject(ms, obj);
				ms.Seek(0, SeekOrigin.Begin);
				retval = ser.ReadObject(ms);
				ms.Close();
			}
			return (T)retval;
		}
	}
}
