using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

namespace HuYiMessageService
{
    public static class Util
    {
        public static Dictionary<string, string> GetDic(string pResultXML)
        {
            pResultXML = pResultXML.Replace("&", "&amp;");
            Dictionary<string, string> result = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pResultXML);
            var list = doc.DocumentElement.ChildNodes;
            foreach (XmlNode item in list)
            {
                result[item.Name] = HttpUtility.UrlDecode(item.InnerXml, Encoding.UTF8);
            }
            return result;
        }
    }
}
