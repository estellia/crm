using System;
using System.Web;

namespace JIT.Utility.CometManagement
{
    public class CometHandler : IHttpAsyncHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //write your handler implementation here.
            // Todo: process logout.
        }

        #endregion

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            // Todo: 解析命令
            var userId = context.Request["userid"];
            var action = context.Request["action"];

            // 生成IAsyncResult对象，调用callback回调，EndProcessRequest才被触发
            var result = new CometResult(context, cb, extraData);

            if (string.IsNullOrEmpty(userId))
            {
                result.CallRefresh();
                return result;
            }

            if(!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "logout":
                        CometRequestManager.Instance.Release(userId);
                        result.CallLogout();
                        return result;
                    default:
                        break;
                }
            }
            // 保存IAsyncResult对象
            CometRequestManager.Instance.UpdateComet(userId, result);
            return result;
        }

        public void EndProcessRequest(IAsyncResult asyncResult)
        {
            // 得到对应的IAsyncResult对象
            var result = asyncResult as CometResult;
            if (result != null)
            {
            }
        }
    }
}
