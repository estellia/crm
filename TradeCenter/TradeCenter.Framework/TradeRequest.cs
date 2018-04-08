/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/1/2 10:47:40
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

using JIT.TradeCenter.Framework.ValueObject;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;

namespace JIT.TradeCenter.Framework
{
    /// <summary>
    /// 交易请求 
    /// </summary>
    public class TradeRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TradeRequest()
        {
        }
        #endregion

        public int? AppID { get; set; }

        /// <summary>
        /// 发送交易请求的客户ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 发送交易请求的用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 预留，用于安全认证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 请求参数
        /// <remarks>
        /// <para>请求参数与请求操作一一对应</para>
        /// </remarks>
        /// </summary>
        public object Parameters { get; set; }

        public T GetParameter<T>()
        {
            if (this.Parameters == null)
                return default(T);
            else
                return this.Parameters.ToJSON().DeserializeJSONTo<T>();
        }

        public BasicUserInfo GetUserInfo()
        {
            BasicUserInfo user = new BasicUserInfo() { ClientID = this.ClientID, UserID = this.UserID };
            return user;
        }
    }
}
