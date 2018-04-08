using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.AppVersion.Framework
{
    public class AppMgrRequest
    {
        /// <summary>
        /// 接口认证(预留)
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 应用Code
        /// </summary>
        public string AppCode { get; set; }
        /// <summary>
        /// 业务参数
        /// </summary>
        public object Parameters { get; set; }

        /// <summary>
        /// 平台，1-Android,2-IOS
        /// </summary>
        public string Plat { get; set; }

        public BasicUserInfo GetUserInfo()
        {
            return new BasicUserInfo() { ClientID = this.ClientID, UserID = this.UserID };
        }

        public T GetParameters<T>()
        {
            return this.Parameters.ToJSON().DeserializeJSONTo<T>();
        }
    }
}
