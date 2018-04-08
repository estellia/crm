using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Cache;
using Memcached.ClientLibrary;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using System.Configuration;

namespace JIT.Utility.Cache2
{
    public class JITDistributedCache : BaseCache, IJITCache2
    {
        private JITDistributedCache(string[] pServerList)
        {
            ServerList = pServerList;
            if (ServerList.Length == 0)
                ServerList = new string[] { "127.0.0.1:11211" };
            Init();
        }

        SockIOPool pool;
        MemcachedClient client;

        private static JITDistributedCache _default;
        /// <summary>
        /// 返回单一实例
        /// </summary>
        /// <param name="pServerList"></param>
        /// <returns></returns>
        public static JITDistributedCache GetInstance(string[] pServerList)
        {
            if (_default != null)
                return _default;
            else
                return _default = new JITDistributedCache(pServerList);
        }

        /// <summary>
        /// 格式为:  IP地址:端口,如127.0.0.1:11211
        /// </summary>
        public string[] ServerList { get; private set; }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">键</param>
        /// <param name="pValue">值</param>
        /// <returns>是否成功</returns>
        public void AddOrReplace(string pKey, object pValue)
        {
            AddOrReplace(pKey, pValue, DateTime.MaxValue);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">键</param>
        /// <param name="pValue">值</param>
        /// <param name="pTime">到期时间,失效时间</param>
        /// <returns>是否成功</returns>
        public void AddOrReplace(string pKey, object pValue, DateTime pTime)
        {
            CheckKey(pKey);
            client.Set(pKey, pValue, pTime);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">键</param>
        /// <returns>值</returns>
        public object Get(string pKey)
        {
            var node = _doc.SelectSingleNode("//" + pKey);
            if (node != null)
                return client.Get(pKey);
            else
                return null;
        }

        /// <summary>
        /// 根据键删除
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">键</param>
        /// <returns>是否成功</returns>
        public void Remove(string pKey)
        {
            var node = _doc.SelectSingleNode("//" + pKey);
            if (node != null)
                node.ParentNode.RemoveChild(node);
        }

        /// <summary>
        /// 根据xpath路径删除子节点的所有内容
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        public void RemoveRange(string pXpath)
        {
            Remove(pXpath);
        }

        /// <summary>
        /// 保存当前缓存到文件,便于检查当前用户的键
        /// </summary>
        /// <param name="path">保存的完整路径</param>
        /// <returns></returns>
        public bool SaveCacheMap(string path, out string errorMessage)
        {
            try
            {
                _doc.Save(path);
                errorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                errorMessage = "保存文件失败:" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //配置分布缓存池和客户端
            pool = SockIOPool.GetInstance();
            pool.SetServers(ServerList);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            client = new MemcachedClient();
        }

        /// <summary>
        /// 对大数据的缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pKey"></param>
        /// <param name="pValue"></param>
        public void AddOrReplaceRange<T>(string pKey, List<T> pValue)
        {
            int m = pValue.Count % _segmentation == 0 ? 0 : 1;
            int c = pValue.Count / _segmentation;
            if (c == 0)
            {
                AddOrReplace(pKey, pValue);
            }
            else
            {
                for (int i = 0; i < c + m; i++)
                {
                    var key = pKey + "/Sub" + i;
                    CheckKey(key);
                    if (i != c + m - 1)
                    {
                        AddOrReplace(key, pValue.GetSubs(i * _segmentation, (i + 1) * _segmentation));
                    }
                    else
                    {
                        if (m == 0)
                            AddOrReplace(key, pValue.GetSubs(i * _segmentation, (i + 1) * _segmentation));
                        else
                            AddOrReplace(key, pValue.GetSubs(i * _segmentation, i * _segmentation + pValue.Count % _segmentation));
                    }
                }
            }
        }

        /// <summary>
        /// 对大数据的获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pKey"></param>
        /// <returns></returns>
        public List<T> GetRange<T>(string pKey)
        {
            var node = _doc.SelectSingleNode("//" + pKey);
            if (node != null)
            {
                if (node.HasChildNodes)
                {
                    List<T> list = new List<T> { };
                    foreach (var item in node.ChildNodes)
                    {
                        var key = GetKey(item as XmlElement);
                        var temp = Get(key) as List<T>;
                        if (temp != null)
                            list.AddRange(temp);
                    }
                    return list;
                }
                else
                    return Get(pKey) as List<T>;
            }
            return null;
        }
    }

}
