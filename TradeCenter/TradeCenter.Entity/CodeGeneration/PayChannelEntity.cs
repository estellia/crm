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
    /// 实体： 支付通道支付通道+订单信息 组成了支付所必须的所有信息。如果应用下各个客户的收款账户不一样,则应用下的各个客户的支付通道是不同的。 
    /// </summary>
    public partial class PayChannelEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PayChannelEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 支付渠道ID，自增主键
        /// </summary>
        public Int32? ChannelID { get; set; }

        /// <summary>
        /// 支付方式
        /// <remarks>
        /// <para>当前支持的有：</para>
        /// <para>1=银联WAP支付</para>
        /// <para>2=银联语音支付</para>
        /// <para>3=支付宝WAP支付</para>
        /// <para>4=支付宝线下支付</para>
        /// <para>5=微信支付</para>
        /// <para>6=旺财支付</para>
        /// </remarks>
        /// </summary>
        public Int32? PayType { get; set; }

        /// <summary>
        /// 支付通道的参数,各种类型的支付方式的参数会不一样。因此参数以JSON的格式存储
        /// </summary>
        public String ChannelParameters { get; set; }

        /// <summary>
        /// 是否是测试Channel
        /// </summary>
        public Boolean? IsTest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String NotifyUrl { get; set; }

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