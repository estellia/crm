using System;
using System.Web;
using JIT.Utility.CometManagement.Message;

namespace JIT.Utility.CometManagement
{
    public class CometResult : IAsyncResult
    {
        private static readonly string[] MessageBodys = new string[] {
            "您有一封新邮件", 
            "李阳给你发来了一份设计图", 
            "李渊完成了PRD", 
            "{    \"Type\": \"QiXinCommand\",\"Description\": \"更新通讯录\",\"Request\": {\"action\": \"UpdateAddressBook\",\"UserId\": \"00da749cbb4f4de8acae74c85448e95d\"}}",
            "{    \"Type\": \"CrmCommand\",\"Description\": \"客户XX发来一个邀请\",\"Request\": {\"action\": \"CustomerDate\",\"DateId\": \"028eae8bbc734ffaaca389e1d407bbc1\"}}", 
            "{    \"Type\": \"MobileLearningCommand\",\"Description\": \"HR发布学习任务\",\"Request\": {\"action\": \"NewLearningTask\",\"TaskId\": \"017278c379164010a8e01904431a5511\"}}" 
        };

        public object AsyncState
        {
            get;
            private set;
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get;
            private set;
        }

        public bool CompletedSynchronously
        {
            get;
            private set;
        }

        public bool IsCompleted
        {
            get;
            private set;
        }

        public HttpCookie AuthCookie { get; set; }
        public AsyncCallback Callback { get; private set; }
        public HttpContext Context { get; private set; }
        public object ExtraData { get; private set; }

        public CometResult(HttpContext context, AsyncCallback callback, object extraData)
        {
            Context = context;
            Callback = callback;
            ExtraData = extraData;
        }

        /// <summary>
        /// End this comet.
        /// </summary>
        public void Call()
        {
            IsCompleted = true;

            if (Callback != null)
                Callback(this);
        }

        /// <summary>
        /// End this comet with normal timeout.
        /// </summary>
        public void CallRefresh()
        {
            IsCompleted = true;

            var message = new CometHeartbeatMessage();
            Context.Response.Write(message.ToJson());

            if (Callback != null)
                Callback(this);
        }

        /// <summary>
        /// End this comet with normal timeout.
        /// </summary>
        /// <param name="requestJson">Json string send to client</param>
        public void CallReqeust(string requestJson)
        {
            this.IsCompleted = true;

            Context.Response.Write(requestJson);

            if (Callback != null)
                Callback(this);
        }

        /// <summary>
        /// 客户端结束退出
        /// </summary>
        public void CallLogout()
        {
            this.IsCompleted = true;
            this.CompletedSynchronously = true;

            this.CallRefresh();
        }

        /// <summary>
        /// 模拟心跳，用于调试
        /// </summary>
        public void CallHeartBeat()
        {
            this.IsCompleted = true;

            Random r = new Random(DateTime.Now.Millisecond);
            var index = r.Next(MessageBodys.Length);
            var message = new CometNotifyMessage(DateTime.Now + " " + MessageBodys[index]);
            Context.Response.Write(message.ToJson());
            if (Callback != null)
                Callback(this);
        }
    }
}
