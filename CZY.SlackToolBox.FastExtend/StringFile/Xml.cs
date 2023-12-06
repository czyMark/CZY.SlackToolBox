using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace CZY.SlackToolBox.FastExtend
{
    public static class Xml
    {
        /// <summary>
        /// 指定到xml文件路径 xml文件没有会自动生成 如果存在就清空
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        public static bool CreateXmlDocument(string XmlPath, string Version = "1.0", string Encoding = "utf-8", string Standalone = "yes")
        {
            try
            {
                XDocument xdoc = new XDocument(new XDeclaration(Version, Encoding, Standalone));
                xdoc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 查询文件夹下所有的xml文件
        /// </summary>
        /// <param name="Path">路径指定到文件夹</param>
        /// <returns>xml文件路径</returns>
        public static List<string> SelectAllXmlPath(string XmlPath)
        {
            string[] fileArray = Directory.GetFiles(XmlPath);
            List<string> filePathList = new List<string>();
            foreach (string item in fileArray)
            {
                if (item.Contains(".Xml") || item.Contains(".xml"))
                {
                    filePathList.Add(item);
                }
            }
            return filePathList;
        }

        /// <summary>
        /// 增加xml节点
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        /// <param name="ElementName">新增加节点名称</param>
        /// <param name="ElementValue">新增加节点值</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool AddXmlNode(string XmlPath, XmlElement XmlNode)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XmlPath);
                XmlElement root = doc.DocumentElement;
                root.AppendChild(XmlNode);
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 增加xml节点
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        /// <param name="ElementName">新增加节点名称</param>
        /// <param name="ElementValue">新增加节点值</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool AddXmlNode(string XmlPath, XElement XmlNode)
        {
         
            try
            {
                XElement doc = XElement.Load(XmlPath);
                doc.AddFirst(XmlNode);
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 在指定xml节点前增加xml节点
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        /// <param name="OldElementName">指定的节点名称</param>
        /// <param name="ElementNode">新指定的节点</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool AddAfterXmlElement(string XmlPath, string OldElementName, XElement ElementNode)
        {
            try
            {
                XElement doc = XElement.Load(XmlPath);
                XElement xele = doc.Element(OldElementName);
                xele.AddAfterSelf(ElementNode);
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 在指定xml节点后加xml节点
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        /// <param name="OldElementName">指定的节点名称</param>
        /// <param name="ElementNode">新指定的节点</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool AddBeforeXmlElement(string XmlPath, string OldElementName, XElement ElementNode)
        {
           
            try
            {
                XElement doc = XElement.Load(XmlPath);
                XElement xele = doc.Element(OldElementName);
                xele.AddBeforeSelf(ElementNode);
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 移除所有指定的xml文档属性
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        /// <param name="AttributeName"></param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool RemoveXmlAttribute(string XmlPath, string AttributeName)
        {
            try
            {
                XElement doc = XElement.Load(XmlPath);
                XAttribute attr = doc.Attribute(AttributeName);
                attr.Remove();
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 移除所有指定的xml文档节点
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        /// <param name="NodeName">指定节点名称</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool RemoveXmlNode(string XmlPath, string NodeName)
        {
            try
            {
                XElement doc = XElement.Load(XmlPath);
                (from item in doc.Elements(NodeName)
                 select item).Remove();
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 移除所有指定的xml文档节点
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        /// <param name="NodeName">指定节点名称</param>
        /// <param name="NodeValue">指定节点值</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool RemoveXmlNode(string XmlPath, string NodeName, XElement NodeValue)
        {
            try
            {
                XElement doc = XElement.Load(XmlPath);
                (from item in doc.Elements(NodeName)
                 where item.Nodes() == NodeValue
                 select item).Remove();
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 根据节点是属性值删除xml节点
        /// </summary>
        /// <param name="XmlPath">xml路径</param>
        /// <param name="NodeName">指定节点名称</param>
        /// <param name="NodeAttrbute">指定节点属性名称</param>
        /// <param name="NodeAttrbuteValue">指定节点属性值</param>
        public static bool RemoveXmlNode(string XmlPath, string NodeName, string NodeAttrbute, string NodeAttrbuteValue)
        {
            try
            {
                XElement doc = XElement.Load(XmlPath);
                (from item in doc.Elements(NodeName)
                 where item.Attribute(NodeAttrbute).Value == NodeAttrbuteValue
                 select item).Remove();
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 修改指定xml文档节点
        /// </summary>
        /// <param name="XmlPath">指定到xml文件路径</param>
        /// <param name="OldElementName">指定旧的xml文档节点</param>
        /// <param name="NewElementName">指定新的xml文档节点</param>
        /// <param name="ElementName">指定新的节点值</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool UpdateXmlElement(string XmlPath, string OldElementName, string NewElementName, string Elementvalue)
        {
            try
            {
                XElement doc = XElement.Load(XmlPath);
                doc.Element(OldElementName).ReplaceWith(new XElement(NewElementName, Elementvalue));
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 修改指定xml文档节点
        /// </summary>
        /// <param name="NodeName">指定到xml文件路径</param>
        /// <param name="NodeAttrbute">指定节点属性名称</param>
        /// <param name="NodeAttrbuteValue">指定节点是属性值</param>
        /// <param name="Node">指定新的节点值</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool UpdateXmlElementWAttrbute(string XmlPath, string NodeName, string NodeAttrbute, string NodeAttrbuteValue, XElement Node)
        {
            try
            {
                XElement doc = XElement.Load(XmlPath);
                (from item in doc.Elements(NodeName)
                 where item.Attribute(NodeAttrbute).Value == NodeAttrbuteValue
                 select item).FirstOrDefault().ReplaceWith(Node);
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 修改指定xml文档节点
        /// </summary>
        /// <param name="NodeName">指定到xml文件路径</param>
        /// <param name="NodeAttrbute">指定节点名称</param>
        /// <param name="NodeAttrbuteValue">指定节点值</param>
        /// <param name="Node">指定新的节点值</param>
        /// <returns>true:成功 | false:失败</returns>
        public static bool UpdateXmlElement(string XmlPath, string NodeName, string NodeXElement, object NodeXElementValue, XElement Node)
        {
            try
            {
                XElement doc = XElement.Load(XmlPath);
               var temp =(from item in doc.Elements(NodeName)
                 where item.Element(NodeXElement).Value== NodeXElementValue.ToString()
                 select item).FirstOrDefault();
               temp.ReplaceWith(Node);
                doc.Save(XmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
