using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient
{
    public static class RemoteSettings
    {
        /// <summary>
        /// 设置请求超时  ms
        /// </summary>
        public static RedisOpenAPI SetTimeOut(this RedisOpenAPI remote, int timeoutTime)
        {
            remote.TimeoutTime = timeoutTime;
            return remote;
        }

        /// <summary>
        /// 设置请求数据 json
        /// </summary>
        //public static RedisOpenAPI SetRequstData(this RedisOpenAPI remote, string requestJSON)
        //{
        //    remote.Content = requestJSON;
        //    return remote;
        //}

        /// <summary>
        /// 设置请求重试
        /// </summary>
        public static RedisOpenAPI SetRetryCount(this RedisOpenAPI remote, int retryCount)
        {
            remote.RetryCount = retryCount;
            return remote;
        }

        /// <summary>
        /// 柯里化
        /// </summary>
        private static Func<int, Func<int, Func<string, RedisOpenAPI>>> SetConfig(this RedisOpenAPI remote)
        {
            return (int timeoutTime) =>
                       (int retryCount) =>
                       (string requestJSON) =>
                       {
                           remote.SetTimeOut(timeoutTime);
                           remote.SetRetryCount(retryCount);
                           //remote.SetRequstData(requestJSON);
                           return remote;
                       };
        }



        /// <summary>
        /// 测试
        /// </summary>
        private static void test()
        {
            RedisOpenAPI.Instance.SetConfig()(1)(1)("");

        }
    }
}
