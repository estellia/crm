/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/16 9:59:38
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
using System.Text;

using JIT.Utility;

namespace JIT.Utility.Cache
{
    /// <summary>
    /// JIT的缓存机制 
    /// </summary>
    public interface IJITCache
    {
        /// <summary>
        /// 添加需要缓存的对象
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        /// <param name="pCachedItem">被缓存的对象</param>
        void Add(BasicUserInfo pUserInfo, string pKey, object pCachedObject);


        /// <summary>
        /// 添加需要缓存的对象（如果已存在，则替换）
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        /// <param name="pCachedItem">被缓存的对象</param>
        void AddOrReplace(BasicUserInfo pUserInfo, string pKey, object pCachedObject);

        /// <summary>
        /// 获取已缓存的对象
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        object Get(BasicUserInfo pUserInfo, string pKey);

        /// <summary>
        /// 移除指定的缓存项
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pKey">缓存项的键值</param>
        void Remove(BasicUserInfo pUserInfo, string pKey);

    }
}
