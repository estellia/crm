/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/8 15:45:54
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

namespace JIT.TradeCenter.Entity
{
    /// <summary>
    /// 实体： 应用订单表 
    /// </summary>
    public partial class AppOrderEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AppOrderEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 自增主键，此ID为发送给支付平台用于标识订单信息的订单ID
        /// </summary>
        public Int64? OrderID { get; set; }

        /// <summary>
        /// 应用ID。表App的外键
        /// </summary>
        public Int32? AppID { get; set; }

        /// <summary>
        /// 应用的客户ID
        /// </summary>
        public String AppClientID { get; set; }

        /// <summary>
        /// 应用中下单的用户的ID。
        /// </summary>
        public String AppUserID { get; set; }

        /// <summary>
        /// 支付通道ID。表PayChannel的外键
        /// </summary>
        public Int32? PayChannelID { get; set; }

        /// <summary>
        /// 应用的订单ID
        /// </summary>
        public String AppOrderID { get; set; }

        /// <summary>
        /// 订单描述
        /// </summary>
        public String AppOrderDesc { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime? AppOrderTime { get; set; }

        /// <summary>
        /// 订单金额(金额的单位为币种下的最小货币单位,例如：RMB则为分)
        /// </summary>
        public Int32? AppOrderAmount { get; set; }

        /// <summary>
        /// 消费者的手机号。当为银联语音支付时,该字段必须填写。
        /// </summary>
        public String MobileNO { get; set; }

        /// <summary>
        /// 币种，其中值为：1=RMB默认为RMB
        /// </summary>
        public Int32? Currency { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public String ErrorMessage { get; set; }

        /// <summary>
        /// 支付跳转页
        /// </summary>
        public String PayUrl { get; set; }

        /// <summary>
        /// 是否已通知
        /// </summary>
        public Boolean? IsNotified { get; set; }

        /// <summary>
        /// 通知次数
        /// </summary>
        public Int32? NotifyCount { get; set; }

        /// <summary>
        /// 下次通知时间
        /// </summary>
        public DateTime? NextNotifyTime { get; set; }

        /// <summary>
        /// 状态0=初始1=预订单成功2=支付成功
        /// </summary>
        public Int32? Status { get; set; }

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

        /// <summary>
        /// 微信用户id
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 客户端ip
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string NotifyUrl { get; set; }



        #endregion

    }
}