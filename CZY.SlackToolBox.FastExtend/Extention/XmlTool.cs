using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class XmlTool
    {
        //XML文件存储的字符集
        //设置编码,不能用Encoding.UTF8,会导致带有BOM标记
        public static Encoding Encoding = new UTF8Encoding(false);
        //设置自动缩进
        //xmlWriterSettings.OmitXmlDeclaration = true;//删除XmlDeclaration：<?xml version="1.0" encoding="utf-16"?>
        //xmlWriterSettings.NewLineChars = "\r\n";
        //xmlWriterSettings.NewLineHandling = NewLineHandling.None;
        public static bool Indent = true;

        //去掉 attribute 里面的 xmlns:xsi 和 xmlns:xsd
        public static bool removeAttributeXmlns = true;

        /// <summary>
        /// 获取XML文档的根字符
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string GetRoot(this string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.Replace("\r\n", "").Replace("\0", "").Trim());
            var e = doc.DocumentElement;
            return e.InnerText;
        }


        /// <summary>
        /// 读取XML配置文件,如果文件不存在；新建文件赋予默认值后在返回实体
        /// </summary>
        /// <typeparam name="T">读取出的对应的实体</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>读取出对应的实体 如果没有找到或转换出错返回null</returns>
        public static T LoadXmlData<T>(this string path)
        {
            try
            {
                //读取文件
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding))
                    {
                        var json = sr.ReadToEnd().ToString();
                        var newT = json.DeserializeXML<T>();
                        return newT;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("加载文件错误：" + e.Message);
                return (T)new object();
            }
        }

        /// <summary>
        /// 将XML存储本地
        /// </summary>
        /// <typeparam name="T">读取出对应的实体</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="info">要序列化保存的信息</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static bool SaveXmlFile(this string path, object info)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))//验证文件路径是否存在
                {
                    //验证文件路径是否存在，不存在就创建
                    Path.GetDirectoryName(path).CreateDirectory();
                }
                System.IO.File.WriteAllText(path, info.SerializeXML(), Encoding);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("保存文件错误：" + e.Message);
                return false;
            }
        }
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static T DeserializeXML<T>(this string xml)
        {
            return (T)DeserializeXML(typeof(T), xml);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="stream">字节流</param>
        /// <returns></returns>
        public static T DeserializeXML<T>(this Stream stream)
        {
            return (T)Deserialize(typeof(T), stream);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        private static object DeserializeXML(System.Type type, string xml)
        {
            try
            {
                xml = xml.Replace("\r\n", "").Replace("\0", "").Trim();
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static object Deserialize(System.Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion
        
        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string SerializeXML<T>(this T obj)
        {
            return SerializeXML(typeof(T), obj);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        private static string SerializeXML(this System.Type type, object obj)
        {
            using (MemoryStream Stream = new MemoryStream())
            {

                XmlSerializerNamespaces _name = new XmlSerializerNamespaces();
                if (removeAttributeXmlns)
                    _name.Add("", "");//这样就 去掉 attribute 里面的 xmlns:xsi 和 xmlns:xsd
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Encoding = Encoding;
                xmlWriterSettings.Indent = Indent;
                XmlSerializer xml = new XmlSerializer(type);
                try
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(Stream, xmlWriterSettings))
                    {
                        //序列化对象
                        xml.Serialize(xmlWriter, obj, _name);
                    }
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
                return Encoding.UTF8.GetString(Stream.ToArray()).Trim();
            }
        }
        #endregion
    }
}
