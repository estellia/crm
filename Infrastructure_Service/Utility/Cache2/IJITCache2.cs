using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Cache2
{
    public interface IJITCache2
    {
        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <param name="pUserInfo">用户</param>
        /// <param name="pKey">键</param>
        /// <param name="pValue">值</param>
        /// <returns></returns>
        void AddOrReplace(string pKey, object pValue);

        /// <summary>
        /// 设置带有时效的缓存值
        /// </summary>
        /// <param name="pUserInfo">用户</param>
        /// <param name="pKey">键</param>
        /// <param name="pValue">值</param>
        /// <param name="pTime">到期时间</param>
        /// <returns></returns>
        void AddOrReplace(string pKey, object pValue, DateTime pTime);

        /// <summary>
        /// 设置大数据缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pUserInfo"></param>
        /// <param name="pKey"></param>
        /// <param name="pValue"></param>
        /// <param name="pTime"></param>
        void AddOrReplaceRange<T>(string pKey, List<T> pValue);

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="pUserInfo">用户</param>
        /// <param name="pKey">键</param>
        /// <returns>值</returns>
        object Get(string pKey);

        /// <summary>
        /// 获取大数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pUserInfo"></param>
        /// <param name="pKey"></param>
        /// <returns></returns>
        List<T> GetRange<T>(string pKey);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="pUserInfo">用户</param>
        /// <param name="pKey">键</param>
        /// <returns></returns>
        void Remove(string pKey);

        /// <summary>
        /// 删除用户的所有缓存
        /// </summary>
        /// <param name="pUserInfo">用户</param>
        void RemoveRange(string pXpath);

        /// <summary>
        /// 保存缓存键到文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        bool SaveCacheMap(string path, out string errorMessage);

    }
}
