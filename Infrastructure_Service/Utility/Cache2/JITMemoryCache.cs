/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/16 10:09:08
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using System.Xml;

namespace JIT.Utility.Cache2
{
    /// <summary>
    /// JIT的内存缓存 
    /// </summary>
    public class JITMemoryCache : BaseCache, IJITCache2
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        private JITMemoryCache()
        {
            this._policy = new CacheItemPolicy();
            this._policy.Priority = CacheItemPriority.NotRemovable;
        }
        #endregion

        private static JITMemoryCache _default;
        public static JITMemoryCache GetInstance()
        {
            if (_default != null)
                return _default;
            else
                return _default = new JITMemoryCache();
        }

        #region 属性集
        private CacheItemPolicy _policy = null;
        #endregion

        /// <summary>
        /// 添加需要缓存的对象()
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        /// <param name="pCachedObject">被缓存的对象</param>
        public void AddOrReplace(string pKey, object pCachedObject)
        {
            AddOrReplace(pKey, pCachedObject, DateTime.MaxValue);
        }

        /// <summary>
        /// 添加需要缓存的对象()
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        /// <param name="pCachedObject">被缓存的对象</param>
        /// <param name="pTime">到期时间</param>
        public void AddOrReplace(string pKey, object pValue, DateTime pTime)
        {
            CheckKey(pKey);
            CACHE_PROVIDER.Set(pKey, pValue, pTime);
        }

        /// <summary>
        /// 获取已缓存的对象
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        public object Get(string pKey)
        {
            var node = _doc.SelectSingleNode("//" + pKey);
            if (node != null)
                return CACHE_PROVIDER[pKey];
            else
                return null;
        }

        /// <summary>
        /// 移除指定的缓存项
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        public void Remove(string pKey)
        {
            var node = _doc.SelectSingleNode("//" + pKey);
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
                CACHE_PROVIDER.Remove(pKey);
            }
        }

        /// <summary>
        /// 获得一个对默认MemoryCache的引用
        /// </summary>
        private static ObjectCache CACHE_PROVIDER = MemoryCache.Default;

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="pXpath"></param>
        public void RemoveRange(string pXpath)
        {
            var node = _doc.SelectSingleNode("//" + pXpath);
            var keys = GetAllKeys(pXpath);
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
            }
            foreach (var item in keys)
            {
                CACHE_PROVIDER.Remove(item);
            }
        }

        /// <summary>
        /// 保存键到文件
        /// </summary>
        /// <param name="path">文件的全路径</param>
        /// <param name="errorMessage">错误信息</param>
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
        /// 大数据的缓存
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
        /// 大数据的获取
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
