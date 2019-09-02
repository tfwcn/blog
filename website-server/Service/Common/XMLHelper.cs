using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common
{
    public class XmlHelper
    {
        /// <summary>
        /// 修改XML 2017-6-29 韩永健
        /// </summary>
        /// <param name="path">文件名路径，包括文件名</param>
        /// <param name="tagName">标签名</param>
        /// <param name="value">值</param>
        public static void WriteXML(string path, string tagName, object value)
        {
            CheckFile(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;
            XmlNodeList personNodes = GetElementsByTagName(doc, root, "SysConfig", null);
            foreach (XmlNode node in personNodes)
            {
                XmlElement ele = (XmlElement)node;
                XmlElement nameEle0 = (XmlElement)GetElementsByTagName(doc, ele, tagName, value)[0];
                nameEle0.InnerText = value.ToString();
            }
            doc.Save(path);
        }
        /// <summary>
        /// 文件如果不存在，则创建文件
        /// </summary>
        /// <param name="path"></param>
        public static void CheckFile(string path)
        {
            if (!File.Exists(path))
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode xmlNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                xmlDoc.AppendChild(xmlNode);
                XmlNode rootNode = xmlDoc.CreateElement("SysConfig");
                xmlDoc.AppendChild(rootNode);
                xmlDoc.Save(path);
            }
        }
        /// <summary>
        /// 读取XML 2017-6-29 韩永健
        /// </summary>
        /// <param name="path">文件名路径，包括文件名</param>
        /// <param name="tagName">标签名</param>
        /// <param name="value">值</param>
        public static string ReadXML(string path, string tagName, object value)
        {
            CheckFile(path);
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;
            XmlNodeList personNodes = GetElementsByTagName(doc, root, "SysConfig", null);
            foreach (XmlNode node in personNodes)
            {
                XmlElement ele = (XmlElement)node;
                XmlElement nameEle0 = (XmlElement)GetElementsByTagName(doc, ele, tagName, value)[0];
                return nameEle0.InnerText;
            }
            return "";
        }
        /// <summary>
        /// 获取或创建标签 2017-6-29 韩永健
        /// </summary>
        /// <param name="tagName">标签名</param>
        public static XmlNodeList GetElementsByTagName(XmlDocument doc, XmlElement root, string tagName, object value)
        {
            XmlNodeList personNodes = root.GetElementsByTagName(tagName);
            if (personNodes.Count > 0)
            {
                return personNodes;
            }
            else
            {
                XmlNode rtspNode = doc.CreateNode(XmlNodeType.Element, tagName, null);
                if (value != null)
                {
                    rtspNode.InnerText = value.ToString();
                }
                root.AppendChild(rtspNode);
                return root.GetElementsByTagName(tagName);
            }
        }
    }
}
