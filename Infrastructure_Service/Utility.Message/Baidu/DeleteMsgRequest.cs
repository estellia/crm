/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 11:27:21
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

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Message.Baidu.ValueObject;

namespace JIT.Utility.Message.Baidu
{
    /// <summary>
    /// 百度云推送的delete_msg方法的请求 
    /// </summary>
    public class DeleteMsgRequest : BaiduPushMessageRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DeleteMsgRequest()
        {
            base.Method = "delete_msg";
        }
        #endregion

        /// <summary>
        /// API的资源操作方法名
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public override string Method
        {
            get
            {
                return base.Method;
            }
            set
            {
                base.Method = value;
            }
        }

        /// <summary>
        /// 用户标识，在Android里，channel_id + userid指定某一个特定client。不超过256字节，如果存在此字段，则只推送给此用户。
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public string BaiduUserID
        {
            get { return this.GetParam<string>("user_id"); }
            set { this.SetParam("user_id", value); }
        }

        /// <summary>
        /// 删除的消息id列表，由一个或多个msg_id组成，多个用json数组表示。如：123 或 [123, 456]。
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string MsgIds
        {
            get { return this.GetParam<string>("msg_ids"); }
            set { this.SetParam("msg_ids", value); }
        }
    }
}
