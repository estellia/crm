using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Configuration;

namespace JIT.Utility.Cache2
{
    public abstract class BaseCache
    {
        static BaseCache()
        {
            _doc = new XmlDocument();
            XmlElement root = _doc.CreateElement("Memcached");
            _doc.AppendChild(root);
            var value = ConfigurationManager.AppSettings["Segmentation"];
            int m;
            if (int.TryParse(value, out m))
            {
                if (m > 100)
                    _segmentation = m;
            }
        }

        protected static readonly XmlDocument _doc;
        protected static readonly int _segmentation = 100;
        /// <summary>
        /// 检查键值是否存大,不存在则添加
        /// </summary>
        /// <param name="pUserInfo"></param>
        /// <param name="pKey"></param>
        protected void CheckKey(string pKey)
        {
            var keys = pKey.Split('/');
            var keyscount = keys.Count();
            lock (this)
            {
                try
                {
                    string str = string.Empty;
                    XmlElement current = _doc.DocumentElement;
                    for (int i = 0; i < keyscount; i++)
                    {
                        str += keys[i] + "/";
                        var temp = _doc.SelectSingleNode(string.Format("//{0}", str.Trim('/')));
                        if (temp == null)
                        {
                            var node = _doc.CreateElement(keys[i]);
                            current.AppendChild(node);
                            current = node;
                        }
                        else
                        {
                            current = temp as XmlElement;
                        }

                    }
                }
                catch (Exception ee)
                {
                    Loggers.Exception(new ExceptionLogInfo(ee));
                    throw ee;
                }
            }
        }

        /// <summary>
        /// 检查是否存在键
        /// </summary>
        /// <param name="pUserInfo"></param>
        /// <param name="pKey"></param>
        /// <returns></returns>
        protected bool IsExsits(BasicUserInfo pUserInfo, string pKey)
        {
            return _doc.SelectSingleNode(string.Format("/*/C{0}/U{1}/{2}", pUserInfo.ClientID, pUserInfo.UserID, pKey)) != null;
        }

        /// <summary>
        /// 根据用户获取所有键
        /// </summary>
        /// <param name="pUserInfo"></param>
        /// <returns></returns>
        protected string[] GetAllKeys(string pXpath)
        {
            List<string> keys = new List<string> { };
            var list = _doc.SelectNodes(string.Format("//{0}/*", pXpath));
            foreach (XmlElement item in list)
            {
                keys.Add(GetKey(item));
            }
            return keys.ToArray();
        }

        /// <summary>
        /// 获取缓存中的Key
        /// </summary>
        /// <param name="pUserInfo"></param>
        /// <param name="pKey"></param>
        /// <returns></returns>
        protected string GetKey(XmlElement element)
        {
            string str = string.Empty;
            var temp = element;
            while (temp != element.OwnerDocument.DocumentElement)
            {
                str = temp.Name + "/" + str;
                temp = temp.ParentNode as XmlElement;
            }
            return str.Trim('/');
        }

    }
}
