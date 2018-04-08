using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace cPos.Admin.Component
{
    public class XMLGenerator
    {
        private XmlDocument _Doc;
        private XmlElement _RootElement;

        public XMLGenerator()
            : this("data")
        { }

        public XMLGenerator(string rootName)
        {
            _Doc = new XmlDocument();
            //XmlNode node = _Doc.CreateXmlDeclaration("1.0", "gb2312", "no");
            //_Doc.AppendChild(node);
            _RootElement = _Doc.CreateElement(rootName);
            _RootElement.SetAttribute("starttime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
            _Doc.AppendChild(_RootElement);
        }

        public XmlElement AddElement(string name, string value)
        {
            XmlElement element = _Doc.CreateElement(name);
            element.InnerText = value;
            _RootElement.AppendChild(element);
            return element;
        }

        public XmlElement AddElement(XmlElement parent, string name, string value)
        {
            XmlElement element = _Doc.CreateElement(name);
            element.InnerText = value;
            parent.AppendChild(element);
            return element;
        }

        public string ToXML()
        {
            return _Doc.OuterXml;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            XmlSerializer xml_ser = new XmlSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                XmlTextWriter xml_tw = new XmlTextWriter(ms, Encoding.UTF8);
                xml_ser.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(ms))
                {
                    string str = sr.ReadToEnd();
                    xml_tw.Close();
                    ms.Close();
                    return str;
                }
            }  
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="t"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static object Deserialize(Type t, string s)
        {
            using (StringReader rdr = new StringReader(s))
            {
                XmlSerializer xml_ser = new XmlSerializer(t);
                object obj = xml_ser.Deserialize(rdr);
                return obj;
            }
        }

        /// <summary>
        /// 替换XML中的元素的名称
        /// </summary>
        /// <param name="xml">XML</param>
        /// <param name="src">元素的原始名称</param>
        /// <param name="dst">替换后的元素的新名称</param>
        /// <returns></returns>
        public static string ReplaceElementName(string xml, string src, string dst)
        {
            string s1 = xml.Replace(string.Format("<{0}",src),string.Format("<{0}",dst));
            string s2 = s1.Replace(string.Format("</{0}>", src), string.Format("</{0}>", dst));
            return s2;
        }
    }
}
