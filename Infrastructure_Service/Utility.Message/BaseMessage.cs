/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/2 16:18:41
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

namespace JIT.Utility.Message
{
    /// <summary>
    /// 推送的消息的基类 
    /// </summary>
    public abstract class BaseMessage
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseMessage()
        {
            this.MessageID = Guid.NewGuid();
            this.MessageParameters = new Dictionary<string, object>();
        }
        #endregion

        /// <summary>
        /// 消息的ID
        /// </summary>
        public Guid? MessageID { get; set; }

        /// <summary>
        /// 消息推送到的手机端应用的编码
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// 推送给的用户所属的客户的ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 推送给的用户的ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public virtual string MessageContent { get; set; }

        /// <summary>
        /// 消息的参数字典
        /// </summary>
        internal Dictionary<string, object> MessageParameters { get; set; }
    }
}
