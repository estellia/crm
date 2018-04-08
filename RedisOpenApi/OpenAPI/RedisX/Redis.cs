using RedisOpenAPIClient.Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RedisOpenAPIClient.MethodExtensions.NumberExtensions;

namespace OpenAPI.RedisX
{
    internal class Redis
    {
        /// <summary>
        /// 连接多路复用器 / private static
        /// </summary>
        private static ConnectionMultiplexer _multiRoute = default(ConnectionMultiplexer);

        /// <summary>
        /// Redis配置 / private static
        /// </summary>
        private static ConfigurationOptions _redisConfig = default(ConfigurationOptions);

        /// <summary>
        /// ServerIP
        /// </summary>
        private static string ServerIP { get; set; }

        /// <summary>
        /// ServerPort
        /// </summary>
        private static int ServerPort { get; set; }

        /// <summary>
        /// ServerPassword
        /// </summary>
        private static string ServerPassword { get; set; }

        /// <summary>
        /// DefaultValidTimeSpan
        /// </summary>
        public static int DefaultValidTimeSpan { get; set; }

        /// <summary>
        /// Redis / DB / public static
        /// </summary>
        /// <param name="dbIndex">RedisDB,默认dbIndex =0</param>
        public static IDatabase DB(int dbIndex = 0)
        {
            CheckMultiRoute();
            //
            return _multiRoute.GetDatabase(dbIndex);
        }

        /// <summary>
        /// 构造函数 / private static
        /// </summary>
        private Redis()
        {
            
        }

        /// <summary>
        /// 初始化 Redis 配置  /  private static
        /// 参考[Test.GitHubDoc.Configuration]
        /// </summary>
        private static void InitRedisConfig()
        {
            //
            if (string.IsNullOrWhiteSpace(ServerIP))
            {
                ServerIP = AppConfigHelper.GetAppSettingsValue("ServerIP");
            }
            if (ServerPort==0)
            {
                ServerPort = AppConfigHelper.GetAppSettingsValue("ServerPort").ToInt();
            }
            if (string.IsNullOrWhiteSpace(ServerPassword))
            {
                ServerPassword = AppConfigHelper.GetAppSettingsValue("ServerPassword");
            }
            if (DefaultValidTimeSpan == 0)
            {
                DefaultValidTimeSpan = AppConfigHelper.GetAppSettingsValue("DefaultValidTimeSpan").ToInt();
            }

            //
            _redisConfig = new ConfigurationOptions
            {
                EndPoints = 
                { 
                    {ServerIP,ServerPort}
                }
            };

            //
            _redisConfig.Password = ServerPassword;

            //
            _redisConfig.AbortOnConnectFail = true;
            _redisConfig.AllowAdmin = false;
            _redisConfig.ConnectRetry = 3;
            _redisConfig.ConnectTimeout = 500;
            _redisConfig.DefaultDatabase = 0;
            _redisConfig.KeepAlive = 1;
            _redisConfig.SyncTimeout = 5000;
        }

        /// <summary>
        /// 检查并初始化 连接多路复用器  /  private static
        /// </summary>
        private static void CheckMultiRoute()
        {
            // 
            if (_redisConfig == null)
            {
                InitRedisConfig();
            }

            //
            if (_multiRoute == null)
            {
                _multiRoute = ConnectionMultiplexer.Connect(_redisConfig);
            }
        }
    }
}