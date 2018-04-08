using OpenAPI.RedisX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenAPI.Models
{
    /// <summary>
    /// 用于获取全局唯一ID
    /// </summary>
    public class IdentitySingleton
    {
        private static RedisDBEnum _RedisDB = RedisDBEnum.Six;
        private static RedisOperation db = new RedisOperation(_RedisDB);
        private static readonly object padlock = new object();
        private static readonly string key = "Identity";
        public static Int64 GetIdentity
        {
            get
            {
                Int64 result = -1;
                lock (padlock)
                {
                    var value = db.GetKeyString(key);
                    if (!string.IsNullOrEmpty(value))
                    {
                        result = Convert.ToInt64(value) + 1;
                        db.Insert(key, result.ToString());
                    }
                }
                return result;
            }
        }
    }
}