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

namespace JIT.Utility.Cache
{
    /// <summary>
    /// JIT的内存缓存 
    /// </summary>
    public class JITMemoryCache : IJITCache
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public JITMemoryCache()
        {
            this._policy = new CacheItemPolicy();
            this._policy.Priority = CacheItemPriority.NotRemovable;
        }
        #endregion

        #region 属性集
        private CacheItemPolicy _policy = null;
        #endregion

        #region IJITCache 成员
        /// <summary>
        /// 添加需要缓存的对象
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        /// <param name="pCachedItem">被缓存的对象</param>
        public void Add(BasicUserInfo pUserInfo, string pKey, object pCachedObject)
        {
            CACHE_PROVIDER.Set(pKey, pCachedObject, this._policy);
        }
        /// <summary>
        /// 添加需要缓存的对象()
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        /// <param name="pCachedItem">被缓存的对象</param>
        public void AddOrReplace(BasicUserInfo pUserInfo, string pKey, object pCachedObject)
        {
            CACHE_PROVIDER.Set(pKey, pCachedObject, new CacheItemPolicy() { Priority = CacheItemPriority.Default, AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5), SlidingExpiration = TimeSpan.Zero });
        }
        /// <summary>
        /// 获取已缓存的对象
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        public object Get(BasicUserInfo pUserInfo, string pKey)
        {
            return CACHE_PROVIDER[pKey];
        }

        /// <summary>
        /// 移除指定的缓存项
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        public void Remove(BasicUserInfo pUserInfo, string pKey)
        {
            CACHE_PROVIDER.Remove(pKey);
        }
        #endregion

        /// <summary>
        /// 获得一个对默认MemoryCache的引用
        /// </summary>
        private static ObjectCache CACHE_PROVIDER = MemoryCache.Default;
    }
}
