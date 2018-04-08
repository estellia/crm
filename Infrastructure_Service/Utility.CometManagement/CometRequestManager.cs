using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace JIT.Utility.CometManagement
{
    public enum CometState
    {
        Success = 0,
        UserNotConnected = 1,
        UnknownError = -1,
    }

    public class CometRequestManager
    {
        /// <summary>
        /// 长轮询生命周期(分钟)
        /// </summary>
        public const double CometLifeTimeMinute = 5;

        /// <summary>
        /// 长轮询推送重试间隔（秒）
        /// </summary>
        public const int CometPushRetryInteval = 5;

        /// <summary>
        /// Comet凭证时效检查间隔（毫秒）
        /// </summary>
        public const int LifetimeCheckInteval = 5000;

        /// <summary>
        /// 时效检查启动预备时间（毫秒）
        /// </summary>
        public const int LifetimeCheckDueTime = 1000;

        private static readonly Hashtable TokenTable = new Hashtable();
        private static readonly AutoResetEvent AutoEvent = new AutoResetEvent(false);
        private static readonly TimerCallback Tcb = StateCheckCallback;
        private static readonly Timer StateCheckTimer = new Timer(Tcb, AutoEvent, LifetimeCheckDueTime, LifetimeCheckInteval);

        private static readonly Queue<MessageResendItem> MessageRetryQueue = new Queue<MessageResendItem>();
        private static readonly AutoResetEvent AutoEventRetry = new AutoResetEvent(false);
        private static readonly TimerCallback RetryCallback = MessageRetryCallback;
        private static readonly Timer MessageRetryTimer = new Timer(RetryCallback, AutoEventRetry, 1000, 5000);
        private static bool _isRunning;
        /// <summary>
        /// Singleton object
        /// </summary>
        private static CometRequestManager _instance;

        public static CometRequestManager Instance
        {
            get { return _instance ?? (_instance = new CometRequestManager()); }
        }

        /// <summary>
        /// private constructor
        /// </summary>
        private CometRequestManager()
        {
        }

        public void UpdateComet(string userId, CometResult result)
        {
            if (TokenTable.ContainsKey(userId))
            {
                TokenTable[userId] = new CometRequestLifetimeItem(userId, DateTime.Now.AddMinutes(CometLifeTimeMinute),
                    result);
            }
            else
            {
                TokenTable.Add(userId, new CometRequestLifetimeItem(userId, DateTime.Now.AddMinutes(CometLifeTimeMinute), result));
            }
        }

        public CometState PushCometMessage(string userId, string messageJson)
        {
            if (!TokenTable.ContainsKey(userId))
            {
                return CometState.UserNotConnected;
            }

            var result = CometState.Success;
            try
            {
                var item = TokenTable[userId] as CometRequestLifetimeItem;
                if (item != null)
                {
                    var oldResult = item.CometResult;
                    oldResult.CallReqeust(messageJson);
                    TokenTable[userId] = null;
                }
                else
                {
                    var retryItem = new MessageResendItem(userId, messageJson, DateTime.Now.AddSeconds(CometPushRetryInteval));
                    MessageRetryQueue.Enqueue(retryItem);
                }
            }
            catch (Exception)
            {
                result = CometState.UnknownError;
            }

            return result;
        }

        /// <summary>
        /// Timer回调，清理超时comet
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void StateCheckCallback(Object stateInfo)
        {
            try
            {
                foreach (object key in TokenTable.Keys)
                {
                    var item = TokenTable[key] as CometRequestLifetimeItem;
                    if(item == null)
                        continue;

#if(DEBUG)
                    // 测试代码
                    item.CometResult.CallHeartBeat();
                    TokenTable[key] = null;
#endif
                    if (item.LifeTime > DateTime.Now) continue;
                    item.CometResult.CallRefresh();
                    TokenTable[key] = null;

                }
            }
            catch (Exception ex)
            {
                // Todo: log the exception
            }
        }

        /// <summary>
        /// 移除comet入口
        /// </summary>
        /// <param name="userId"></param>
        public void Release(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                TokenTable.Remove(userId);
            }
        }

        /// <summary>
        /// Timer回调，清理超时comet
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void MessageRetryCallback(Object stateInfo)
        {
            if (_isRunning || (MessageRetryQueue.Count == 0) || MessageRetryQueue.First().SendTime > DateTime.Now)
            {
                return;
            }

            _isRunning = true;
            try
            {
                do
                {
                    var item = MessageRetryQueue.Dequeue();
                    Instance.PushCometMessage(item.UserId, item.MessageBody);
                } while (MessageRetryQueue.First().SendTime > DateTime.Now);
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}
