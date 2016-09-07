using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DRP.Framework
{
    public static class XmlHelper
    {
        /// <summary>
        /// XML文档节点值 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetNodeValue(this XmlNode node, string xpath)
        {
            if (node == null) return "";
            var n = node.SelectSingleNode(xpath);
            return n == null ? "" : n.InnerText;
        }
        
        /// <summary>
        /// XML文档节点属性
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string GetNodeAttr(this XmlNode node, string attrName)
        {
            return node.Attributes[attrName].InnerText;
        }
    }
}
