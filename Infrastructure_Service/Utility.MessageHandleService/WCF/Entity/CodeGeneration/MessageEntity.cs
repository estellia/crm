/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/3 17:21:09
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
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.Utility.Message.WCF.Entity
{
    /// <summary>
    /// 实体： 推送消息表 
    /// </summary>
    public partial class MessageEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MessageEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? MessageID { get; set; }

        /// <summary>
        /// 配置ID。表MessageChannel的外键
        /// </summary>
        public Int32? ChannelID { get; set; }

        /// <summary>
        /// 将消息推送大哪各APP下
        /// </summary>
        public String AppCode { get; set; }

        /// <summary>
        /// 接收消息的用户所属的客户ID
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// 接收消息的用户的所属用户ID
        /// </summary>
        public String UserID { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public String MessageContent { get; set; }

        /// <summary>
        /// 推送消息的参数,参数的内容为JSON
        /// </summary>
        public String MessageParameters { get; set; }

        /// <summary>
        /// 发送的次数
        /// </summary>
        public Int32? SendCount { get; set; }

        /// <summary>
        /// 状态0=未发送1=发送失败2=发送成功
        /// </summary>
        public Int32? Status { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public String FaultReason { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 创建者用户ID
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后更新者用户ID
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}