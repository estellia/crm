using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JIT.Utility.Cache2
{
    public  class CacheFactory
    {
        public static IJITCache2 CreateCache()
        {
            var type = ConfigurationManager.AppSettings["CacheType"];
            string str = ConfigurationManager.AppSettings["CacheServers"];
            string[] serverlist;
            if (string.IsNullOrEmpty(str))
            {
                serverlist = new string[] { };
            }
            else
                serverlist = str.Split(',');
            switch (type)
            {
                case "0":
                    return JITMemoryCache.GetInstance();
                case "1":
                    return JITDistributedCache.GetInstance(serverlist);
                default:
                    return JITMemoryCache.GetInstance();
            }
        }
    }
}
