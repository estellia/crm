using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace JIT.Utility.ETCL.Base
{
    public class XMLHandler
    {
        /// <summary>
        /// 根据属性查找节点
        /// </summary>
        /// <param name="pFilePath">XML文件所在的路径</param>
        /// <param name="pNodePath">待查找的父级节点</param>
        /// <param name="pPropertyName">属性名称</param>
        /// <param name="pPropertyValue">期望的属性值</param>
        /// <returns>符合条件的节点</returns>
        public static List<XmlNode> GetNodesByProperty(XmlDocument pXmlDoc,  string pNodePath, string pPropertyName, string pPropertyValue)
        {
            XmlNodeList nodeList = pXmlDoc.SelectNodes(pNodePath);
            List<XmlNode> lstNodes = new List<XmlNode>();
            foreach (XmlNode item in nodeList)
            {
                if (item.Attributes[pPropertyName] != null && item.Attributes[pPropertyName].Value == pPropertyValue)
                {
                    lstNodes.Add(item);
                }
            }
            return lstNodes;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="pPath">路径</param>
        /// <param name="pNode">节点</param>
        /// <param name="pAttribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XMLManager.Read(path, "/Node", "")
         * XMLManager.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string pPath, string pNode, string pAttribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pPath); 
                XmlNode xn = doc.SelectSingleNode(pNode);
                value = (pAttribute.Equals("") ? xn.InnerText : xn.Attributes[pAttribute].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// 读取XML中指定路径的节点
        /// </summary>
        /// <param name="pPath">XML文件路径</param>
        /// <param name="pNodeFullPath">节点路径</param>
        /// <returns>XML节点列表</returns>
        public static XmlNodeList Read(string pPath,string pNodeFullPath)
        {
            XmlNodeList xmlNodeList;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pPath );


                xmlNodeList = doc.SelectNodes(pNodeFullPath); 
            }
            catch (Exception ex)
            {
                xmlNodeList = null;
                throw ex;
            }
            return xmlNodeList;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="pPath">路径</param>
        /// <param name="pNode">节点</param>
        /// <param name="pElement">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="pAttribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="pValue">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XMLManager.Insert(path, "/Node", "Element", "", "Value")
         * XMLManager.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XMLManager.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static bool Insert(string pPath, string pNode, string pElement, string pAttribute, string pValue)
        {
            bool opResult;
            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(pPath);
                XmlNode xn = doc.SelectSingleNode(pNode);
                if (pElement.Equals(""))
                {
                    if (!pAttribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(pAttribute, pValue);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(pElement);
                    if (pAttribute.Equals(""))
                        xe.InnerText = pValue;
                    else
                        xe.SetAttribute(pAttribute, pValue);
                    xn.AppendChild(xe);
                }
                doc.Save(pPath);
                opResult = true;
            }
            catch (Exception ex)
            {
                opResult = false;
                throw ex;
            }
            return opResult;
        }

        /// <summary>
        /// 批量添加子节点
        /// </summary>
        /// <param name="pXmlPath">XML文件路径</param>
        /// <param name="pNode">节点路径</param>
        /// <param name="pElement">新添加的节点名称</param>
        /// <param name="pAttribute">属性名称</param>
        /// <param name="pValue">属性值</param>
        /// <returns>TRUE：添加成功，FALSE：添加失败。</returns>
        public static bool Insert(string pXmlPath, string pNode, string pElement, string[] pAttribute, string[] pValue)
        {
            bool opResult;
            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(pXmlPath);
                XmlNode xn = doc.SelectSingleNode(pNode);
                XmlElement xe = doc.CreateElement(pElement);
                if (pAttribute.Length > 0)
                {
                    for (int i = 0; i < pAttribute.Length; i++)
                    {
                        xe.SetAttribute(pAttribute[i], pValue[i]);
                    }
                }
                xn.AppendChild(xe);
                doc.Save(pXmlPath);
                opResult = true;
            }
            catch (Exception ex)
            {
                opResult = false;
                throw ex;
            }
            return opResult;
        }
         

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="pPath">路径</param>
        /// <param name="pNode">节点</param>
        /// <param name="pAttribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="pValue">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XMLManager.Insert(path, "/Node", "", "Value")
         * XMLManager.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static bool Update(string pPath, string pNode, string pAttribute, string pValue)
        {
            bool opResult ;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pPath);
                XmlNode xn = doc.SelectSingleNode(pNode);
                XmlElement xe = (XmlElement)xn;
                if (pAttribute.Equals(""))
                    xe.InnerText = pValue;
                else
                    xe.SetAttribute(pAttribute, pValue);
                doc.Save(pPath);
                opResult = true;
            }
            catch (Exception ex)
            {
                opResult = false;
                throw ex;
            }
            return opResult;
        }

        /// <summary>
        /// 更新XML文档中的指定属性的值
        /// </summary>
        /// <param name="pPath">XML文件路径</param>
        /// <param name="pNode">节点路径</param>
        /// <param name="pKeyAttrName">节点的名称</param>
        /// <param name="pKeyAttrValue">节点的值</param>
        /// <param name="pAttributeName">属性的名称</param>
        /// <param name="pAttributeValue">属性的值</param>
        /// <returns>TRUE：更新成功，FALSE：更新失败。</returns>
        public static bool UpdateAttribute(string pPath, string pNode,string pKeyAttrName,string pKeyAttrValue, string[] pAttributeName, string[] pAttributeValue)
        {
            bool opResult;
            try
            {
                bool docChanged = false;
                XmlDocument doc = new XmlDocument();
                doc.Load(pPath);
                XmlNodeList nodeListUser = doc.SelectNodes(pNode);
                foreach (var item in nodeListUser)
                {
                    //根据主键逐个匹配
                    XmlNode nodeItem = (XmlNode)item;
                    if (nodeItem.Attributes[pKeyAttrName].Value == pKeyAttrValue)
                    {  //根据主键属性已找到相应节点
                        //逐个属性修改
                        for (int i = 0; i < pAttributeName.Length; i++)
                        {
                            string attrName = pAttributeName[i];
                            string attrValue = pAttributeValue[i];
                            if (attrValue == null)//如果传入的为null,则认为不对属性值进行修改.
                                continue;
                            if (nodeItem.Attributes[attrName].Value != attrValue)//如果信息一样，则不用修改
                            {
                                docChanged = true;
                                nodeItem.Attributes[attrName].Value = attrValue;
                            }
                        }
                    }
                }
                if (docChanged)
                    doc.Save(pPath);
                opResult = true;
            }
            catch (Exception ex)
            {
                opResult = false;
                throw ex;
            }
            return opResult;
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="pPath">路径</param>
        /// <param name="pNode">节点</param>
        /// <param name="pAttribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XMLManager.Delete(path, "/Node", "")
         * XMLManager.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static bool Delete(string pPath, string pNode, string pAttribute)
        {
            bool opResult;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pPath);
                XmlNode xn = doc.SelectSingleNode(pNode);
                XmlElement xe = (XmlElement)xn;
                if (pAttribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(pAttribute);
                doc.Save(pPath);
                opResult = true;
            }
            catch (Exception ex)
            {
                opResult = false;
                throw ex;
            }
            return opResult;
        }

        /// <summary>
        /// 根据属性值删除节点
        /// </summary>
        /// <param name="pPath">XML文件路径</param>
        /// <param name="pNode">节点路径</param>
        /// <param name="pAttributeName">属性名称数组</param>
        /// <param name="pAttributeValue">属性值数据</param>
        /// <returns>TRUE：删除成功，FALSE：删除失败。</returns>
        public static bool DeleteByAttribute(string pPath, string pNode, string[] pAttributeName, string[] pAttributeValue)
        {
            bool  opResult;
            try
            {
                bool isNodeDeleted = false;
                XmlDocument doc = new XmlDocument();
                doc.Load(pPath);
                XmlNodeList nodeListUser = doc.SelectNodes(pNode);
                foreach (var item in nodeListUser)
                {
                    //根据属性逐个匹配
                    XmlNode nodeItem = (XmlNode)item;
                    bool matched = true;
                    for (int i = 0; i < pAttributeName.Length; i++)
                    {
                        string attrName = pAttributeName[i];
                        string attrValue = pAttributeValue[i];
                        
                        if (nodeItem.Attributes[attrName].Value != attrValue)
                        {
                            matched = false;
                        }
                    }
                    //如果匹配，则删除
                    if (matched)
                    {
                        isNodeDeleted = true;
                        nodeItem.ParentNode.RemoveChild(nodeItem);
                    }
                }
                if (isNodeDeleted)
                    doc.Save(pPath);
                opResult = true;
            }
            catch (Exception ex)
            {
                opResult = false;
                throw ex;
            }
            return opResult;
        }
    }
}
