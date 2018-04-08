using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JIT.Utility.Log;

namespace JIT.Utility.Cache2
{
    /// <summary>
    /// 分布式缓存
    /// </summary>
    public static class Caches2
    {
        static Caches2()
        {
            Default = CacheFactory.CreateCache();
        }

        public static readonly IJITCache2 Default = null;
    }
}
